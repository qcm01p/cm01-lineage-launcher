using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

////////////////////////////////////////////////////////////////////////////////////////// <summary>
/// 닷넷 4.5.2 필요
/// 2022-01-07 개발 시작
/// by.cm01
////////////////////////////////////////////////////////////////////////////////////////// </summary>


namespace LineageConnector
{
    public partial class LineageConnector : Form
    {
        private bool UnshowInnerBroswer = false; //true일 경우 내장 브라우저 사용안함, false일 경우 내장 브라우저 사용함
        private bool USE_CM01_DLL = true;
        private bool FormClosingFlag = false;
        private bool Inited = false;

        private List<string> DownloadLinkList = new List<string>();
        private string[] BadProcessList = null;

        private string ClientZipName = "";
        private string SERVER_ADDRESS = "server_address_start_127.0.0.1_server_address_end_really_end"; //헥스 에디터로 찾기 쉽게하기 위함. (직접 소스에서 수정해도 된다)
        private string sSERVER_PORT = "pppppppport_linage_2000"; //헥스 에디터로 찾기 쉽게하기 위함. 접속기 소스를 수정하고 싶으면 여기에 리니지 서버 포트를 적어야함
        private string sPAYMENT_SERVER_PORT = "pppppppport_payment_2002"; //헥스 에디터로 찾기 쉽게하기 위함. 어지간해서는 서버 포트를 수정 하지 않는 것을 추천 (직접 소스에서 수정해도 된다)
        private int PAYMENT_SERVER_PORT = 2002; //수정 금지 (수정하고 싶으면 위에 string형 포트 수정해야함)
        private int SERVER_PORT = 2000; //수정 금지 (수정하고 싶으면 위에 string형 포트 수정해야함)
        private string UseLauncherSecurity = "!__Used__!";// 헥스 에디터로 찾기 쉽게하기 위함.      접속기 강제 종료할 때 리니지 같이 꺼지는 기능을 사용하고 싶지 않다면 "__Unused__";

        private static string HOMEPAGE_ADDRESS = "google.com";
        private Point Homepage_Start_Scroll_Pos = new Point(0, 0); //0,0 적을 경우 홈페이지 띄우면 좌측 상단부터 보이게 한다. -> 이것을 수정함으로써 홈페이지의 최초 스크롤을 조절 가능 (홈페이지 스크롤 좌표)
        private static string START_FILE_NAME = "lin.bin".ToLower();

        private static string DXWND_PATH = Environment.CurrentDirectory; //\\DXWND 이런식으로 적으면 클라이언트\DXWND 폴더가 있는것
        private static string DXWND_NAME = "WindowMode"; //DXWND 파일명
        private static string WINDOW_MODE_TITLE_NAME = "lineage";


        Zip zip = new Zip(); //압축 관련 일하는놈
        X64Checker x64checker = null;
        PaymentServerRequest paymentserver = null; //PaymentServer와 관련된 모든 일을 한다
        Downloader downloader = null; //비동기 다운로드 지원
        DXWND windowmode = null; //윈도우 모드에 사용되는 놈
        Dictionary<int, LineageProcess> Lineages = new Dictionary<int, LineageProcess>();
        private readonly object LineagesLockObj = new object();

        private bool StartLineageFlag = false; //GameStart 버튼 <-> 스레드
        private const long staticMB = 1024 * 1024; //다운로드 과정에서 참조함

        public LineageConnector()
        {
            InitializeComponent();
            if (UnshowInnerBroswer)
                webBrowser1.Visible = false;
            this.Opacity = 0.925; // 접속기 폼 투명도 조절
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        //관리자 권한 체크 (서버 보안을 위해 필수적인 과정. 왜냐면 기본적으로 클라이언트 변조하는 엔진류 프로그램들은 관리자권한으로 실행되기 때문)
        private void CheckAdmin() //이 함수 생성자에서 호출하면 에러뜬다.
        {
#if _DEBUG
#else
            //릴리즈 모드에서만 관리자 권한 체크
            if (IsAdministrator() == false)
            {
                if (MessageBox.Show("관리자 권한으로 실행해야 합니다. 실행하시겠습니까?", "알림", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        FormClosingFlag = true;
                        Task.Run(() =>
                        {
                            ProcessStartInfo procInfo = new ProcessStartInfo();
                            procInfo.UseShellExecute = true;
                            procInfo.FileName = Application.ExecutablePath;
                            procInfo.WorkingDirectory = Environment.CurrentDirectory;
                            procInfo.Verb = "runas";
                            Process.Start(procInfo);
                        });
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message.ToString());
                        ForceExit();
                    }
                }


                ForceExit();
            }
#endif
        }

