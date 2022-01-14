using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LineageConnector
{
    public class Import
    {
        public const string CM01_DLL_NAME = "cm01.dll";

        [DllImport("kernel32")] public static extern Int32 IsWow64Process(IntPtr hProcess, out Boolean bWow64Process);

        [DllImport("kernel32")]
        public static extern Int32 TerminateProcess(IntPtr hProcess, Int32 ExitCode);
        [DllImport("kernel32")]
        public static extern void CloseHandle(IntPtr hProcess);
        [DllImport("kernel32")]
        public static extern IntPtr OpenProcess(Int32 Access, Boolean InheritHandle, Int32 ProcessId);
        public const Int32 PROCESS_TERMINATE = 0x1;




        [DllImport(CM01_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool InitCM01();
        [DllImport(CM01_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenPS(int pid, [MarshalAs(UnmanagedType.LPWStr)] string DLL_PATH);

        //[DllImport(CM01_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        //public static extern int CreateSuspendedProcess([MarshalAs(UnmanagedType.LPWStr)]string Path, [MarshalAs(UnmanagedType.LPWStr)]string CommandLine);

        [DllImport(CM01_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int CreateProcessThroughDXWND(IntPtr hProcess, int StartDelay);
        //public static extern int CreateProcessThroughDXWND(IntPtr hProcess, [MarshalAs(UnmanagedType.LPStr)] string FullPath, [MarshalAs(UnmanagedType.LPStr)] string DirectoryPath, [MarshalAs(UnmanagedType.LPStr)] string CommandLine, [MarshalAs(UnmanagedType.LPStr)] string title);

        [DllImport(CM01_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetCommunicationAddress(IntPtr hProcess, bool WriteName = true);



    }
}
