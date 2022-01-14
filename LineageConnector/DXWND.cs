using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Resources;

namespace LineageConnector
{
    public class DXWND
    {
        private string DXWND_PATH = ""; //exe 파일의 경로는 포함하지 않음
        private string DXWND_NAME = "dxwnd.exe"; //기본값
        private string DXWND_CONFIG = "dxwnd.ini";

        private Process DXWND_PROCESS = null;

        public DXWND(string Path, string FileName = "dxwnd.exe", string ConfigName = "dxwnd.ini")
        {
            this.DXWND_NAME = FileName;
            this.DXWND_CONFIG = ConfigName;
            this.DXWND_PATH = Path;
        }


        public bool StartDXWND()
        {
            Process[] ExistProcess = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(DXWND_NAME));
            if (ExistProcess == null || ExistProcess.Length == 0) //꺼져있으면 실행
            {
                if (!File.Exists(Path.Combine(DXWND_PATH, DXWND_NAME))) return false;

                ProcessStartInfo info = new ProcessStartInfo(Path.Combine(DXWND_PATH, DXWND_NAME));
                info.CreateNoWindow = true;
                info.WorkingDirectory = DXWND_PATH;
                info.UseShellExecute = false;
#if _DEBUG
#else
                info.WindowStyle = ProcessWindowStyle.Hidden;
#endif
                DXWND_PROCESS = Process.Start(info);
            }
            else DXWND_PROCESS = ExistProcess[0];
            return true;
        }

        public bool ExitDXWND()
        {
            bool result = false;
            Process[] ExistProcess = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(DXWND_NAME));
            if (ExistProcess != null && ExistProcess.Length > 0 && DXWND_PROCESS != null)
            {
                IntPtr hProcess = Import.OpenProcess(Import.PROCESS_TERMINATE, false, DXWND_PROCESS.Id);
                if(hProcess != IntPtr.Zero)
                {
                    if (Import.TerminateProcess(hProcess, 0) != 0)
                        result = true;
                    Import.CloseHandle(hProcess);
                }
            }

            return result;
        }

        private string ReadINI()
        {
            string inipath = Path.Combine(DXWND_PATH, DXWND_CONFIG);
            if (!File.Exists(inipath)) return "";
            string s = File.ReadAllText(Path.Combine(DXWND_PATH, DXWND_CONFIG));
            return s;
        }

        public bool EditINIStringForLineage(string IP, int PORT, string LineageFullPath, string SizeX, string SizeY, string TitleName = "lineage")
        {
            string INIString = ReadINI();
            if (INIString == null || INIString == "") return false;

            const string exepath_key = "exepath";
            const string path0_key = "path0";
            string inipath = Path.Combine(DXWND_PATH, DXWND_CONFIG);
            string[] sss = INIString.Split('\n');
            StringBuilder sb = new StringBuilder();
            if (sss != null && sss.Length > 0)
            {
                for (int i = 0; i < sss.Length; i++)
                {
                    string s = sss[i];
                    if (s == null || s.Length == 0) continue;
                    s = s.Replace("\r", "");
                    string[] attribute = s.Split('=');
                    if (attribute == null) continue;
                    if (attribute.Length > 1)
                    {
                        string Key = attribute[0];
                        string Val = attribute[1];
                        if (Key == exepath_key)
                            Val = Path.GetDirectoryName(LineageFullPath);
                        else if (Key == path0_key)
                            Val = LineageFullPath; //+ " " + IP + " " + PORT;
                        else if (Key == "sizx0" || Key == "initresw0")
                            Val = SizeX;
                        else if (Key == "sizy0" || Key == "initresh0")
                            Val = SizeY;
                        else if (Key == "title0")
                            Val = TitleName;
                        else if (Key == "cmdline0")
                            Val = Path.GetFileName(LineageFullPath) + " " + IP + " " + PORT;
                        sss[i] = Key + "=" + Val;
                    }
                    sb.Append(sss[i] + "\n");
                }
            }

            try
            {
                File.WriteAllText(inipath, sb.ToString());
            }
            catch
            {
                throw new Exception("창모드 설정에 실패했습니다.");
                return false;
            }
            return true;
        }

        public int RunProcessFromDXWND(int StartDelay = 550)
        {
            if (DXWND_PROCESS == null) return -100;
            return Import.CreateProcessThroughDXWND(DXWND_PROCESS.Handle, StartDelay);
        }




    }
}