        private void LineageConnector_Load(object sender, EventArgs e)
        {
            CheckAdmin();
            PAYMENT_SERVER_PORT = int.Parse(sPAYMENT_SERVER_PORT.Replace("pppppppport_payment_", "").Trim('\0')); //수정 금지
            SERVER_ADDRESS = SERVER_ADDRESS.Replace("server_address_start_", "").Replace("_server_address_end_really_end", "").Trim('\0'); //수정 금지
            SERVER_PORT = int.Parse(sSERVER_PORT.Replace("pppppppport_linage_", "").Trim('\0')); //수정 금지
            UseLauncherSecurity = UseLauncherSecurity.Replace("_", "").Replace("!", "").Trim('\0');
            paymentserver = new PaymentServerRequest(SERVER_ADDRESS, PAYMENT_SERVER_PORT);
            if (FormClosingFlag) return;
            Initialize();
            int z = 0;
        }

        private async Task<bool> Initialize()
        {
            await Task.Run(() =>
            {
                if (!ProcessInformationFromPaymentServer())
                {
                    DeleteGarbageFile();
                    return false;
                }
                if (!File.Exists(DXWND_NAME)) zip.UnzipFromMemory(Properties.Resources.WindowMode); //창모드 파일 압축 풀기
                windowmode = new DXWND(DXWND_PATH, DXWND_NAME);
                downloader = new Downloader(progressBar_download, ProgressBarChanged);
                x64checker = new X64Checker(zip); //이건 원래 이름으로 엔진 잡는 기능 말고 프로세스 검사하면서 아예 백신처럼 자가보호 기능작동하게 하는 용도로 만들었으나.. 공개 배포 버전에는 관련 코드가 포함되지 않을듯
                LoadFormResources();
                if(!x64checker.ExtractCM01()) Invoke(new MethodInvoker(delegate () { label_Status.Text = Import.CM01_DLL_NAME + " 리소스 로드 실패 (접속기는 사용 가능)"; }));
                if (!File.Exists(Import.CM01_DLL_NAME)) USE_CM01_DLL = false;
                else Import.InitCM01();
             
                Invoke(new MethodInvoker(delegate () {
                    pictureBox_GameStart.Enabled = true;
                }));

                Thread t = new Thread(ProcessThread);
                t.IsBackground = true;
                t.Start();
                Inited = true;
                Invoke(new MethodInvoker(delegate () { label_ServerName.Text = label_ServerName.Text.Trim('\0'); label_Status.Text = "접속기가 로딩이 완료되었습니다."; }));
                return true;
            });
            return true;
        }

