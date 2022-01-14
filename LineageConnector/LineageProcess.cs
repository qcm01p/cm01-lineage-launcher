using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace LineageConnector
{
    public class LineageProcess : IDisposable
    {
        private Process _Lineage = null;
        public Process Lineage 
        {
            get { return _Lineage; }
            set { _Lineage = value; _hProcess = Import.OpenProcess(0x1FFFFF, false, value.Id);      }
        }
        public int GetPID { get { if (Lineage != null) return Lineage.Id; else return 0; } }

        private IntPtr _hProcess = IntPtr.Zero;
        public IntPtr hProcess { get { return _hProcess; } }
        public int CommunicationAddress { get; set; }

        public void TerminateLineage()
        {
            if (_hProcess != IntPtr.Zero)
                Import.TerminateProcess(_hProcess, 0);
        }

        public void Dispose()
        {
            if (_hProcess != IntPtr.Zero)
                Import.CloseHandle(hProcess);
            if (_Lineage != null)
                _Lineage.Dispose();
        }
    }
}
