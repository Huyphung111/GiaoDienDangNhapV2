using System;
using System.Windows.Forms;
using OfficeOpenXml;

namespace GiaoDienDangNhap
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // EPPlus 5.8.0 license setup
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GiaoDien());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