        /// <summary>
        /// 결제서버에서 오는 정보들을 처리 (클라이언트 다운로드 링크,  홈페이지 링크)
        /// </summary>
        private bool ProcessInformationFromPaymentServer()
        {
            //클라이언트 파일이 없으면 진행
            if (!File.Exists(START_FILE_NAME))
            {
                string[] DownloadLinks = paymentserver.GetDownloadLink();
                if (DownloadLinks == null)
                {
                    AddOnlyOneDownloadComboboxError("다운로드 실패");
#if _DEBUG
                    return true;

#else
                    Invoke(new MethodInvoker(delegate () {  label_Status.Text = "클라이언트 다운로드를 하는데에 실패했습니다."; this.Hide(); }));
                    MessageBox.Show("클라이언트 다운로드를 하는데에 실패했습니다.", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Invoke(new MethodInvoker(delegate () { this.Close(); }));
                    return false;
#endif
                }

                string FileName = DownloadLinks[(int)DownloadLink.ClientZipFileName]; //서버에서 정해진 규격대로 Split됨..
                string Link1 = null;
                string Link2 = null;
                string ZipPassword = null;

                if (FileName != null && FileName.Length > 0)
                {
                    ClientZipName = FileName;
                    if (DownloadLinks.Length >= 2)
                        Link1 = DownloadLinks[(int)DownloadLink.Link1];
                    if (DownloadLinks.Length >= 3)
                        Link2 = DownloadLinks[(int)DownloadLink.Link2];
                    if (DownloadLinks.Length >= 4)
                        ZipPassword = DownloadLinks[(int)DownloadLink.ClientZipPassword];

                    if ((Link1 == null && Link2 == null) || (Link1 != null && Link2 != null && (Link1 + Link2).Length < 15)) //다운로드 서버를 두개 다 받아올 수 없는 경우 (PaymentServer에서 뭔가 설정이 잘못됨)
                    {
                        AddOnlyOneDownloadComboboxError("다운로드 링크 에러");
                        Invoke(new MethodInvoker(delegate () { label_Status.Text = "서버로부터 클라이언트 정보를 받을 수 없습니다."; }));
                    }
                    else
                    {
                        if (Link1 != null && Link1.Length > 7)
                        {
                            Invoke(new MethodInvoker(delegate ()
                            {
                                comboBox_DownloadLink.Enabled = true;
                                comboBox_DownloadLink.Items.Clear();
                                comboBox_DownloadLink.Items.Add("다운로드 서버 1");
                            }));
                            DownloadLinkList.Add(Link1);
                        }
                        if (Link2 != null && Link2.Length > 7)
                        {
                            Invoke(new MethodInvoker(delegate ()
                            {
                                comboBox_DownloadLink.Enabled = true;
                                if (Link1 == null || Link1.Length < 8) comboBox_DownloadLink.Items.Clear();
                                comboBox_DownloadLink.Items.Add("다운로드 서버 2");
                            }));
                            DownloadLinkList.Add(Link2);

                        }
                        if (comboBox_DownloadLink.Items.Count > 0)
                            Invoke(new MethodInvoker(delegate () { comboBox_DownloadLink.SelectedIndex = 0; }));
                    }


                    ///처리해준다
                }
                else
                    AddOnlyOneDownloadComboboxError("다운로드 서버 정보 없음");
            }
            else
            {
                if (comboBox_DownloadLink.Items.Count > 0)
                    Invoke(new MethodInvoker(delegate () { comboBox_DownloadLink.SelectedIndex = 0; }));
            }

            string Homepage = paymentserver.GetHomepage(); //여기서 홈페이지를 불러오지 못했을 경우는 그냥 하드코딩된 홈페이지를 출력하게 된다
            if (Homepage != null && Homepage.Length > 7) HOMEPAGE_ADDRESS = Homepage;

            string[] Badasshole = paymentserver.GetBadProcessList();
            if (Badasshole != null)
            {
                for (int i = 0; i < Badasshole.Length; i++)
                    Badasshole[i] = Badasshole[i].ToLower();
                BadProcessList = Badasshole;
            }

            return true;
        }



        //주기적으로 처리할 일이 있을 경우 처리해주는 스레드
        private void ProcessThread()
        {
            string LIN_FILE_NAME_WITHOUT_EXT = Path.GetFileNameWithoutExtension(START_FILE_NAME);
            string CurrentPath = Directory.GetCurrentDirectory();
            string LIN_PATH = Path.Combine(CurrentPath, START_FILE_NAME);

            x64checker.RunX64Checker();

            while (!FormClosingFlag)
            {
                Thread.Sleep(75);
                if (StartLineageFlag)
                    GameStart();

                RemoveExitedLineage();

            }
        }

        //폼에 관련된 리소스 로딩
        private void LoadFormResources()
        {

            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(HOMEPAGE_ADDRESS);

            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
            Invoke(new MethodInvoker(delegate ()
            {
                this.pictureBox_GameStart.Size = Properties.Resources.GameStart.Size;
                this.pictureBox_GameStart.Image = Properties.Resources.GameStart;
                if (comboBox_해상도.Items.Count > 0) comboBox_해상도.SelectedIndex = 0;
            }));
        }

        //게임시작 관련 함수
        private void GameStart()
        {
            StartLineageFlag = false;

            var loc = pictureBox_GameStart.Location;
            var newLoc = new Point(loc.X - 6, loc.Y - 6); //게임시작 흔들리는 액션.. ㅡㅡ;

            Invoke(new MethodInvoker(delegate ()
            {
                pictureBox_GameStart.Location = newLoc; //게임시작 흔들리는 액션.. ㅡㅡ;
            }));

            //여기까지 왔는데 실행파일 못찾았을 경우? 실패다
            if (!File.Exists(START_FILE_NAME)) { Invoke(new MethodInvoker(delegate () { label_Status.Text = "실행 파일을 찾을 수 없습니다. :: " + START_FILE_NAME; pictureBox_GameStart.Enabled = true; })); return; }
            
            //실행파일도 잘 있겠다.. 시작을 해본다.
            LineageProcess lineage = null;
            if (checkBox_WindowMode.Checked == false || !USE_CM01_DLL)
            {
                ProcessStartInfo info = new ProcessStartInfo(START_FILE_NAME);
                info.Arguments = SERVER_ADDRESS + " " + SERVER_PORT;
                info.UseShellExecute = false;


                Process p = Process.Start(info);
                lineage = new LineageProcess() { Lineage = p };
            }
            else
            {
                windowmode.HideDXWND();
                Thread.Sleep(150);
                int successflag = windowmode.RunProcessFromDXWND(550); //너무 빨리 실행하면 뒤지더라..  by.cm01     2022-01-13
                if (successflag < 0)
                {
                    Invoke(new MethodInvoker(delegate ()
                    {
                        label_Status.Text = "게임 실행에 실패했습니다. Flag:" + successflag;
                    }));
                }
                else
                {
                    Thread.Sleep(250);
                    Process p = FindNewLineage();
                    if (p != null)
                    {
                        lineage = new LineageProcess() { Lineage = p };
                    }

                }
            }

            if (lineage != null)
            {
                if (USE_CM01_DLL)
                {
                    bool UseSecurityFlag = true;
                    if (UseLauncherSecurity.Length > 5 && UseLauncherSecurity.ToLower().Contains("unused")) UseSecurityFlag = false; //unused면 접속기 종료할 때 리니지가 꺼지지 않도록 함.
                    int Communication = Import.SetCommunicationAddress(lineage.hProcess, UseSecurityFlag);
#if _DEBUG
                Invoke(new MethodInvoker(delegate () { button_MoveToHomePage.Text = Communication.ToString("X"); }));
#endif
                    if (Communication < 4)
                    {
                        lineage.CommunicationAddress = Communication;
                        TerminateLineage(lineage);
                        Invoke(new MethodInvoker(delegate ()
                        {
                            label_Status.Text = "Communication Error";
                        }));
                    }
                    else
                    {
                        string CM01_PATH = Path.Combine(Environment.CurrentDirectory, Import.CM01_DLL_NAME);
                        int result = Import.OpenPS(lineage.Lineage.Id ^ 0x9993, CM01_PATH);
                        if (result < 0)
                        {
                            TerminateLineage(lineage);
                            Invoke(new MethodInvoker(delegate ()
                            {
                                label_Status.Text = "게임 실행에 실패했습니다. error:" + result;
                            }));
                        }

                    }
                }
                lock (LineagesLockObj)
                    if (!Lineages.ContainsKey(lineage.Lineage.Id)) //여기서 이미 존재하면 뭐가 잘못됐다고 보면 된다.
                    {
                        Lineages.Add(lineage.Lineage.Id, lineage); //시작했으면 딕셔너리에 추가
                        Invoke(new MethodInvoker(delegate ()
                        {
                            label_Status.Text = "게임을 실행했습니다.";
                        }));
                    }
                
            }
            Invoke(new MethodInvoker(delegate ()
            {
                pictureBox_GameStart.Enabled = true; //게임시작 버튼 다시 활성화
                Thread.Sleep(1000);
                windowmode.HideDXWND();
            }));
        }





















        private void TerminateLineage(LineageProcess lineage)
        {
            lineage.TerminateLineage();
            lineage.Dispose();
            Invoke(new MethodInvoker(delegate ()
            {
                label_Status.Text = "게임 실행에 실패했습니다.";
            }));
        }

        private bool KillBadProcess(Process p)
        {
            if (p == null || BadProcessList == null || BadProcessList.Length == 0) return false;
            for (int j = 0; j < BadProcessList.Length; j++)
            {
                if (p.ProcessName.ToLower().Contains(BadProcessList[j]) || p.MainWindowTitle.ToLower().Contains(BadProcessList[j]))
                {
                    IntPtr h = Import.OpenProcess(0x1FFFFF, false, p.Id);
                    if (h == IntPtr.Zero) continue;
                    Import.TerminateProcess(h, 0);
                    Import.CloseHandle(h);
                    return true; //true면 나쁜 프로세스로 확인되어서 죽였다는 뜻
                }
            }
            return false;
        }

        private List<Process> GetLineageProcessList(bool TerminateBadassholeProcess = true)
        {
            List<Process> LineageProcesses = new List<Process>();
            Process[] p = Process.GetProcesses();
            for (int i = 0; i < p.Length; i++)
            {
                string FileName = null;
                Process lin = p[i];
                bool isWOW64 = false;
                if (TerminateBadassholeProcess && KillBadProcess(lin) == true) { lin.Dispose();  continue; } //true면 리니지가 아니라 BadProcess에 해당하므로 더 안봐도 되므로 continue;
                if (lin.ProcessName.ToLower() != START_FILE_NAME) { lin.Dispose(); continue; }
                try
                {
                    if (Import.IsWow64Process(lin.Handle, out isWOW64) == 0) { lin.Dispose(); continue; }
                    if (!isWOW64) { lin.Dispose(); continue; }
                    if (lin.MainModule == null) { lin.Dispose(); continue; }
                    FileName = Path.GetFileName(lin.MainModule.FileName);
                }
                catch { continue; }
                if (FileName != null && FileName.ToLower() == START_FILE_NAME)
                {
                    LineageProcesses.Add(lin);
                    p[i] = null;
                }
            }
            return LineageProcesses;
        }

        private Process FindNewLineage() //이미 종료된 리니지는 딕셔너리에서 지우고 새로운 리니지는 리턴해준다
        {
            List<Process> linporcesses = GetLineageProcessList();
            Process NewProcess = null;
            for (int i = 0; i < linporcesses.Count; i++) //켜져있는 모든 리니지 리스트를 불러온다.
            {
                Process p = linporcesses[i];
                bool Found = false;
                lock (LineagesLockObj)
                    foreach (KeyValuePair<int, LineageProcess> lineages in Lineages) //현재 접속기 내 메모리에 존재하는 리니지를 가져와서 비교
                        if (p.Id == lineages.Key) { Found = true; break; } //메모리에 존재하고있다? 찾은거다
                if (!Found)
                {
                    NewProcess = p; //딕셔너리 뒤져봤을 때 없다는 것은 새로운 프로세스라는 의미
                    break;
                }
            }

            return NewProcess;
        }

        private void RemoveExitedLineage()
        {
            List<int> ExitList = new List<int>();
            List<Process> linprocesses = null;
            try
            { 
                linprocesses = GetLineageProcessList();
            }
            catch
            {
                return;
            }
            if (linprocesses != null)
            {
                lock (LineagesLockObj)
                    foreach (KeyValuePair<int, LineageProcess> lineages in Lineages) //켜졌다고 여겨지는 리니지 목록을 살펴본다.
                    {
                        bool Found = false;
                        try
                        {
                            for (int i = 0; i < linprocesses.Count; i++)
                            {
                                Process p = linprocesses[i];
                                if (p.Id == lineages.Key) { Found = true; break; } //현재 프로세스 목록에 존재하는 리니지와, 접속기 내부 메모리에 존재하는 리니지와 비교했을 때 일치한다? Found = true
                            }
                        } catch { continue; }
                        if (!Found) //이 경우는 리니지가 종료된 것
                            ExitList.Add(lineages.Key);
                    }

                for (int i = 0; i < ExitList.Count; i++) //이미 종료된 리니지가 메모리에 남아있다면 지워준다.
                    lock (LineagesLockObj)
                        if (Lineages.ContainsKey(ExitList[i]))
                            Lineages.Remove(ExitList[i]);
            }
        }


        //관리자 권한 실행 확인
        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            if (null != identity)
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }

