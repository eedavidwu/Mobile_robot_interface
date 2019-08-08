namespace test_try
{
    partial class DataAnalyse
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button_az = new System.Windows.Forms.Button();
            this.button_ax = new System.Windows.Forms.Button();
            this.button_ay = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(129, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(696, 468);
            this.panel1.TabIndex = 2;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(690, 461);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(6, 440);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "renew";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button_az
            // 
            this.button_az.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_az.Location = new System.Drawing.Point(6, 411);
            this.button_az.Name = "button_az";
            this.button_az.Size = new System.Drawing.Size(111, 23);
            this.button_az.TabIndex = 4;
            this.button_az.Text = "az：view";
            this.button_az.UseVisualStyleBackColor = true;
            this.button_az.Click += new System.EventHandler(this.button_az_Click);
            // 
            // button_ax
            // 
            this.button_ax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ax.Location = new System.Drawing.Point(6, 353);
            this.button_ax.Name = "button_ax";
            this.button_ax.Size = new System.Drawing.Size(111, 23);
            this.button_ax.TabIndex = 5;
            this.button_ax.Text = "ax：view";
            this.button_ax.UseVisualStyleBackColor = true;
            this.button_ax.Click += new System.EventHandler(this.button_ax_Click);
            // 
            // button_ay
            // 
            this.button_ay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ay.Location = new System.Drawing.Point(6, 382);
            this.button_ay.Name = "button_ay";
            this.button_ay.Size = new System.Drawing.Size(111, 23);
            this.button_ay.TabIndex = 6;
            this.button_ay.Text = "ay：view";
            this.button_ay.UseVisualStyleBackColor = true;
            this.button_ay.Click += new System.EventHandler(this.button_ay_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button_ax);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button_az);
            this.panel2.Controls.Add(this.button_ay);
            this.panel2.Location = new System.Drawing.Point(2, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(124, 468);
            this.panel2.TabIndex = 7;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel2_Paint);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(6, 324);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Acceleration curve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataAnalyse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 470);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DataAnalyse";
            this.ShowInTaskbar = false;
            this.Text = "DataAnalyse";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DataAnalyse_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_az;
        private System.Windows.Forms.Button button_ax;
        private System.Windows.Forms.Button button_ay;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
    }
}