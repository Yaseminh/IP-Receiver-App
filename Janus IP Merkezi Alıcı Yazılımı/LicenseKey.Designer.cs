namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    partial class LicenseKey
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseKey));
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1key = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2invoice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lsnMail = new System.Windows.Forms.Label();
            this.lblurun = new System.Windows.Forms.Label();
            this.mailAddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(206, 298);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(238, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "Lisansı Aktif Et";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(73, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Lisans Anahtarı:";
            // 
            // textBox1key
            // 
            this.textBox1key.Location = new System.Drawing.Point(206, 248);
            this.textBox1key.Name = "textBox1key";
            this.textBox1key.Size = new System.Drawing.Size(238, 20);
            this.textBox1key.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(206, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(238, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Lisansı Doğrula";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2invoice
            // 
            this.textBox2invoice.Location = new System.Drawing.Point(206, 150);
            this.textBox2invoice.Name = "textBox2invoice";
            this.textBox2invoice.Size = new System.Drawing.Size(238, 20);
            this.textBox2invoice.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(76, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Fatura Numarası:";
            // 
            // lsnMail
            // 
            this.lsnMail.AutoSize = true;
            this.lsnMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lsnMail.Location = new System.Drawing.Point(73, 46);
            this.lsnMail.Name = "lsnMail";
            this.lsnMail.Size = new System.Drawing.Size(368, 13);
            this.lsnMail.TabIndex = 11;
            this.lsnMail.Text = "Lisansı Anahtarı almak için Mai adresi ve fatura bilgilerini Giriniz";
            // 
            // lblurun
            // 
            this.lblurun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblurun.Location = new System.Drawing.Point(76, 94);
            this.lblurun.Name = "lblurun";
            this.lblurun.Size = new System.Drawing.Size(100, 23);
            this.lblurun.TabIndex = 10;
            this.lblurun.Text = "Mail Adresi:";
            // 
            // mailAddress
            // 
            this.mailAddress.Location = new System.Drawing.Point(206, 97);
            this.mailAddress.Name = "mailAddress";
            this.mailAddress.Size = new System.Drawing.Size(238, 20);
            this.mailAddress.TabIndex = 9;
            // 
            // LicenseKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(516, 401);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1key);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2invoice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lsnMail);
            this.Controls.Add(this.lblurun);
            this.Controls.Add(this.mailAddress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LicenseKey";
            this.Text = "Lisans";
            this.Load += new System.EventHandler(this.LicenseKey_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1key;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2invoice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lsnMail;
        private System.Windows.Forms.Label lblurun;
        private System.Windows.Forms.TextBox mailAddress;
    }
}