        //별 쓸모 없는 함수, 과거에 만들어놨던 잔재.
        private void ForceExit()
        {
            this.Close();

            //Application.ExitThread();
            //Application.Exit();
            //Environment.Exit(0); //이거 강제로 호출하면 익셉션 날 확률 있음

        }

        //오직 ProcessInformationFromPaymentServer에서만 쓰임
        private void AddOnlyOneDownloadComboboxError(string msg)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                if (comboBox_DownloadLink.Items.Count > 0)
                {
                    comboBox_DownloadLink.Items.Clear();
                    comboBox_DownloadLink.Items.Add(msg);
                    comboBox_DownloadLink.SelectedIndex = 0;
                }
                else
                {
                    comboBox_DownloadLink.Items.Add(msg);
                    comboBox_DownloadLink.SelectedIndex = 0;
                }
            }));
        }

        //폼 꺼질때 꺼진다고 플래그 바꿔주는 함수. ProcessThread 함수 안전 종료를 위한 장치도 포함
        private void LineageConnector_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormClosingFlag == false) //이게 false인 케이스는 관리자 권한으로 실행이 안된 케이스 뿐
            {
                FormClosingFlag = true;
                if (Lineages.Count > 0 && UseLauncherSecurity != null && UseLauncherSecurity.Length < 6 && UseLauncherSecurity.ToLower().Contains("used"))
                    foreach (KeyValuePair<int, LineageProcess> lineages in Lineages)
                        lineages.Value.TerminateLineage(); //접속기 끄면 게임도 같이 뒤진다.   by. cm01    2022-01-13
                                                         //접속기를 강제 종료할 경우 FormClosing 이벤트가 실행되지 않는데 이 경우는 cm01.dll 에서 잘 처리해준다ㅋ
                if (windowmode != null)
                    windowmode.ExitDXWND(); //접속기가 죽을 때는 DXWND도 같이 죽도록
            }
        }

        //게임 스타트 버튼 클릭할 경우 취할 행동
        private void pictureBox_GameStart_Click(object sender, EventArgs e)
        {
            if(!Inited)
            {
                label_Status.Text = "접속기 로딩을 완료하기 전에 시작할 수 없습니다.";
                return;
            }

            if(!File.Exists(START_FILE_NAME))
            {
                if (comboBox_DownloadLink.SelectedItem == null || comboBox_DownloadLink.SelectedIndex < 0 || DownloadLinkList == null || DownloadLinkList.Count == 0)
                {
                    label_Status.Text = "다운로드 서버를 선택해주세요.";
                    return;
                }
                label_Status.Text = "다운로드를 시작합니다..";
                string SelectedDownloadLink = DownloadLinkList[comboBox_DownloadLink.SelectedIndex];
                if (!downloader.DownloadClient(SelectedDownloadLink, ClientZipName))
                {
                    label_Status.Text = "알 수 없는 링크입니다. 브라우저를 통해 직접 다운로드 해주세요.";
                    if (UnshowInnerBroswer)
                        webBrowser1.Navigate(SelectedDownloadLink);
                    else
                        RunExternalBroswer(SelectedDownloadLink);
                }
                return;
            }

            if (StartLineageFlag == false)
            {
                if (windowmode == null) { MessageBox.Show("접속기 실행 과정에서 문제가 발생했습니다."); return; }
                if (checkBox_WindowMode.Checked) //창모드 설정
                {
                    string[] XY = new string[2] { "800", "600" };
                    if (comboBox_해상도.Items.Count > 0 && comboBox_해상도.SelectedIndex > 0)
                    {
                        string[] TempXY = comboBox_해상도.SelectedItem.ToString().Split('*');
                        if (TempXY != null && TempXY.Length > 1) { XY[0] = TempXY[0].Trim(' '); XY[1] = TempXY[1].Trim(' '); }
                    }

                    if (windowmode.EditINIStringForLineage(SERVER_ADDRESS, SERVER_PORT, Path.Combine(Environment.CurrentDirectory, START_FILE_NAME), XY[0], XY[1], WINDOW_MODE_TITLE_NAME))
                    {
                        if (!windowmode.StartDXWND())
                            MessageBox.Show("창모드 설정에 실패했습니다. (1)파일을 찾을 수 없습니다.");
                    }
                    else MessageBox.Show("창모드 설정에 실패했습니다. (2)설정 파일 수정에 실패했습니다.");
                }
                else windowmode.ExitDXWND();

                var loc = pictureBox_GameStart.Location;
                var newLoc = new Point(loc.X + 6, loc.Y + 6);
                pictureBox_GameStart.Location = newLoc;
                StartLineageFlag = true; //스레드에 게임 시작하라고 명령
                label_Status.Text = "게임 실행을 시도합니다..";
                pictureBox_GameStart.Enabled = false;
            }
            else label_Status.Text = "게임 실행을 이미 시도중입니다..";
        }

        //웹브라우저 스크롤 컨트롤하는 함수
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            webBrowser1.Document.Window.ScrollTo(Homepage_Start_Scroll_Pos.X, Homepage_Start_Scroll_Pos.Y); //웹브라우저의 스크롤을 좌측 상단 좌표 (0,0)으로 이동
        }

        private void checkBox_WindowMode_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_WindowMode.Checked == false && windowmode != null) { comboBox_해상도.Enabled = false; windowmode.ExitDXWND(); }
            else comboBox_해상도.Enabled = true;
        }


        //다운로드할 때 프로그래스바 관련 함수 -> 다운 완료되면 압축 해제까지 해준다.
        private async void ProgressBarChanged(long Max, long Current, string[] FileNames = null)
        {
            if (Max != 0 && Max == Current)
            {
                label_Status.Text = "압축을 해제합니다..";
                if (FileNames == null || FileNames.Length == 0 || FileNames[0] == null || FileNames[0].Length == 0)
                    label_Status.Text = "다운로드 한 파일을 찾을 수 없습니다.";
                await Task.Run(() =>
                {
                    if (File.Exists(FileNames[0]))
                        if(zip.Unzip(FileNames[0]))
                            for (int i = 0; i < FileNames.Length; i++)
                                File.Delete(FileNames[i]); //다 깔았으니까 압축파일은 지워주자!
                    Invoke(new MethodInvoker(delegate () { label_Status.Text = "다운로드가 완료되었습니다. 실행해주세요!"; pictureBox_GameStart.Enabled = true; }));
                    
                });
            }
            else
            {
                long _staticMB = staticMB;
                if (Max < staticMB) _staticMB = 1;
                string sMax = (Math.Round((double)Max / (double)_staticMB, 3)).ToString();
                string sCurrent = (Math.Round((double)Current / (double)_staticMB, 3)).ToString();
                if (Max >= staticMB)
                    label_Downloaded.Text = sCurrent + "MB / " + sMax + "MB";
                else
                    label_Downloaded.Text = sCurrent + " / " + sMax;
            }
        }

        private void button_MoveToHomePage_Click(object sender, EventArgs e)
        {
            //이미지로 바꾸고 싶은가? 그럼 알아서 바꾸셈. 디자인은 내 분야가 아님니다   -   cm01
            RunExternalBroswer(HOMEPAGE_ADDRESS);
        }

        //외부 브라우저를 통해 링크로 들어가는 함수
        private void RunExternalBroswer(string pageaddress)
        {
            try //구현 개 쓰레기같이 try~catch로 해놨음. because 뇌에서 생각이란거를 하고싶지 않기 때문.   - 2022-01-10   cm01
            {
                Process.Start("chrome.exe", pageaddress);
            }
            catch
            {
                try
                {
                    Process.Start("msedge.exe", pageaddress);
                }
                catch
                {
                    Process.Start("iexplore.exe", pageaddress);
                }
            }
        }

        private void DeleteGarbageFile() //Resources로 부터 풀어진 파일들 삭제
        {
            if (File.Exists(Path.Combine(DXWND_PATH, DXWND_NAME)))
                File.Delete(Path.Combine(DXWND_PATH, DXWND_NAME));
            if(File.Exists(Path.Combine(DXWND_PATH, "dxwnd.ini")))
                File.Delete(Path.Combine(DXWND_PATH, "dxwnd.ini"));
            if (File.Exists(Path.Combine(DXWND_PATH, "dxwnd.dll")))
                File.Delete(Path.Combine(DXWND_PATH, "dxwnd.dll"));
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, Import.CM01_DLL_NAME)))
                File.Delete(Path.Combine(Environment.CurrentDirectory, Import.CM01_DLL_NAME));
        }
        
        //결제 폼 실행
        private void button_Payment_Click(object sender, EventArgs e)
        {
            PaymentForm paymentform = new PaymentForm(paymentserver);
            paymentform.ShowDialog();
        }
    }
}
