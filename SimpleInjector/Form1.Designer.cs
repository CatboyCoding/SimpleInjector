namespace SimpleInjector {
    partial class Form1 {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent() {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.xylosTabControl1 = new XylosTabControl();
            this.MainDivider = new System.Windows.Forms.TabPage();
            this.Injector = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xylosSeparator1 = new XylosSeparator();
            this.simpleButton1 = new XylosButton();
            this.comboBox1 = new XylosCombobox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkEdit1 = new XylosCheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button5 = new XylosButton();
            this.button4 = new XylosButton();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new XylosButton();
            this.comboBoxEdit1 = new XylosCombobox();
            this.button2 = new XylosButton();
            this.MiscDivider = new System.Windows.Forms.TabPage();
            this.Settings = new System.Windows.Forms.TabPage();
            this.Debug = new System.Windows.Forms.TabPage();
            this.debugLogger = new System.Windows.Forms.RichTextBox();
            this.xylosTabControl1.SuspendLayout();
            this.Injector.SuspendLayout();
            this.Debug.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "dll";
            this.openFileDialog1.FileName = "select dll file";
            this.openFileDialog1.Filter = "dll files|*.dll";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // xylosTabControl1
            // 
            this.xylosTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.xylosTabControl1.Controls.Add(this.MainDivider);
            this.xylosTabControl1.Controls.Add(this.Injector);
            this.xylosTabControl1.Controls.Add(this.MiscDivider);
            this.xylosTabControl1.Controls.Add(this.Settings);
            this.xylosTabControl1.Controls.Add(this.Debug);
            this.xylosTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xylosTabControl1.FirstHeaderBorder = true;
            this.xylosTabControl1.ItemSize = new System.Drawing.Size(40, 180);
            this.xylosTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xylosTabControl1.Multiline = true;
            this.xylosTabControl1.Name = "xylosTabControl1";
            this.xylosTabControl1.SelectedIndex = 0;
            this.xylosTabControl1.Size = new System.Drawing.Size(530, 293);
            this.xylosTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.xylosTabControl1.TabIndex = 13;
            // 
            // MainDivider
            // 
            this.MainDivider.BackColor = System.Drawing.Color.White;
            this.MainDivider.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MainDivider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.MainDivider.Location = new System.Drawing.Point(184, 4);
            this.MainDivider.Name = "MainDivider";
            this.MainDivider.Size = new System.Drawing.Size(342, 285);
            this.MainDivider.TabIndex = 3;
            this.MainDivider.Tag = "Divider";
            this.MainDivider.Text = "Main";
            // 
            // Injector
            // 
            this.Injector.BackColor = System.Drawing.Color.White;
            this.Injector.Controls.Add(this.panel1);
            this.Injector.Controls.Add(this.xylosSeparator1);
            this.Injector.Controls.Add(this.simpleButton1);
            this.Injector.Controls.Add(this.comboBox1);
            this.Injector.Controls.Add(this.label3);
            this.Injector.Controls.Add(this.checkEdit1);
            this.Injector.Controls.Add(this.listBox1);
            this.Injector.Controls.Add(this.label1);
            this.Injector.Controls.Add(this.label2);
            this.Injector.Controls.Add(this.button5);
            this.Injector.Controls.Add(this.button4);
            this.Injector.Controls.Add(this.label6);
            this.Injector.Controls.Add(this.button1);
            this.Injector.Controls.Add(this.comboBoxEdit1);
            this.Injector.Controls.Add(this.button2);
            this.Injector.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Injector.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.Injector.Location = new System.Drawing.Point(184, 4);
            this.Injector.Name = "Injector";
            this.Injector.Padding = new System.Windows.Forms.Padding(3);
            this.Injector.Size = new System.Drawing.Size(342, 285);
            this.Injector.TabIndex = 0;
            this.Injector.Tag = "";
            this.Injector.Text = "Injector";
            this.Injector.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::SimpleInjector.Properties.Resources.ad1;
            this.panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(3, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(336, 112);
            this.panel1.TabIndex = 40;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // xylosSeparator1
            // 
            this.xylosSeparator1.Location = new System.Drawing.Point(0, 162);
            this.xylosSeparator1.Name = "xylosSeparator1";
            this.xylosSeparator1.Size = new System.Drawing.Size(376, 2);
            this.xylosSeparator1.TabIndex = 39;
            this.xylosSeparator1.Text = "xylosSeparator1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.EnabledCalc = true;
            this.simpleButton1.Location = new System.Drawing.Point(227, 130);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(112, 26);
            this.simpleButton1.TabIndex = 38;
            this.simpleButton1.TabStop = false;
            this.simpleButton1.Text = "Join Discord";
            this.simpleButton1.Click += new XylosButton.ClickEventHandler(this.simpleButton1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.EnabledCalc = true;
            this.comboBox1.ItemHeight = 20;
            this.comboBox1.Items.AddRange(new object[] {
            "loadlibrary",
            "manualmap (best)"});
            this.comboBox1.Location = new System.Drawing.Point(112, 130);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(112, 26);
            this.comboBox1.TabIndex = 35;
            this.comboBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(109, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 15);
            this.label3.TabIndex = 36;
            this.label3.Text = "Injection Method";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Checked = true;
            this.checkEdit1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.checkEdit1.EnabledCalc = true;
            this.checkEdit1.Location = new System.Drawing.Point(228, 54);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Size = new System.Drawing.Size(112, 18);
            this.checkEdit1.TabIndex = 37;
            this.checkEdit1.Text = "Asynchronous";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(103, 153);
            this.listBox1.TabIndex = 34;
            this.listBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(226, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Process:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(109, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 15);
            this.label2.TabIndex = 33;
            this.label2.Text = "DLLs";
            // 
            // button5
            // 
            this.button5.EnabledCalc = true;
            this.button5.Location = new System.Drawing.Point(227, 81);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 26);
            this.button5.TabIndex = 25;
            this.button5.TabStop = false;
            this.button5.Text = "Inject DLLs";
            this.button5.Click += new XylosButton.ClickEventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.EnabledCalc = true;
            this.button4.Location = new System.Drawing.Point(112, 81);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 26);
            this.button4.TabIndex = 31;
            this.button4.TabStop = false;
            this.button4.Text = "Remove All DLLs";
            this.button4.Click += new XylosButton.ClickEventHandler(this.button4_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(274, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 27;
            this.label6.Text = "Unknown";
            // 
            // button1
            // 
            this.button1.EnabledCalc = true;
            this.button1.Location = new System.Drawing.Point(112, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 26);
            this.button1.TabIndex = 29;
            this.button1.TabStop = false;
            this.button1.Text = "Add DLL";
            this.button1.Click += new XylosButton.ClickEventHandler(this.button1_Click);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.comboBoxEdit1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEdit1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEdit1.EnabledCalc = true;
            this.comboBoxEdit1.ItemHeight = 20;
            this.comboBoxEdit1.Location = new System.Drawing.Point(227, 19);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Size = new System.Drawing.Size(112, 26);
            this.comboBoxEdit1.Sorted = true;
            this.comboBoxEdit1.TabIndex = 28;
            this.comboBoxEdit1.TabStop = false;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            this.comboBoxEdit1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.comboBoxEdit1_MouseDown);
            // 
            // button2
            // 
            this.button2.EnabledCalc = true;
            this.button2.Location = new System.Drawing.Point(112, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 26);
            this.button2.TabIndex = 30;
            this.button2.TabStop = false;
            this.button2.Text = "Remove DLL";
            this.button2.Click += new XylosButton.ClickEventHandler(this.button2_Click);
            // 
            // MiscDivider
            // 
            this.MiscDivider.BackColor = System.Drawing.Color.White;
            this.MiscDivider.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MiscDivider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.MiscDivider.Location = new System.Drawing.Point(184, 4);
            this.MiscDivider.Name = "MiscDivider";
            this.MiscDivider.Size = new System.Drawing.Size(342, 285);
            this.MiscDivider.TabIndex = 4;
            this.MiscDivider.Tag = "Divider";
            this.MiscDivider.Text = "Misc";
            // 
            // Settings
            // 
            this.Settings.BackColor = System.Drawing.Color.White;
            this.Settings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Settings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.Settings.Location = new System.Drawing.Point(184, 4);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(342, 285);
            this.Settings.TabIndex = 5;
            this.Settings.Text = "Settings";
            // 
            // Debug
            // 
            this.Debug.BackColor = System.Drawing.Color.White;
            this.Debug.Controls.Add(this.debugLogger);
            this.Debug.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Debug.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(133)))), ((int)(((byte)(142)))));
            this.Debug.ImageIndex = 2;
            this.Debug.Location = new System.Drawing.Point(184, 4);
            this.Debug.Name = "Debug";
            this.Debug.Padding = new System.Windows.Forms.Padding(3);
            this.Debug.Size = new System.Drawing.Size(342, 285);
            this.Debug.TabIndex = 2;
            this.Debug.Text = "Debug Output";
            // 
            // debugLogger
            // 
            this.debugLogger.BackColor = System.Drawing.Color.White;
            this.debugLogger.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.debugLogger.DetectUrls = false;
            this.debugLogger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugLogger.Location = new System.Drawing.Point(3, 3);
            this.debugLogger.Name = "debugLogger";
            this.debugLogger.ReadOnly = true;
            this.debugLogger.Size = new System.Drawing.Size(336, 279);
            this.debugLogger.TabIndex = 1;
            this.debugLogger.TabStop = false;
            this.debugLogger.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(55)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(530, 293);
            this.Controls.Add(this.xylosTabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Injector";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.xylosTabControl1.ResumeLayout(false);
            this.Injector.ResumeLayout(false);
            this.Injector.PerformLayout();
            this.Debug.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private XylosTabControl xylosTabControl1;
        private System.Windows.Forms.TabPage Injector;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private XylosButton button5;
        private XylosButton button4;
        private System.Windows.Forms.Label label6;
        private XylosButton button1;
        private XylosCombobox comboBoxEdit1;
        private XylosButton button2;
        private System.Windows.Forms.TabPage Debug;
        private System.Windows.Forms.RichTextBox debugLogger;
        private XylosCombobox comboBox1;
        private System.Windows.Forms.Label label3;
        private XylosCheckBox checkEdit1;
        private XylosButton simpleButton1;
        private System.Windows.Forms.TabPage MainDivider;
        private System.Windows.Forms.TabPage MiscDivider;
        private System.Windows.Forms.TabPage Settings;
        private XylosSeparator xylosSeparator1;
        private System.Windows.Forms.Panel panel1;
    }
}

