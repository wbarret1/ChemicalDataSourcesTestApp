namespace ChemicalDataSourcesTestApp
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.findButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chemicalTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.nioshCDSCButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.ToxnetHSDBButton = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.nfpaHealthLabel = new System.Windows.Forms.Label();
            this.nfpaFlammabilityLabel = new System.Windows.Forms.Label();
            this.nfpaInstabilityLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.internationalChemSafetyDataCardutton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // findButton
            // 
            this.findButton.Location = new System.Drawing.Point(334, 19);
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(75, 23);
            this.findButton.TabIndex = 0;
            this.findButton.Text = "Find";
            this.findButton.UseVisualStyleBackColor = true;
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chemical Name:";
            // 
            // chemicalTextBox
            // 
            this.chemicalTextBox.Location = new System.Drawing.Point(110, 21);
            this.chemicalTextBox.Name = "chemicalTextBox";
            this.chemicalTextBox.Size = new System.Drawing.Size(208, 20);
            this.chemicalTextBox.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(334, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 250);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Synonyms:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(20, 109);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(298, 160);
            this.listBox1.TabIndex = 7;
            // 
            // nioshCDSCButton
            // 
            this.nioshCDSCButton.Location = new System.Drawing.Point(23, 291);
            this.nioshCDSCButton.Name = "nioshCDSCButton";
            this.nioshCDSCButton.Size = new System.Drawing.Size(295, 23);
            this.nioshCDSCButton.TabIndex = 8;
            this.nioshCDSCButton.Text = "Show NIOSH Chemical Safety Data Card";
            this.nioshCDSCButton.UseVisualStyleBackColor = true;
            this.nioshCDSCButton.Click += new System.EventHandler(this.nioshCDSCButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(476, 18);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 9;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // ToxnetHSDBButton
            // 
            this.ToxnetHSDBButton.Location = new System.Drawing.Point(23, 355);
            this.ToxnetHSDBButton.Name = "ToxnetHSDBButton";
            this.ToxnetHSDBButton.Size = new System.Drawing.Size(295, 23);
            this.ToxnetHSDBButton.TabIndex = 10;
            this.ToxnetHSDBButton.Text = "Show TOXNET Hazardous Substance Database Entry";
            this.ToxnetHSDBButton.UseVisualStyleBackColor = true;
            this.ToxnetHSDBButton.Click += new System.EventHandler(this.ToxnetHSDBButton_Click_1);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "nfpa_diamond.png");
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ChemicalDataSourcesTestApp.Properties.Resources.nfpa_diamond;
            this.pictureBox2.Location = new System.Drawing.Point(377, 339);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(189, 184);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // nfpaHealthLabel
            // 
            this.nfpaHealthLabel.AutoSize = true;
            this.nfpaHealthLabel.BackColor = System.Drawing.Color.White;
            this.nfpaHealthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nfpaHealthLabel.Location = new System.Drawing.Point(415, 422);
            this.nfpaHealthLabel.Name = "nfpaHealthLabel";
            this.nfpaHealthLabel.Size = new System.Drawing.Size(127, 20);
            this.nfpaHealthLabel.TabIndex = 12;
            this.nfpaHealthLabel.Text = "nfpaHealthLabel";
            this.nfpaHealthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.nfpaHealthLabel.Visible = false;
            // 
            // nfpaFlammabilityLabel
            // 
            this.nfpaFlammabilityLabel.AutoSize = true;
            this.nfpaFlammabilityLabel.BackColor = System.Drawing.Color.White;
            this.nfpaFlammabilityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nfpaFlammabilityLabel.Location = new System.Drawing.Point(466, 375);
            this.nfpaFlammabilityLabel.Name = "nfpaFlammabilityLabel";
            this.nfpaFlammabilityLabel.Size = new System.Drawing.Size(167, 20);
            this.nfpaFlammabilityLabel.TabIndex = 13;
            this.nfpaFlammabilityLabel.Text = "nfpaFlammabilityLabel";
            this.nfpaFlammabilityLabel.Visible = false;
            // 
            // nfpaInstabilityLabel
            // 
            this.nfpaInstabilityLabel.AutoSize = true;
            this.nfpaInstabilityLabel.BackColor = System.Drawing.Color.White;
            this.nfpaInstabilityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nfpaInstabilityLabel.Location = new System.Drawing.Point(511, 422);
            this.nfpaInstabilityLabel.Name = "nfpaInstabilityLabel";
            this.nfpaInstabilityLabel.Size = new System.Drawing.Size(146, 20);
            this.nfpaInstabilityLabel.TabIndex = 14;
            this.nfpaInstabilityLabel.Text = "nfpaInstabilityLabel";
            this.nfpaInstabilityLabel.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 397);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "label4";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 420);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 443);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "label6";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 466);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "label7";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 489);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "label8";
            this.label8.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 512);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "label9";
            this.label9.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 535);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "label10";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "label11";
            this.label11.Visible = false;
            // 
            // internationalChemSafetyDataCardutton
            // 
            this.internationalChemSafetyDataCardutton.Location = new System.Drawing.Point(23, 323);
            this.internationalChemSafetyDataCardutton.Name = "internationalChemSafetyDataCardutton";
            this.internationalChemSafetyDataCardutton.Size = new System.Drawing.Size(295, 23);
            this.internationalChemSafetyDataCardutton.TabIndex = 23;
            this.internationalChemSafetyDataCardutton.Text = "Show International Chemical Safety Data Card";
            this.internationalChemSafetyDataCardutton.UseVisualStyleBackColor = true;
            this.internationalChemSafetyDataCardutton.Click += new System.EventHandler(this.internationalChemSafetyDataCardutton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "ACToR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AcceptButton = this.findButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 575);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.internationalChemSafetyDataCardutton);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nfpaInstabilityLabel);
            this.Controls.Add(this.nfpaFlammabilityLabel);
            this.Controls.Add(this.nfpaHealthLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.ToxnetHSDBButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.nioshCDSCButton);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chemicalTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.findButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button findButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chemicalTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button nioshCDSCButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button ToxnetHSDBButton;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label nfpaHealthLabel;
        private System.Windows.Forms.Label nfpaFlammabilityLabel;
        private System.Windows.Forms.Label nfpaInstabilityLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button internationalChemSafetyDataCardutton;
        private System.Windows.Forms.Button button1;
    }
}

