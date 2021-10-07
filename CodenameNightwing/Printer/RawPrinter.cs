using CodenameNightwing.Config;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CodenameNightwing.Printer
{
    public class RawPrinter
    {
        // Structure and API declarations:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)]string szPrinter, ref IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In(), MarshalAs(UnmanagedType.LPStruct)]DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, ref Int32 dwWritten);

        [DllImport("winspool.Drv", EntryPoint = "ReadPrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ReadPrinter(IntPtr hPrinter, [MarshalAs(UnmanagedType.LPStr)] StringBuilder pBytes, Int32 dwCount, out Int32 dwNReadBytes);


        private IntPtr hPrinter = new IntPtr(0);
        private DOCINFOA di = new DOCINFOA();

        public RawPrinter()
        {

        }

        public RawPrinter(IntPtr hPrinter)
        {
            this.hPrinter = hPrinter;
            PrinterOpen = true;
        }


        public bool PrinterOpen = false;
        public bool PrinterIsOpen
        {
            get { return PrinterOpen; }
        }


        public string ReadFromPrinter()
        {

            string reaDstrinG = "";
            Int32 buf = 0;
            Int32 pcRead;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool read = ReadPrinter(hPrinter, sb.Append(reaDstrinG), buf, out pcRead);
            return sb.ToString();

        }

        public bool OpenPrint(string szPrinterName)
        {
            if (PrinterOpen == false)
            {
                di.pDocName = ".NET RAW Document";
                di.pDataType = "RAW";
                if (OpenPrinter(szPrinterName.Normalize(), ref hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        if (StartPagePrinter(hPrinter))
                        {
                            PrinterOpen = true;
                        }
                    }
                }
            }
            return PrinterOpen;
        }

        public void ClosePrint()
        {
            if (PrinterOpen)
            {
                EndPagePrinter(hPrinter);
                EndDocPrinter(hPrinter);
                ClosePrinter(hPrinter);
                PrinterOpen = false;
            }
        }

        public bool SendStringToPrinter(string szPrinterName, string szString)
        {
            bool functionReturnValue = false;
            if (PrinterOpen)
            {
                IntPtr pBytes = default(IntPtr);
                Int32 dwCount = default(Int32);
                Int32 dwWritten = 0;
                dwCount = szString.Length;
                pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                functionReturnValue = WritePrinter(hPrinter, pBytes, dwCount, ref dwWritten);

                if (dwCount != dwWritten)
                {
                    functionReturnValue = false;
                }

                Marshal.FreeCoTaskMem(pBytes);
            }
            else
            {
                functionReturnValue = false;
            }
            return functionReturnValue;
        }
    }
}
