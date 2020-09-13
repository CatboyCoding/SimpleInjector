using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SimpleInjector {
    static class Program {
        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [STAThread]
        static void Main() {
            string sandboxie = "SbieDll";
            bool sanboxieRun = GetModuleHandle(sandboxie) != IntPtr.Zero;

            if (sanboxieRun == true) {
                MessageBox.Show("Please don't run this software in a sandbox.", "Anti-Sandbox Tripped.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (VirtualMachineDetector.Assert()) {
                MessageBox.Show("Please don't run this software in a VM.", "Anti-VM Tripped.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
