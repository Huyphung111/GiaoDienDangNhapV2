namespace GiaoDienDangNhap
{
    partial class DonHang
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gb_donhang = new System.Windows.Forms.GroupBox();
            this.btn_huydonhangnay = new System.Windows.Forms.Button();
            this.cb_trangthai = new System.Windows.Forms.ComboBox();
            this.txt_tongtien = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txt_makh = new System.Windows.Forms.TextBox();
            this.txt_madonhang = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cb_MaCTDH = new System.Windows.Forms.ComboBox();
            this.txt_thanhtien = new System.Windows.Forms.TextBox();
            this.txt_dongia = new System.Windows.Forms.TextBox();
            this.txt_soluong = new System.Windows.Forms.TextBox();
            this.txt_masanpham = new System.Windows.Forms.TextBox();
            this.txt_mathucung = new System.Windows.Forms.TextBox();
            this.txt_loaisanpham = new System.Windows.Forms.TextBox();
            this.txt_madonhang_chitietdonhang = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_quaylai = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gb_donhang.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(439, 26);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1071, 219);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // gb_donhang
            // 
            this.gb_donhang.Controls.Add(this.btn_huydonhangnay);
            this.gb_donhang.Controls.Add(this.cb_trangthai);
            this.gb_donhang.Controls.Add(this.txt_tongtien);
            this.gb_donhang.Controls.Add(this.dateTimePicker1);
            this.gb_donhang.Controls.Add(this.txt_makh);
            this.gb_donhang.Controls.Add(this.txt_madonhang);
            this.gb_donhang.Controls.Add(this.label5);
            this.gb_donhang.Controls.Add(this.label4);
            this.gb_donhang.Controls.Add(this.label3);
            this.gb_donhang.Controls.Add(this.label2);
            this.gb_donhang.Controls.Add(this.label1);
            this.gb_donhang.Location = new System.Drawing.Point(27, 27);
            this.gb_donhang.Name = "gb_donhang";
            this.gb_donhang.Size = new System.Drawing.Size(356, 493);
            this.gb_donhang.TabIndex = 1;
            this.gb_donhang.TabStop = false;
            this.gb_donhang.Text = "Đơn Hàng";
            this.gb_donhang.Enter += new System.EventHandler(this.gb_donhang_Enter);
            // 
            // btn_huydonhangnay
            // 
            this.btn_huydonhangnay.Location = new System.Drawing.Point(144, 423);
            this.btn_huydonhangnay.Name = "btn_huydonhangnay";
            this.btn_huydonhangnay.Size = new System.Drawing.Size(195, 30);
            this.btn_huydonhangnay.TabIndex = 3;
            this.btn_huydonhangnay.Text = "Hủy đơn hàng này ";
            this.btn_huydonhangnay.UseVisualStyleBackColor = true;
            this.btn_huydonhangnay.Click += new System.EventHandler(this.btn_huydonhangnay_Click);
            // 
            // cb_trangthai
            // 
            this.cb_trangthai.FormattingEnabled = true;
            this.cb_trangthai.Location = new System.Drawing.Point(144, 360);
            this.cb_trangthai.Name = "cb_trangthai";
            this.cb_trangthai.Size = new System.Drawing.Size(195, 28);
            this.cb_trangthai.TabIndex = 9;
            this.cb_trangthai.SelectedIndexChanged += new System.EventHandler(this.cb_trangthai_SelectedIndexChanged);
            // 
            // txt_tongtien
            // 
            this.txt_tongtien.Location = new System.Drawing.Point(144, 282);
            this.txt_tongtien.Name = "txt_tongtien";
            this.txt_tongtien.Size = new System.Drawing.Size(199, 26);
            this.txt_tongtien.TabIndex = 8;
            this.txt_tongtien.TextChanged += new System.EventHandler(this.txt_tongtien_TextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(134, 210);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(209, 26);
            this.dateTimePicker1.TabIndex = 7;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // txt_makh
            // 
            this.txt_makh.Location = new System.Drawing.Point(144, 136);
            this.txt_makh.Name = "txt_makh";
            this.txt_makh.Size = new System.Drawing.Size(199, 26);
            this.txt_makh.TabIndex = 6;
            this.txt_makh.TextChanged += new System.EventHandler(this.txt_makh_TextChanged);
            // 
            // txt_madonhang
            // 
            this.txt_madonhang.Location = new System.Drawing.Point(144, 61);
            this.txt_madonhang.Name = "txt_madonhang";
            this.txt_madonhang.Size = new System.Drawing.Size(199, 26);
            this.txt_madonhang.TabIndex = 5;
            this.txt_madonhang.TextChanged += new System.EventHandler(this.txt_madonhang_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 363);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Trạng thái:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 285);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Tổng Tiền:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ngày đặt";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mã khách hàng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã đơn hàng: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_xoa);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.cb_MaCTDH);
            this.groupBox2.Controls.Add(this.txt_thanhtien);
            this.groupBox2.Controls.Add(this.txt_dongia);
            this.groupBox2.Controls.Add(this.txt_soluong);
            this.groupBox2.Controls.Add(this.txt_masanpham);
            this.groupBox2.Controls.Add(this.txt_mathucung);
            this.groupBox2.Controls.Add(this.txt_loaisanpham);
            this.groupBox2.Controls.Add(this.txt_madonhang_chitietdonhang);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(443, 296);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1067, 223);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi Tiết Đơn Hàng";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(901, 36);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(148, 148);
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // cb_MaCTDH
            // 
            this.cb_MaCTDH.FormattingEnabled = true;
            this.cb_MaCTDH.Location = new System.Drawing.Point(136, 34);
            this.cb_MaCTDH.Name = "cb_MaCTDH";
            this.cb_MaCTDH.Size = new System.Drawing.Size(156, 28);
            this.cb_MaCTDH.TabIndex = 24;
            this.cb_MaCTDH.SelectedIndexChanged += new System.EventHandler(this.cb_MaCTDH_SelectedIndexChanged);
            // 
            // txt_thanhtien
            // 
            this.txt_thanhtien.Location = new System.Drawing.Point(683, 88);
            this.txt_thanhtien.Name = "txt_thanhtien";
            this.txt_thanhtien.Size = new System.Drawing.Size(175, 26);
            this.txt_thanhtien.TabIndex = 23;
            this.txt_thanhtien.TextChanged += new System.EventHandler(this.txt_thanhtien_TextChanged);
            // 
            // txt_dongia
            // 
            this.txt_dongia.Location = new System.Drawing.Point(683, 36);
            this.txt_dongia.Name = "txt_dongia";
            this.txt_dongia.Size = new System.Drawing.Size(175, 26);
            this.txt_dongia.TabIndex = 22;
            this.txt_dongia.TextChanged += new System.EventHandler(this.txt_dongia_TextChanged);
            // 
            // txt_soluong
            // 
            this.txt_soluong.Location = new System.Drawing.Point(419, 151);
            this.txt_soluong.Name = "txt_soluong";
            this.txt_soluong.Size = new System.Drawing.Size(148, 26);
            this.txt_soluong.TabIndex = 21;
            this.txt_soluong.TextChanged += new System.EventHandler(this.txt_soluong_TextChanged);
            // 
            // txt_masanpham
            // 
            this.txt_masanpham.Location = new System.Drawing.Point(419, 91);
            this.txt_masanpham.Name = "txt_masanpham";
            this.txt_masanpham.Size = new System.Drawing.Size(148, 26);
            this.txt_masanpham.TabIndex = 20;
            this.txt_masanpham.TextChanged += new System.EventHandler(this.txt_masanpham_TextChanged);
            // 
            // txt_mathucung
            // 
            this.txt_mathucung.Location = new System.Drawing.Point(419, 39);
            this.txt_mathucung.Name = "txt_mathucung";
            this.txt_mathucung.Size = new System.Drawing.Size(148, 26);
            this.txt_mathucung.TabIndex = 19;
            this.txt_mathucung.TextChanged += new System.EventHandler(this.txt_mathucung_TextChanged);
            // 
            // txt_loaisanpham
            // 
            this.txt_loaisanpham.Location = new System.Drawing.Point(136, 151);
            this.txt_loaisanpham.Name = "txt_loaisanpham";
            this.txt_loaisanpham.Size = new System.Drawing.Size(149, 26);
            this.txt_loaisanpham.TabIndex = 18;
            this.txt_loaisanpham.TextChanged += new System.EventHandler(this.txt_loaisanpham_TextChanged);
            // 
            // txt_madonhang_chitietdonhang
            // 
            this.txt_madonhang_chitietdonhang.Location = new System.Drawing.Point(136, 94);
            this.txt_madonhang_chitietdonhang.Name = "txt_madonhang_chitietdonhang";
            this.txt_madonhang_chitietdonhang.Size = new System.Drawing.Size(149, 26);
            this.txt_madonhang_chitietdonhang.TabIndex = 17;
            this.txt_madonhang_chitietdonhang.TextChanged += new System.EventHandler(this.txt_madonhang_chitietdonhang_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(592, 94);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 20);
            this.label14.TabIndex = 16;
            this.label14.Text = "Thành tiền:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(607, 42);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 20);
            this.label12.TabIndex = 15;
            this.label12.Text = "Đơn giá";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(309, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 20);
            this.label11.TabIndex = 14;
            this.label11.Text = "Số Lương: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(304, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 20);
            this.label10.TabIndex = 13;
            this.label10.Text = "Mã Sản Phẩm";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(309, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Mã Thú Cưng";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Loại Sản Phẩm: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 97);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Mã đơn hàng: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(38, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Mã CTDH";
            // 
            // btn_xoa
            // 
            this.btn_xoa.Location = new System.Drawing.Point(628, 145);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(120, 38);
            this.btn_xoa.TabIndex = 26;
            this.btn_xoa.Text = "Xoa";
            this.btn_xoa.UseVisualStyleBackColor = true;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_quaylai
            // 
            this.btn_quaylai.Location = new System.Drawing.Point(439, 252);
            this.btn_quaylai.Name = "btn_quaylai";
            this.btn_quaylai.Size = new System.Drawing.Size(120, 38);
            this.btn_quaylai.TabIndex = 27;
            this.btn_quaylai.Text = "Quay lai";
            this.btn_quaylai.UseVisualStyleBackColor = true;
            this.btn_quaylai.Click += new System.EventHandler(this.btn_quaylai_Click);
            // 
            // DonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1603, 538);
            this.Controls.Add(this.btn_quaylai);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gb_donhang);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DonHang";
            this.Text = "DonHang";
            this.Load += new System.EventHandler(this.DonHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gb_donhang.ResumeLayout(false);
            this.gb_donhang.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox gb_donhang;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_tongtien;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox txt_makh;
        private System.Windows.Forms.TextBox txt_madonhang;
        private System.Windows.Forms.ComboBox cb_trangthai;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_madonhang_chitietdonhang;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_thanhtien;
        private System.Windows.Forms.TextBox txt_dongia;
        private System.Windows.Forms.TextBox txt_soluong;
        private System.Windows.Forms.TextBox txt_masanpham;
        private System.Windows.Forms.TextBox txt_mathucung;
        private System.Windows.Forms.TextBox txt_loaisanpham;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btn_huydonhangnay;
        private System.Windows.Forms.ComboBox cb_MaCTDH;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Button btn_quaylai;
    }
}