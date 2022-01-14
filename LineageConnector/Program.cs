using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LineageConnector
{
    static class Program
    {

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (IsExistProcessMutex())
            {
                //System.Windows.Forms.MessageBox.Show("접속기는 하나만 실행할 수 있습니다.");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new LineageConnector());
            }
        }


        //코드 수준 개 박살났는데 크게 문제는 없을듯
        static bool IsExistProcessMutex() //https://sosopro.tistory.com/130 블로그 펌.     by.cm01     2022-01-13
        {
            const string MutextName = "cm01_lineage_launcher_2022";
            bool createdNew;
            // createdNew  : processName로 이미 명명된 뮤텍스가 있을경우 false반환 
            // 신규인경우는 true를반환한다. 
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, MutextName, out createdNew);
            if (createdNew == true)
                return false;
            else
            {
                System.Threading.Thread.Sleep(1750); //1.75초만 기다리고 다시 확인해본다.
                mutex = new System.Threading.Mutex(true, MutextName, out createdNew);
                if (createdNew == true)
                    return false;
                else
                    return true;
            }
        }
    }
}
