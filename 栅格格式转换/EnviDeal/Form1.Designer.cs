namespace EnviDeal
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataTypeTxt = new System.Windows.Forms.TextBox();
            this.GetTypeTxt = new System.Windows.Forms.TextBox();
            this.BandCountTxt = new System.Windows.Forms.TextBox();
            this.pixRowTxt = new System.Windows.Forms.TextBox();
            this.pixCollumTxt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HeadFileTxt = new System.Windows.Forms.TextBox();
            this.ChooseFileHeadBtt = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.outFileBtt = new System.Windows.Forms.Button();
            this.outFileTxt = new System.Windows.Forms.TextBox();
            this.TransformBtt = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BILButton3 = new System.Windows.Forms.RadioButton();
            this.BIPButton2 = new System.Windows.Forms.RadioButton();
            this.BSQButton1 = new System.Windows.Forms.RadioButton();
            this.inputFileBtt = new System.Windows.Forms.Button();
            this.inputFileTxt = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DisPlayGroupBox = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.DisPlayGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.HeadFileTxt);
            this.groupBox1.Controls.Add(this.ChooseFileHeadBtt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1362, 141);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "头文件";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataTypeTxt);
            this.groupBox3.Controls.Add(this.GetTypeTxt);
            this.groupBox3.Controls.Add(this.BandCountTxt);
            this.groupBox3.Controls.Add(this.pixRowTxt);
            this.groupBox3.Controls.Add(this.pixCollumTxt);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(30, 46);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(573, 89);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "文件信息";
            // 
            // dataTypeTxt
            // 
            this.dataTypeTxt.Location = new System.Drawing.Point(407, 21);
            this.dataTypeTxt.Name = "dataTypeTxt";
            this.dataTypeTxt.Size = new System.Drawing.Size(100, 21);
            this.dataTypeTxt.TabIndex = 9;
            // 
            // GetTypeTxt
            // 
            this.GetTypeTxt.Location = new System.Drawing.Point(216, 54);
            this.GetTypeTxt.Name = "GetTypeTxt";
            this.GetTypeTxt.Size = new System.Drawing.Size(100, 21);
            this.GetTypeTxt.TabIndex = 8;
            this.GetTypeTxt.TextChanged += new System.EventHandler(this.GetTypeTxt_TextChanged);
            // 
            // BandCountTxt
            // 
            this.BandCountTxt.Location = new System.Drawing.Point(216, 21);
            this.BandCountTxt.Name = "BandCountTxt";
            this.BandCountTxt.Size = new System.Drawing.Size(100, 21);
            this.BandCountTxt.TabIndex = 7;
            // 
            // pixRowTxt
            // 
            this.pixRowTxt.Location = new System.Drawing.Point(65, 54);
            this.pixRowTxt.Name = "pixRowTxt";
            this.pixRowTxt.Size = new System.Drawing.Size(64, 21);
            this.pixRowTxt.TabIndex = 6;
            // 
            // pixCollumTxt
            // 
            this.pixCollumTxt.Location = new System.Drawing.Point(65, 22);
            this.pixCollumTxt.Name = "pixCollumTxt";
            this.pixCollumTxt.Size = new System.Drawing.Size(64, 21);
            this.pixCollumTxt.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(348, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "数据格式";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(157, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "解析格式";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(157, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "波段个数";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "像素行数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "像素列数";
            // 
            // HeadFileTxt
            // 
            this.HeadFileTxt.Location = new System.Drawing.Point(87, 18);
            this.HeadFileTxt.Name = "HeadFileTxt";
            this.HeadFileTxt.Size = new System.Drawing.Size(344, 21);
            this.HeadFileTxt.TabIndex = 2;
            // 
            // ChooseFileHeadBtt
            // 
            this.ChooseFileHeadBtt.Location = new System.Drawing.Point(437, 18);
            this.ChooseFileHeadBtt.Name = "ChooseFileHeadBtt";
            this.ChooseFileHeadBtt.Size = new System.Drawing.Size(54, 23);
            this.ChooseFileHeadBtt.TabIndex = 1;
            this.ChooseFileHeadBtt.Text = "选择";
            this.ChooseFileHeadBtt.UseVisualStyleBackColor = true;
            this.ChooseFileHeadBtt.Click += new System.EventHandler(this.ChooseFileHeadBtt_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "头文件：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.outFileBtt);
            this.groupBox2.Controls.Add(this.outFileTxt);
            this.groupBox2.Controls.Add(this.TransformBtt);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.inputFileBtt);
            this.groupBox2.Controls.Add(this.inputFileTxt);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox2.Location = new System.Drawing.Point(0, 141);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 601);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "格式转换";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(198, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "选择波段";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // outFileBtt
            // 
            this.outFileBtt.Location = new System.Drawing.Point(219, 113);
            this.outFileBtt.Name = "outFileBtt";
            this.outFileBtt.Size = new System.Drawing.Size(48, 23);
            this.outFileBtt.TabIndex = 8;
            this.outFileBtt.Text = "选择";
            this.outFileBtt.UseVisualStyleBackColor = true;
            this.outFileBtt.Click += new System.EventHandler(this.outFileBtt_Click);
            // 
            // outFileTxt
            // 
            this.outFileTxt.Location = new System.Drawing.Point(14, 113);
            this.outFileTxt.Name = "outFileTxt";
            this.outFileTxt.Size = new System.Drawing.Size(199, 21);
            this.outFileTxt.TabIndex = 7;
            // 
            // TransformBtt
            // 
            this.TransformBtt.Location = new System.Drawing.Point(59, 324);
            this.TransformBtt.Name = "TransformBtt";
            this.TransformBtt.Size = new System.Drawing.Size(75, 23);
            this.TransformBtt.TabIndex = 6;
            this.TransformBtt.Text = "转换";
            this.TransformBtt.UseVisualStyleBackColor = true;
            this.TransformBtt.Click += new System.EventHandler(this.TransformBtt_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BILButton3);
            this.groupBox4.Controls.Add(this.BIPButton2);
            this.groupBox4.Controls.Add(this.BSQButton1);
            this.groupBox4.Location = new System.Drawing.Point(14, 163);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 136);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "转换格式";
            // 
            // BILButton3
            // 
            this.BILButton3.AutoSize = true;
            this.BILButton3.Location = new System.Drawing.Point(16, 99);
            this.BILButton3.Name = "BILButton3";
            this.BILButton3.Size = new System.Drawing.Size(41, 16);
            this.BILButton3.TabIndex = 2;
            this.BILButton3.TabStop = true;
            this.BILButton3.Text = "bil";
            this.BILButton3.UseVisualStyleBackColor = true;
            // 
            // BIPButton2
            // 
            this.BIPButton2.AutoSize = true;
            this.BIPButton2.Location = new System.Drawing.Point(16, 60);
            this.BIPButton2.Name = "BIPButton2";
            this.BIPButton2.Size = new System.Drawing.Size(41, 16);
            this.BIPButton2.TabIndex = 1;
            this.BIPButton2.TabStop = true;
            this.BIPButton2.Text = "bip";
            this.BIPButton2.UseVisualStyleBackColor = true;
            // 
            // BSQButton1
            // 
            this.BSQButton1.AutoSize = true;
            this.BSQButton1.Location = new System.Drawing.Point(16, 21);
            this.BSQButton1.Name = "BSQButton1";
            this.BSQButton1.Size = new System.Drawing.Size(41, 16);
            this.BSQButton1.TabIndex = 0;
            this.BSQButton1.TabStop = true;
            this.BSQButton1.Text = "bsq";
            this.BSQButton1.UseVisualStyleBackColor = true;
            // 
            // inputFileBtt
            // 
            this.inputFileBtt.Location = new System.Drawing.Point(219, 52);
            this.inputFileBtt.Name = "inputFileBtt";
            this.inputFileBtt.Size = new System.Drawing.Size(48, 23);
            this.inputFileBtt.TabIndex = 3;
            this.inputFileBtt.Text = "选择";
            this.inputFileBtt.UseVisualStyleBackColor = true;
            this.inputFileBtt.Click += new System.EventHandler(this.inputFileBtt_Click);
            // 
            // inputFileTxt
            // 
            this.inputFileTxt.Location = new System.Drawing.Point(14, 54);
            this.inputFileTxt.Name = "inputFileTxt";
            this.inputFileTxt.Size = new System.Drawing.Size(199, 21);
            this.inputFileTxt.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "输出文件";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "输入文件";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(273, 141);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1089, 601);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // DisPlayGroupBox
            // 
            this.DisPlayGroupBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.DisPlayGroupBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DisPlayGroupBox.Controls.Add(this.button2);
            this.DisPlayGroupBox.Location = new System.Drawing.Point(273, 141);
            this.DisPlayGroupBox.Name = "DisPlayGroupBox";
            this.DisPlayGroupBox.Size = new System.Drawing.Size(181, 320);
            this.DisPlayGroupBox.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2.Location = new System.Drawing.Point(0, 293);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(177, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 742);
            this.Controls.Add(this.DisPlayGroupBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "简易Envi图像小软件";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.DisPlayGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox dataTypeTxt;
        private System.Windows.Forms.TextBox GetTypeTxt;
        private System.Windows.Forms.TextBox BandCountTxt;
        private System.Windows.Forms.TextBox pixRowTxt;
        private System.Windows.Forms.TextBox pixCollumTxt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox HeadFileTxt;
        private System.Windows.Forms.Button ChooseFileHeadBtt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button outFileBtt;
        private System.Windows.Forms.TextBox outFileTxt;
        private System.Windows.Forms.Button TransformBtt;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button inputFileBtt;
        private System.Windows.Forms.TextBox inputFileTxt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton BILButton3;
        private System.Windows.Forms.RadioButton BIPButton2;
        private System.Windows.Forms.RadioButton BSQButton1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel DisPlayGroupBox;
        private System.Windows.Forms.Button button2;
    }
}

