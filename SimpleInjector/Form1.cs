using SimpleInjector.gaylibrary;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SimpleInjector {
    public partial class Form1 : Form {
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);

        public static bool Is64Bit(Process process) {
            if (!Environment.Is64BitOperatingSystem)
                return false;

            bool isWow64;
            if (!IsWow64Process(process.Handle, out isWow64))
                throw new Win32Exception();
            return !isWow64;
        }

        public List<string> dlls = new List<string>();
        Process target = Process.GetCurrentProcess();

        public static Color ColorFromHex(string Hex) {
            return Color.FromArgb(checked((int)long.Parse(string.Format("FFFFFFFFFF{0}", Hex.Substring(1)), NumberStyles.HexNumber)));
        }

        public Form1() {
            InitializeComponent();

            Injector.Tag = "\uF0E7";
            Settings.Tag = "\uF013";
            Debug.Tag = "\uF188";

            comboBox1.SelectedIndex = 1;
            xylosTabControl1.SelectedIndex = 1;

            Color clr = ColorFromHex("#202020");
            PrivateFontCollection pfc = new PrivateFontCollection();
            byte[] fontBytes = Properties.Resources.fa_regular_400;

            var fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);

            pfc.AddMemoryFont(fontData, fontBytes.Length);
            using (Bitmap bmp = new Bitmap(16, 16)) {
                using (Font fa = new Font(pfc.Families[0], 16, FontStyle.Regular, GraphicsUnit.Pixel)) {
                    using (Graphics g = Graphics.FromImage(bmp)) {
                        g.TextRenderingHint = TextRenderingHint.AntiAlias;
                        using (Brush b = new SolidBrush(clr)) {
                            g.DrawString(Conversions.ToString(Injector.Tag), fa, b, 0, 0);
                        }
                        g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    }
                }
                Icon = Icon.FromHandle(bmp.GetHicon());
            }
        }

        private void Form1_Load(object sender, EventArgs e) {

        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            discord.join("cTy5FAn");
        }

        private void button1_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            foreach (string f in openFileDialog1.FileNames) {
                dlls.Add(f);
                listBox1.Items.Add(Path.GetFileName(f));
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (listBox1.SelectedIndex < 0) return;
            dlls.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button4_Click(object sender, EventArgs e) {
            listBox1.Items.Clear();
            dlls.Clear();
        }

        private void button5_Click(object sender, EventArgs e) {
            if (target.HasExited || !target.Responding) {
                MessageBox.Show("Selected process has ended.", "Invalid process selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            } else if (target.Id == Process.GetCurrentProcess().Id) {
                MessageBox.Show("Please select a valid process.", "Invalid process selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            switch (comboBox1.SelectedIndex) {
                case 0:
                    foreach (string dll in dlls) {
                        LoadLibrary.Inject((uint)target.Id, dll);
                    }
                    break;
                case 1:
                    ManualMapInjector manualmap = new ManualMapInjector(target);
                    manualmap.AsyncInjection = checkEdit1.Checked;
                    foreach (string dll in dlls) {
                        IntPtr result = manualmap.Inject(File.ReadAllBytes(dll));
                        if (result == IntPtr.Zero) {
                            MessageBox.Show($"Failed to inject {Path.GetFileName(dll)}.");
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e) {
            Process p = Process.GetProcesses()[comboBoxEdit1.SelectedIndex];
            label6.Text = p.Id.ToString();
            target = p;
        }

        private void comboBoxEdit1_MouseDown(object sender, MouseEventArgs e) {
            comboBoxEdit1.Items.Clear();
            comboBoxEdit1.Items.AddRange(Process.GetProcesses().Select(p => p.ProcessName).ToArray());
        }

        private void tabPage1_Click(object sender, EventArgs e) {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e) {
            Process.Start("https://m.do.co/c/046c464ac2ca");
        }
    }
}