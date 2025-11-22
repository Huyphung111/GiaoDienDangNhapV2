namespace GiaoDienDangNhap
{
    partial class ThuCung
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThuCung));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTim = new System.Windows.Forms.Button();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.txtTim = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tool_them = new System.Windows.Forms.ToolStripLabel();
            this.tool_xoaa = new System.Windows.Forms.ToolStripLabel();
            this.tool_sua = new System.Windows.Forms.ToolStripLabel();
            this.tool_luu = new System.Windows.Forms.ToolStripLabel();
            this.cbMaloai = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMota = new System.Windows.Forms.TextBox();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.txtTuoi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Btn_ThemThuCungVaoDonHang = new System.Windows.Forms.Button();
            this.tool_themm = new System.Windows.Forms.ToolStripButton();
            this.tool_xoa = new System.Windows.Forms.ToolStripButton();
            this.tool_suaa = new System.Windows.Forms.ToolStripButton();
            this.tool_luuu = new System.Windows.Forms.ToolStripButton();
            this.picAnh = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTim);
            this.panel1.Controls.Add(this.cboLoai);
            this.panel1.Controls.Add(this.txtTim);
            this.panel1.Location = new System.Drawing.Point(200, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1246, 122);
            this.panel1.TabIndex = 0;
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(735, 28);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(111, 41);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // cboLoai
            // 
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Location = new System.Drawing.Point(587, 41);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(121, 28);
            this.cboLoai.TabIndex = 1;
            this.cboLoai.SelectedIndexChanged += new System.EventHandler(this.cboLoai_SelectedIndexChanged);
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(202, 41);
            this.txtTim.Name = "txtTim";
            this.txtTim.Size = new System.Drawing.Size(354, 26);
            this.txtTim.TabIndex = 0;
            this.txtTim.TextChanged += new System.EventHandler(this.txtTim_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(200, 174);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1246, 232);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Btn_ThemThuCungVaoDonHang);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Controls.Add(this.cbMaloai);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.txtMa);
            this.panel2.Controls.Add(this.txtSoLuong);
            this.panel2.Controls.Add(this.picAnh);
            this.panel2.Controls.Add(this.btnChonAnh);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtMota);
            this.panel2.Controls.Add(this.txtGiaBan);
            this.panel2.Controls.Add(this.cboGioiTinh);
            this.panel2.Controls.Add(this.txtTuoi);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtTen);
            this.panel2.Location = new System.Drawing.Point(200, 441);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1237, 255);
            this.panel2.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_themm,
            this.tool_them,
            this.tool_xoa,
            this.tool_xoaa,
            this.tool_suaa,
            this.tool_sua,
            this.tool_luuu,
            this.tool_luu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1237, 33);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tool_them
            // 
            this.tool_them.Name = "tool_them";
            this.tool_them.Size = new System.Drawing.Size(56, 28);
            this.tool_them.Text = "Thêm";
            // 
            // tool_xoaa
            // 
            this.tool_xoaa.Name = "tool_xoaa";
            this.tool_xoaa.Size = new System.Drawing.Size(43, 28);
            this.tool_xoaa.Text = "Xóa";
            // 
            // tool_sua
            // 
            this.tool_sua.Name = "tool_sua";
            this.tool_sua.Size = new System.Drawing.Size(42, 28);
            this.tool_sua.Text = "Sửa";
            // 
            // tool_luu
            // 
            this.tool_luu.Name = "tool_luu";
            this.tool_luu.Size = new System.Drawing.Size(41, 28);
            this.tool_luu.Text = "Lưu";
            // 
            // cbMaloai
            // 
            this.cbMaloai.FormattingEnabled = true;
            this.cbMaloai.Location = new System.Drawing.Point(412, 81);
            this.cbMaloai.Name = "cbMaloai";
            this.cbMaloai.Size = new System.Drawing.Size(165, 28);
            this.cbMaloai.TabIndex = 20;
            this.cbMaloai.SelectedIndexChanged += new System.EventHandler(this.cbMaloai_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(607, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 20);
            this.label9.TabIndex = 19;
            this.label9.Text = "Ma tc";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(607, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 20);
            this.label8.TabIndex = 18;
            this.label8.Text = "So luong";
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(682, 84);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(165, 26);
            this.txtMa.TabIndex = 17;
            this.txtMa.TextChanged += new System.EventHandler(this.txtMa_TextChanged);
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(682, 29);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(165, 26);
            this.txtSoLuong.TabIndex = 16;
            this.txtSoLuong.TextChanged += new System.EventHandler(this.txtSoLuong_TextChanged);
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.Location = new System.Drawing.Point(1058, 184);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(98, 36);
            this.btnChonAnh.TabIndex = 14;
            this.btnChonAnh.Text = "Thêm ảnh";
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(920, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Hình ảnh";
            // 
            // txtMota
            // 
            this.txtMota.Location = new System.Drawing.Point(412, 135);
            this.txtMota.Multiline = true;
            this.txtMota.Name = "txtMota";
            this.txtMota.Size = new System.Drawing.Size(281, 81);
            this.txtMota.TabIndex = 10;
            this.txtMota.TextChanged += new System.EventHandler(this.txtMota_TextChanged);
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Location = new System.Drawing.Point(412, 29);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(165, 26);
            this.txtGiaBan.TabIndex = 9;
            this.txtGiaBan.TextChanged += new System.EventHandler(this.txtGiaBan_TextChanged);
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.Location = new System.Drawing.Point(134, 135);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(121, 28);
            this.cboGioiTinh.TabIndex = 8;
            this.cboGioiTinh.SelectedIndexChanged += new System.EventHandler(this.cboGioiTinh_SelectedIndexChanged);
            // 
            // txtTuoi
            // 
            this.txtTuoi.Location = new System.Drawing.Point(134, 81);
            this.txtTuoi.Name = "txtTuoi";
            this.txtTuoi.Size = new System.Drawing.Size(165, 26);
            this.txtTuoi.TabIndex = 7;
            this.txtTuoi.TextChanged += new System.EventHandler(this.txtTuoi_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(325, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Mô tả";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(325, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Mã loại";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(325, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Giá bán ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Giới tính";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tuổi";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tên thú cưng ";
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(134, 32);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(165, 26);
            this.txtTen.TabIndex = 0;
            this.txtTen.TextChanged += new System.EventHandler(this.txtTen_TextChanged);
            // 
            // Btn_ThemThuCungVaoDonHang
            // 
            this.Btn_ThemThuCungVaoDonHang.Location = new System.Drawing.Point(735, 135);
            this.Btn_ThemThuCungVaoDonHang.Name = "Btn_ThemThuCungVaoDonHang";
            this.Btn_ThemThuCungVaoDonHang.Size = new System.Drawing.Size(215, 81);
            this.Btn_ThemThuCungVaoDonHang.TabIndex = 22;
            this.Btn_ThemThuCungVaoDonHang.Text = "Thêm";
            this.Btn_ThemThuCungVaoDonHang.UseVisualStyleBackColor = true;
            // 
            // tool_themm
            // 
            this.tool_themm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_themm.Image = ((System.Drawing.Image)(resources.GetObject("tool_themm.Image")));
            this.tool_themm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_themm.Name = "tool_themm";
            this.tool_themm.Size = new System.Drawing.Size(34, 28);
            this.tool_themm.Text = "toolStripButton1";
            this.tool_themm.Click += new System.EventHandler(this.tool_themm_Click);
            // 
            // tool_xoa
            // 
            this.tool_xoa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_xoa.Image = ((System.Drawing.Image)(resources.GetObject("tool_xoa.Image")));
            this.tool_xoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_xoa.Name = "tool_xoa";
            this.tool_xoa.Size = new System.Drawing.Size(34, 28);
            this.tool_xoa.Text = "toolStripButton2";
            this.tool_xoa.Click += new System.EventHandler(this.tool_xoa_Click);
            // 
            // tool_suaa
            // 
            this.tool_suaa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_suaa.Image = ((System.Drawing.Image)(resources.GetObject("tool_suaa.Image")));
            this.tool_suaa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_suaa.Name = "tool_suaa";
            this.tool_suaa.Size = new System.Drawing.Size(34, 28);
            this.tool_suaa.Text = "toolStripButton3";
            this.tool_suaa.Click += new System.EventHandler(this.tool_suaa_Click);
            // 
            // tool_luuu
            // 
            this.tool_luuu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_luuu.Image = ((System.Drawing.Image)(resources.GetObject("tool_luuu.Image")));
            this.tool_luuu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_luuu.Name = "tool_luuu";
            this.tool_luuu.Size = new System.Drawing.Size(34, 28);
            this.tool_luuu.Text = "Sửa";
            this.tool_luuu.Click += new System.EventHandler(this.tool_luuu_Click);
            // 
            // picAnh
            // 
            this.picAnh.Location = new System.Drawing.Point(999, 35);
            this.picAnh.Name = "picAnh";
            this.picAnh.Size = new System.Drawing.Size(193, 128);
            this.picAnh.TabIndex = 15;
            this.picAnh.TabStop = false;
            this.picAnh.Click += new System.EventHandler(this.picAnh_Click);
            // 
            // ThuCung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1565, 970);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "ThuCung";
            this.Text = "ThuCung";
            this.Load += new System.EventHandler(this.ThuCung_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAnh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.TextBox txtTim;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtMota;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.TextBox txtTuoi;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox picAnh;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.ComboBox cbMaloai;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tool_themm;
        private System.Windows.Forms.ToolStripLabel tool_them;
        private System.Windows.Forms.ToolStripButton tool_xoa;
        private System.Windows.Forms.ToolStripLabel tool_xoaa;
        private System.Windows.Forms.ToolStripButton tool_suaa;
        private System.Windows.Forms.ToolStripLabel tool_sua;
        private System.Windows.Forms.ToolStripButton tool_luuu;
        private System.Windows.Forms.ToolStripLabel tool_luu;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button Btn_ThemThuCungVaoDonHang;
    }
}