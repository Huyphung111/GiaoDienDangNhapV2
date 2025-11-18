namespace GiaoDienDangNhap
{
    partial class ThongKeBaoCao
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_inbaocao = new System.Windows.Forms.Button();
            this.btn_XuatExcel = new System.Windows.Forms.Button();
            this.btn_modulieu = new System.Windows.Forms.Button();
            this.dtp_DenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtp_TuNgay = new System.Windows.Forms.DateTimePicker();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_doanhthuphukien = new System.Windows.Forms.TextBox();
            this.txt_doanhthuthucung = new System.Windows.Forms.TextBox();
            this.txt_doanhthudichvu = new System.Windows.Forms.TextBox();
            this.txt_tongdoanhthu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv_BaoCao = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BaoCao)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_inbaocao);
            this.groupBox1.Controls.Add(this.btn_XuatExcel);
            this.groupBox1.Controls.Add(this.btn_modulieu);
            this.groupBox1.Controls.Add(this.dtp_DenNgay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtp_TuNgay);
            this.groupBox1.Location = new System.Drawing.Point(251, 136);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1399, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Chọn thời gian";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btn_inbaocao
            // 
            this.btn_inbaocao.Location = new System.Drawing.Point(952, 115);
            this.btn_inbaocao.Name = "btn_inbaocao";
            this.btn_inbaocao.Size = new System.Drawing.Size(117, 41);
            this.btn_inbaocao.TabIndex = 6;
            this.btn_inbaocao.Text = "In Báo Cáo";
            this.btn_inbaocao.UseVisualStyleBackColor = true;
            this.btn_inbaocao.Click += new System.EventHandler(this.btn_inbaocao_Click);
            // 
            // btn_XuatExcel
            // 
            this.btn_XuatExcel.Image = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_18_091139;
            this.btn_XuatExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_XuatExcel.Location = new System.Drawing.Point(671, 115);
            this.btn_XuatExcel.Name = "btn_XuatExcel";
            this.btn_XuatExcel.Size = new System.Drawing.Size(150, 41);
            this.btn_XuatExcel.TabIndex = 5;
            this.btn_XuatExcel.Text = "Xuất Excel";
            this.btn_XuatExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_XuatExcel.UseVisualStyleBackColor = true;
            this.btn_XuatExcel.Click += new System.EventHandler(this.btn_XuatExcel_Click);
            // 
            // btn_modulieu
            // 
            this.btn_modulieu.Image = global::GiaoDienDangNhap.Properties.Resources.Ảnh_chụp_màn_hình_2025_11_18_090915;
            this.btn_modulieu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_modulieu.Location = new System.Drawing.Point(387, 115);
            this.btn_modulieu.Name = "btn_modulieu";
            this.btn_modulieu.Size = new System.Drawing.Size(143, 41);
            this.btn_modulieu.TabIndex = 4;
            this.btn_modulieu.Text = "Xem báo cáo";
            this.btn_modulieu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_modulieu.UseVisualStyleBackColor = true;
            this.btn_modulieu.Click += new System.EventHandler(this.btn_modulieu_Click);
            // 
            // dtp_DenNgay
            // 
            this.dtp_DenNgay.Location = new System.Drawing.Point(952, 47);
            this.dtp_DenNgay.Name = "dtp_DenNgay";
            this.dtp_DenNgay.Size = new System.Drawing.Size(242, 26);
            this.dtp_DenNgay.TabIndex = 3;
            this.dtp_DenNgay.ValueChanged += new System.EventHandler(this.dtp_DenNgay_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(844, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ngày lập:";
            // 
            // dtp_TuNgay
            // 
            this.dtp_TuNgay.Location = new System.Drawing.Point(414, 47);
            this.dtp_TuNgay.Name = "dtp_TuNgay";
            this.dtp_TuNgay.Size = new System.Drawing.Size(232, 26);
            this.dtp_TuNgay.TabIndex = 0;
            this.dtp_TuNgay.ValueChanged += new System.EventHandler(this.dtp_TuNgay_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txt_doanhthuphukien);
            this.panel1.Controls.Add(this.txt_doanhthuthucung);
            this.panel1.Controls.Add(this.txt_doanhthudichvu);
            this.panel1.Controls.Add(this.txt_tongdoanhthu);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(254, 325);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1396, 119);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txt_doanhthuphukien
            // 
            this.txt_doanhthuphukien.Location = new System.Drawing.Point(993, 77);
            this.txt_doanhthuphukien.Name = "txt_doanhthuphukien";
            this.txt_doanhthuphukien.Size = new System.Drawing.Size(270, 26);
            this.txt_doanhthuphukien.TabIndex = 7;
            this.txt_doanhthuphukien.TextChanged += new System.EventHandler(this.txt_doanhthuphukien_TextChanged);
            // 
            // txt_doanhthuthucung
            // 
            this.txt_doanhthuthucung.Location = new System.Drawing.Point(410, 77);
            this.txt_doanhthuthucung.Name = "txt_doanhthuthucung";
            this.txt_doanhthuthucung.Size = new System.Drawing.Size(270, 26);
            this.txt_doanhthuthucung.TabIndex = 6;
            this.txt_doanhthuthucung.TextChanged += new System.EventHandler(this.txt_doanhthuthucung_TextChanged);
            // 
            // txt_doanhthudichvu
            // 
            this.txt_doanhthudichvu.Location = new System.Drawing.Point(993, 21);
            this.txt_doanhthudichvu.Name = "txt_doanhthudichvu";
            this.txt_doanhthudichvu.Size = new System.Drawing.Size(270, 26);
            this.txt_doanhthudichvu.TabIndex = 5;
            this.txt_doanhthudichvu.TextChanged += new System.EventHandler(this.txt_doanhthudichvu_TextChanged);
            // 
            // txt_tongdoanhthu
            // 
            this.txt_tongdoanhthu.Location = new System.Drawing.Point(410, 24);
            this.txt_tongdoanhthu.Name = "txt_tongdoanhthu";
            this.txt_tongdoanhthu.Size = new System.Drawing.Size(270, 26);
            this.txt_tongdoanhthu.TabIndex = 4;
            this.txt_tongdoanhthu.TextChanged += new System.EventHandler(this.txt_tongdoanhthu_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(815, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 20);
            this.label6.TabIndex = 3;
            this.label6.Text = "Doanh Thu Phụ Kiện";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(822, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Doanh Thu Dịch Vụ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Doanh Thu Thú Cưng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(256, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tổng Doanh Thu";
            // 
            // dgv_BaoCao
            // 
            this.dgv_BaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_BaoCao.Location = new System.Drawing.Point(254, 450);
            this.dgv_BaoCao.Name = "dgv_BaoCao";
            this.dgv_BaoCao.RowHeadersWidth = 62;
            this.dgv_BaoCao.RowTemplate.Height = 28;
            this.dgv_BaoCao.Size = new System.Drawing.Size(1396, 269);
            this.dgv_BaoCao.TabIndex = 2;
            this.dgv_BaoCao.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_BaoCao_CellContentClick);
            this.dgv_BaoCao.SelectionChanged += new System.EventHandler(this.dgv_BaoCao_SelectionChanged);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(578, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(701, 73);
            this.label7.TabIndex = 3;
            this.label7.Text = "BÁO CÁO DOANH THU";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ThongKeBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1849, 870);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgv_BaoCao);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "ThongKeBaoCao";
            this.Text = "ThongKeBaoCao";
            this.Load += new System.EventHandler(this.ThongKeBaoCao_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_BaoCao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DateTimePicker dtp_DenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtp_TuNgay;
        private System.Windows.Forms.Button btn_inbaocao;
        private System.Windows.Forms.Button btn_XuatExcel;
        private System.Windows.Forms.Button btn_modulieu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_doanhthuphukien;
        private System.Windows.Forms.TextBox txt_doanhthuthucung;
        private System.Windows.Forms.TextBox txt_doanhthudichvu;
        private System.Windows.Forms.TextBox txt_tongdoanhthu;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgv_BaoCao;
        private System.Windows.Forms.Label label7;
    }
}