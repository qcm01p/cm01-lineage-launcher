using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineageConnector
{
    public partial class PaymentForm : Form
    {
        private const string PaymentSaveFileName = "paymentsave";
        PaymentServerRequest paymentserver = null;
        bool useSSL = false;


        public PaymentForm(PaymentServerRequest paymentserver)
        {
            InitializeComponent();
            this.paymentserver = paymentserver;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            LoadPaymentInfoFromFile();
        }



        private async void button_RequestPayment_Click(object sender, EventArgs e)
        {
            if (!파라미터_검사함수()) return; //인자 검사했는데 뭔가 이상하다? 그냥 함수 진행 안한다.


            EnabledRequestControl(false); //여기까지 왔으면 결제 시도하기 위해 컨트롤 다 잠군다.
            label_Status.Text = "결제를 요청하는 중입니다. (30초 이내 소요)";

            string result = "";
            string api_server_wallet = "";
            await Task.Run(() =>
            {
                result = paymentserver.RequestPayment(textBox_Account.Text, textBox_CharName.Text, textBox_결제금액.Text, textBox_PaymentPassword.Text, textBox_billsender.Text, useSSL);
            });
            if(result != null && result == "err")
            {
                MessageBox.Show("결제 서버가 닫힌 것 같습니다.");
                label_Status.Text = "결제 요청에 실패했습니다.";
                EnabledRequestControl(true);
                return;
            }
            if (result == null || result.Length < 7)
            {
                if (result == null) result = "";
                MessageBox.Show("결제 요청에 실패했습니다.\n" + result);
                label_Status.Text = "결제 요청에 실패했습니다.";
                EnabledRequestControl(true);
                return;
            }

            //이 부분 PaymentServer 버전에 따라 좀 달라서 하드코딩 빡세게 해놨다;;
            result = result.Replace("<br>", "^");
            string[] StringsFromServer = result.Split('^');

            //이 경우는 PaymentServer가 2022-01-10 이전의 구버전이라서 API 서버의 지갑주소를 안주는 것 ㅡㅡ;
            if (StringsFromServer.Length < 3 || (StringsFromServer.Length == 3 && StringsFromServer[2] != null && StringsFromServer[2].Length < 2))
            {
                api_server_wallet = GetAPIServerWallet();
                if (api_server_wallet == null || api_server_wallet.Length < 7)
                    return;
            }
            else //신버전은 <br>이 두개 이상이라 StringsFromServer.Length == 3이다.
            {
                if (StringsFromServer[2] == null || StringsFromServer[2].Length < 10)
                {
                    api_server_wallet = GetAPIServerWallet();
                    if (api_server_wallet == null || api_server_wallet.Length < 7)
                        return;
                }
                if (api_server_wallet == null || api_server_wallet.Length < 10)
                {
                    int TempIndex = StringsFromServer[2].IndexOf(" 주소로"); //신버전 기준
                    api_server_wallet = StringsFromServer[2].Substring(0, TempIndex).Trim(' ');
                }
            }

            result = result.Replace("^", "\n");
            if (api_server_wallet == null || api_server_wallet.Length < 10)
            {
                AddStatus("결제 API 서버의 지갑 주소를 가져오는데에 실패했습니다.\n" + result);
                return;
            }

            string result_ToLower = result.ToLower();
            if (result_ToLower.Contains("success"))
                AddStatus(string.Format("게임에서 반드시 로그아웃 하신 뒤 {0} 주소로 {1} TRX를 송금해주세요.\n송금을 완료하신 뒤 TxID를 입력하시면 됩니다.", api_server_wallet, textBox_결제금액.Text));
            else if(result.Contains("error"))
                AddStatus(string.Format("결제 요청을 받아들일 수 없습니다. 일회용 결제 비밀번호나 계정을 확인해보세요."));
            else
                AddStatus(result);
            EnabledStartPaymentControl(true);
            if (checkBox_Save.Checked) SavePaymentInfoToFile();
        }

        private void button_StartPayment_Click(object sender, EventArgs e)
        {
            if (!파라미터_검사함수() || !피라미터_검사함수2()) return;
           
            string result = paymentserver.StartPayment(textBox_Account.Text, textBox_CharName.Text, textBox_결제금액.Text, textBox_PaymentPassword.Text, textBox_TxID.Text, useSSL);
            if (result != null && result == "err")
            {
                MessageBox.Show("결제 서버가 닫힌 것 같습니다.\n결제에 실패했습니다.\n운영자에게 문의해주세요.");
                label_Status.Text = "결제를 완료할 수 없습니다. 운영자에게 문의해주세요.";
                AddStatus(label_Status.Text);
                EnabledRequestControl(true);
                return;
            }

            if(result == null || result.Length < 7)
            {
                MessageBox.Show("결제를 완료할 수 없습니다.\n결제에 실패했습니다.\n운영자에게 문의해주세요.");
                label_Status.Text = "결제를 완료할 수 없습니다. 운영자에게 문의해주세요.";
                AddStatus(label_Status.Text);
                EnabledRequestControl(true);
                return;
            }

            string result_ToLower = result.ToLower();
            if (result_ToLower.Contains("success"))
            {
                label_Status.Text = "결제에 성공했습니다.";
                EnabledRequestControl(true);
                EnabledStartPaymentControl(false);
            }
            else if(result.Contains("error"))
            {
                label_Status.Text = "결제에 실패한 것 같습니다. TxID를 확인해주세요.";
            }
            AddStatus(result);
        }



        private string GetAPIServerWallet()
        {
            var api_server_wallet = paymentserver.GetAPIServerWalletAddress();
            if (api_server_wallet == null || api_server_wallet.Length < 7)
            {
                MessageBox.Show("결제 API 서버의 지갑 주소를 가져오는데에 실패했습니다.\n" + api_server_wallet);
                AddStatus("결제 API 서버의 지갑 주소를 가져오는데에 실패했습니다.\n" + api_server_wallet);
                label_Status.Text = "결제 API 서버의 지갑 주소를 가져오는데에 실패했습니다.";
                EnabledRequestControl(true);
                return "";
            }
            string tempstr = "ServerWalletAddress\":\"";
            int WalletIndex = api_server_wallet.IndexOf(tempstr) + tempstr.Length;
            int DoublequoteIndex = api_server_wallet.IndexOf('\"', WalletIndex + 1);
            api_server_wallet = api_server_wallet.Substring(WalletIndex, DoublequoteIndex - WalletIndex).Replace("\"", "");
            return api_server_wallet;
        }

        private bool 피라미터_검사함수2() //결제 완료 절차에서 사용 (StartPayment 버튼)
        {
            if (textBox_TxID.Text.Length < 8 || textBox_TxID.Text.Length > 512)
            {
                MessageBox.Show("TxID (트랜잭션 ID)를 확인해주세요.");
                return false;
            }
            textBox_TxID.Text = textBox_TxID.Text.Trim(' ');
            if (isExistSpecialChar(textBox_TxID.Text))
            {
                MessageBox.Show("TxID (트랜잭션 ID)에는 특수문자가 들어갈 수 없습니다!");
                return false;
            }
            return true;
        }
        private bool 파라미터_검사함수(bool Check결제금액 = true)
        {
            if (textBox_CharName.Text.Length == 0)
            {
                MessageBox.Show("캐릭명을 적어주세요.");
                return false;
            }
            if (textBox_Account.Text.Length == 0)
            {
                MessageBox.Show("계정명을 적어주세요. 계정명은 로그인할 때 사용하는 ID입니다.");
                return false;
            }

            if (Check결제금액)
            {
                double TRX = 0;
                if (!double.TryParse(textBox_결제금액.Text, out TRX))
                {
                    MessageBox.Show("결제 금액을 확인해주세요.");
                    return false;
                }
                if (TRX <= 2.1)
                {
                    MessageBox.Show("결제 금액이 너무 낮습니다.");
                    return false;
                }
            }
            if (textBox_PaymentPassword.Text.Length > 16 || textBox_PaymentPassword.Text.Length == 0)
            {
                MessageBox.Show("일회용 결제 비밀번호는 1~16자 이내의 영어 또는 숫자의 혼합으로만 써야합니다.");
                return false;
            }
            if (isExistSpecialChar(textBox_PaymentPassword.Text))
            {
                MessageBox.Show("일회용 결제 비밀번호에는 특수문자를 사용할 수 없습니다.");
                return false;
            }

            textBox_billsender.Text = textBox_billsender.Text.Trim(' ');
            if (textBox_billsender.Text.Length < 8 || textBox_billsender.Text.Length > 512)
            {
                MessageBox.Show("지갑 주소를 정확하게 적어주주세요.");
                return false;
            }

            if (isExistSpecialChar(textBox_billsender.Text))
            {
                MessageBox.Show("지갑 주소에는 특수문자를 사용할 수 없습니다.");
                return false;
            }
            return true;
        }
        private static bool isExistSpecialChar(string msg)
        {
            string str = @"[~!@\#$%^&*\()\=+|\\/:;?""<>']";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(str);
            return rex.IsMatch(msg);
        }
        //문제가 발생했을 때 긴 로그를 찍기 위해 사용
        private void AddStatus(string msg)
        {
            textBox_Status.Text = msg;
        }

        private void EnabledRequestControl(bool flag)
        {
            textBox_billsender.Enabled = flag;
            textBox_PaymentPassword.Enabled = flag;
            textBox_Account.Enabled = flag;
            textBox_CharName.Enabled = flag;
            textBox_결제금액.Enabled = flag;
            button_RequestPayment.Enabled = flag;
        }

        private void EnabledStartPaymentControl(bool flag)
        {
            textBox_TxID.Enabled = flag;
            textBox_TxID.Visible = flag;
            label_TxID.Visible = flag;
            button_StartPayment.Enabled = flag;
            button_StartPayment.Visible = flag;
            groupBox_StartPayment.Enabled = flag;
            groupBox_StartPayment.Visible = flag;
        }

        private void checkBox_Save_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Save.Checked == false)
            {
                try
                {
                    if (File.Exists(PaymentSaveFileName))
                        File.Delete(PaymentSaveFileName);
                }
                catch
                {
                    label_Status.Text = "결제 정보 저장 파일을 지우는데에 실패했습니다.";
                }
            }
            else
            {
                SavePaymentInfoToFile();
            }
        }

        private void SavePaymentInfoToFile()
        {
            if (!File.Exists(PaymentSaveFileName))
                File.Create(PaymentSaveFileName).Dispose();

            //결제 금액은 저장하지 않는다.

            if (파라미터_검사함수(false))
                File.WriteAllText(PaymentSaveFileName, string.Format("{0},{1},{2},{3}", textBox_Account.Text, textBox_CharName.Text, textBox_billsender.Text, textBox_PaymentPassword.Text));
        }

        private void LoadPaymentInfoFromFile()
        {
            if (File.Exists(PaymentSaveFileName))
            {
                string s = File.ReadAllText(PaymentSaveFileName);
                if (s == null || s.Length < 3) return;
                string[] content = s.Split(',');
                if (content.Length < 4) return;

                textBox_Account.Text = content[0];
                textBox_CharName.Text = content[1];
                textBox_billsender.Text = content[2];
                textBox_PaymentPassword.Text = content[3];
            }
        }
    }
}
