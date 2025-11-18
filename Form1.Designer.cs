namespace GiaoDienDangNhap
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txt_tdn = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txt_matkhau = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_taotaikhoang = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btn_doimatkhau = new System.Windows.Forms.Button();
            this.check_hienmatkhau = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1019, 103);
            this.flowLayoutPanel2.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(986, 103);
            this.label3.TabIndex = 0;
            this.label3.Text = "PET SHOP";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click_2);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.HighlightText;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(220, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.HighlightText;
            this.panel3.BackgroundImage = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_10082710;
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Location = new System.Drawing.Point(91, 59);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(487, 67);
            this.panel3.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_1008274;
            this.panel4.Controls.Add(this.txt_tdn);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 39);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(487, 25);
            this.panel4.TabIndex = 7;
            // 
            // txt_tdn
            // 
            this.txt_tdn.AutoSize = true;
            this.txt_tdn.BackColor = System.Drawing.SystemColors.Control;
            this.txt_tdn.Dock = System.Windows.Forms.DockStyle.Left;
            this.txt_tdn.ForeColor = System.Drawing.Color.Red;
            this.txt_tdn.Image = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_1008278;
            this.txt_tdn.Location = new System.Drawing.Point(0, 0);
            this.txt_tdn.Name = "txt_tdn";
            this.txt_tdn.Size = new System.Drawing.Size(266, 20);
            this.txt_tdn.TabIndex = 0;
            this.txt_tdn.Text = "Tên đăng nhập không tồn tại, thử lại!";
            this.txt_tdn.Click += new System.EventHandler(this.txt_tdn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 39);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel2.Location = new System.Drawing.Point(4, 37);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(530, 2);
            this.panel2.TabIndex = 7;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.DarkGray;
            this.textBox1.Location = new System.Drawing.Point(-3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(544, 31);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Tài Khoản";
            this.textBox1.Click += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 137);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(74, 46);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.HighlightText;
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Location = new System.Drawing.Point(88, 137);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(490, 59);
            this.panel5.TabIndex = 8;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_1008275;
            this.panel6.Controls.Add(this.txt_matkhau);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 39);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(490, 34);
            this.panel6.TabIndex = 7;
            // 
            // txt_matkhau
            // 
            this.txt_matkhau.AutoSize = true;
            this.txt_matkhau.BackColor = System.Drawing.SystemColors.Control;
            this.txt_matkhau.Dock = System.Windows.Forms.DockStyle.Left;
            this.txt_matkhau.ForeColor = System.Drawing.Color.Red;
            this.txt_matkhau.Image = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_1008277;
            this.txt_matkhau.Location = new System.Drawing.Point(0, 0);
            this.txt_matkhau.Name = "txt_matkhau";
            this.txt_matkhau.Size = new System.Drawing.Size(266, 20);
            this.txt_matkhau.TabIndex = 0;
            this.txt_matkhau.Text = "Tên đăng nhập không tồn tại, thử lại!";
            this.txt_matkhau.Click += new System.EventHandler(this.label3_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Controls.Add(this.textBox2);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(490, 39);
            this.panel7.TabIndex = 6;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel8.Location = new System.Drawing.Point(4, 37);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(530, 2);
            this.panel8.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.textBox2.ForeColor = System.Drawing.Color.DarkGray;
            this.textBox2.Location = new System.Drawing.Point(3, 3);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(500, 31);
            this.textBox2.TabIndex = 0;
            this.textBox2.Text = "Mật Khẩu";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Image = global::GiaoDienDangNhap.Properties.Resources.unnamed__3_3;
            this.button1.Location = new System.Drawing.Point(189, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(244, 62);
            this.button1.TabIndex = 3;
            this.button1.Text = "Dang Nhap";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pictureBox1.BackgroundImage = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_1008279;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 56);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 42);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // btn_taotaikhoang
            // 
            this.btn_taotaikhoang.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_taotaikhoang.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_taotaikhoang.BackgroundImage")));
            this.btn_taotaikhoang.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btn_taotaikhoang.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_taotaikhoang.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_taotaikhoang.Image = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_1008272;
            this.btn_taotaikhoang.Location = new System.Drawing.Point(36, 216);
            this.btn_taotaikhoang.Name = "btn_taotaikhoang";
            this.btn_taotaikhoang.Size = new System.Drawing.Size(133, 44);
            this.btn_taotaikhoang.TabIndex = 1;
            this.btn_taotaikhoang.Text = "Tạo tài khoảng";
            this.btn_taotaikhoang.UseVisualStyleBackColor = false;
            this.btn_taotaikhoang.Click += new System.EventHandler(this.btn_taotaikhoang_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Ivory;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Image = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_1008271;
            this.label1.Location = new System.Drawing.Point(252, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "ĐĂNG NHẬP";
            // 
            // panel9
            // 
            this.panel9.BackgroundImage = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_100827;
            this.panel9.Controls.Add(this.btn_doimatkhau);
            this.panel9.Controls.Add(this.check_hienmatkhau);
            this.panel9.Controls.Add(this.btn_taotaikhoang);
            this.panel9.Controls.Add(this.pictureBox1);
            this.panel9.Controls.Add(this.label1);
            this.panel9.Controls.Add(this.button1);
            this.panel9.Controls.Add(this.panel5);
            this.panel9.Controls.Add(this.pictureBox2);
            this.panel9.Controls.Add(this.panel3);
            this.panel9.Controls.Add(this.label2);
            this.panel9.Location = new System.Drawing.Point(9, 106);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(636, 275);
            this.panel9.TabIndex = 0;
            // 
            // btn_doimatkhau
            // 
            this.btn_doimatkhau.BackColor = System.Drawing.SystemColors.HighlightText;
            this.btn_doimatkhau.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_doimatkhau.BackgroundImage")));
            this.btn_doimatkhau.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btn_doimatkhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_doimatkhau.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_doimatkhau.Image = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_1008272;
            this.btn_doimatkhau.Location = new System.Drawing.Point(463, 219);
            this.btn_doimatkhau.Name = "btn_doimatkhau";
            this.btn_doimatkhau.Size = new System.Drawing.Size(126, 39);
            this.btn_doimatkhau.TabIndex = 3;
            this.btn_doimatkhau.Text = "Đổi mật khẩu";
            this.btn_doimatkhau.UseVisualStyleBackColor = false;
            this.btn_doimatkhau.Click += new System.EventHandler(this.btn_doimatkhau_Click);
            // 
            // check_hienmatkhau
            // 
            this.check_hienmatkhau.AutoSize = true;
            this.check_hienmatkhau.BackgroundImage = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_13_10082710;
            this.check_hienmatkhau.Location = new System.Drawing.Point(451, 186);
            this.check_hienmatkhau.Name = "check_hienmatkhau";
            this.check_hienmatkhau.Size = new System.Drawing.Size(138, 24);
            this.check_hienmatkhau.TabIndex = 5;
            this.check_hienmatkhau.Text = "Hiện mật khẩu";
            this.check_hienmatkhau.UseVisualStyleBackColor = true;
            this.check_hienmatkhau.CheckedChanged += new System.EventHandler(this.check_hienmatkhau_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::GiaoDienDangNhap.Properties.Resources.unnamed__3_1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1019, 605);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.flowLayoutPanel2);
            this.HelpButton = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_taotaikhoang;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label txt_matkhau;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label txt_tdn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.CheckBox check_hienmatkhau;
        private System.Windows.Forms.Button btn_doimatkhau;
    }
}

