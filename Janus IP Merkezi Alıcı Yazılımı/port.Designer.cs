namespace Janus_IP_Merkezi_Alıcı_Yazılımı
{
    partial class port
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(port));
            this.button2 = new System.Windows.Forms.Button();
            this.findport = new System.Windows.Forms.Button();
            this.textBoxport = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(240, 85);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Kapat";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // findport
            // 
            this.findport.Location = new System.Drawing.Point(60, 85);
            this.findport.Name = "findport";
            this.findport.Size = new System.Drawing.Size(75, 23);
            this.findport.TabIndex = 6;
            this.findport.Text = "Uygula";
            this.findport.UseVisualStyleBackColor = true;
            this.findport.Click += new System.EventHandler(this.findport_Click_1);
            // 
            // textBoxport
            // 
            this.textBoxport.Location = new System.Drawing.Point(156, 23);
            this.textBoxport.Name = "textBoxport";
            this.textBoxport.Size = new System.Drawing.Size(214, 20);
            this.textBoxport.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Denetleme Portu";
            // 
            // port
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 294);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.findport);
            this.Controls.Add(this.textBoxport);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "port";
            this.Text = "Network Portları";
            this.Load += new System.EventHandler(this.port_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button findport;
        private System.Windows.Forms.TextBox textBoxport;
        private System.Windows.Forms.Label label1;
    }
}