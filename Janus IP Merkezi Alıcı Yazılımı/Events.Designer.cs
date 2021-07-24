namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    partial class Events
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Events));
            this.eventsearch = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // eventsearch
            // 
            this.eventsearch.Location = new System.Drawing.Point(439, 4);
            this.eventsearch.Name = "eventsearch";
            this.eventsearch.Size = new System.Drawing.Size(75, 23);
            this.eventsearch.TabIndex = 7;
            this.eventsearch.Text = "Sorgula";
            this.eventsearch.UseVisualStyleBackColor = true;
            this.eventsearch.Click += new System.EventHandler(this.eventsearch_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Tıbbi Alarmlar",
            "100 - (Tıbbi)",
            "101 - (Kişisel Acil Durum)",
            "102 - (Raporlama başarısız)",
            "Yangın alarmları",
            "110 - (Ateş)",
            "111 - (Duman)",
            "112 - (Yanma)",
            "113 - (Su baskını)",
            "114 - (Isı)",
            "115 - (Çekme istasyonu)",
            "116 - (Kanal)",
            "117 - (Alev)",
            "118 - (Alarma Yakın)",
            "Panik Alarmları",
            "120 - (Panik)",
            "121 - (Zorlama)",
            "122 - (Sessiz)",
            "123 - (Sesli)",
            "124 - (Zorlama - Erişim verildi)",
            "125 - (Zorlama - Çıkış verildi)",
            "Hırsız Alarmları",
            "130 - (Hırsızlık)",
            "131 - (Çevre)",
            "132 - (İçerisi)",
            "133 - (24 Saat (Güvenli))",
            "134 - (Giriş / Çıkış)",
            "135 - (Gündüz / gece)",
            "136 - (Dış Mekan)",
            "137 - (Sabotaj)",
            "138 - (Alarma yakın)",
            "139 - (İzinsiz Giriş Dogrulayıcı)",
            "Genel Alarm",
            "144 - (Sensör kurcalama)",
            "145 - (Genişletme modülü sabotaj)",
            "146 - (Sessiz İzinsiz Giriş)",
            "147 - (Sensör Denetim Arızası)",
            "24 Saat Hırsızlık Yok",
            "150 - (24 Saat İzinsiz Giriş Olmayan)",
            "151 - (Gaz algılandı)",
            "152 - (Soğutma)",
            "153 - (Isı kaybı)",
            "154 - (Su Sızıntısı)",
            "162 - (Karbon Monoksit algılandı)",
            "Sistem Sorunları",
            "301 - (Ac Kaybı)",
            "302 - (Düşük sistem pili)",
            "305 - (Sistem sıfırlama)",
            "309 - (Sistem kapatma)",
            "311 - (Pil Eksik / Bitmiş)",
            "Sensör Arızası",
            "381 - (Denetim kaybı - RF)",
            "383 - (Sensör sabotaj)",
            "384 - (RF düşük pil)",
            "Etkinlik",
            "400 - (Aç / Kapat)",
            "401 - (Kullanıcıya göre Açık / Kapalı)",
            "407 - (Uzaktan kurma / çözme)",
            "409 - (Anahtar Açık / Kapalı)",
            "441 - (İçeride KURMA)",
            "442 - (Anahtarlı İçeride KURMA)",
            "Giriş kontrolü",
            "422 - (Kullanıcıya göre erişim raporu)",
            "570 - (Bölge / Sensör devredışı)",
            "571 - (Yangın devredışı)",
            "572 - (24 Saatlik bölge devredışı)",
            "573 - (İzinsiz Giriş devredışı)"});
            this.comboBox1.Location = new System.Drawing.Point(167, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(246, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Olayları Seçerek Sorgulayın";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-2, 46);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(898, 403);
            this.dataGridView1.TabIndex = 8;
            // 
            // Events
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(896, 449);
            this.Controls.Add(this.eventsearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Events";
            this.Text = "Olaylar";
            this.Load += new System.EventHandler(this.Events_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button eventsearch;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}