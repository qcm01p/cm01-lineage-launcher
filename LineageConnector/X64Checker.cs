using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LineageConnector
{
    public class X64Checker
    {
        public enum x64checker_contents
        {
            NtWVM,
            NtRVM,
            NtOP,
            Process32Next
        }
        private const string x64CheckerName = "x64Checker";
        Zip zip = null;

        private string[] checker_text = null;


        public X64Checker(Zip zip)
        {
            this.zip = zip;
        }

        public string GetX64Address(x64checker_contents contents)
        {
            if (checker_text == null || checker_text.Length == 0) return "";
            return checker_text[(int)contents];

        }

        //원래 32비트 프로세스인 접속기에서 64비트 프로세스인 치트엔진류의 모든 프로세스들을 무력화 시키기 위해 사용된다.
        public void RunX64Checker()
        {
            if (!File.Exists(x64CheckerName)) zip.UnzipFromMemory(Properties.Resources.x64Checker);
            string CheckerPath = Path.Combine(Environment.CurrentDirectory, x64CheckerName);
            using (FileStream fs = new FileStream(CheckerPath, FileMode.Open, FileAccess.ReadWrite))
            {
                byte[] MZ = { 0x4D, 0x5A };
                fs.Write(MZ, 0, MZ.Length);
            }
            ProcessStartInfo info = new ProcessStartInfo(CheckerPath);
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            Process p = Process.Start(info);
            p.WaitForExit(2000);
            string txt = p.StandardOutput.ReadToEnd();
            checker_text = txt.Split('\n');
            if (checker_text != null)
                for (int i = 0; i < checker_text.Length; i++)
                    checker_text[i] = checker_text[i].Replace("\r", "");
            if (File.Exists(x64CheckerName)) File.Delete(x64CheckerName);
        }

        public bool ExtractCM01()  //by.cm01             2022-01-14
        {
            try
            {
                if (!File.Exists(Import.CM01_DLL_NAME)) zip.UnzipFromMemory(Properties.Resources.cm01); //cm01.dll 압축 해제 후 파일로 저장
                string CheckerPath = Path.Combine(Environment.CurrentDirectory, Import.CM01_DLL_NAME);
                using (FileStream fs = new FileStream(CheckerPath, FileMode.Open, FileAccess.ReadWrite)) //cm01.dll 헤더 복원
                {
                    byte[] MZ = { 0x4D, 0x5A };
                    fs.Write(MZ, 0, MZ.Length);
                }
                return true;
            }
            catch { return false; }
            return false;
        }








    }
}
