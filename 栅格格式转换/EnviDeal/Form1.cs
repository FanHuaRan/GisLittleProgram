using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MrFan.Tool.EnviDeal;
namespace EnviDeal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           // this.openFileDialog1.Filter = "*.|*.";
            this.BSQButton1.Checked = true;
            this.DisPlayGroupBox.Visible = false;
            #region 绑定
            //this.StartPosition = FormStartPosition.CenterScreen;
            //this.map = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            //Binding binding1 = new Binding("Text", this, "pixColumCount");
            //Binding binding2 = new Binding("Text", this, "pixRowCount");
            //Binding binding3 = new Binding("Text", this, "dataType");
            //Binding binding4 = new Binding("Text", this, "orignType", true);
            //Binding binding5 = new Binding("Text", this, "bandCount");
            //Binding binding6 = new Binding("Text", this, "ss", true);
            //Binding binding7 = new Binding("Text", this, "inPutFileName");
            //Binding binding8 = new Binding("Text", this, "outPutFileName");
            //this.pixCollumTxt.DataBindings.Add(binding1);
            //this.pixRowTxt.DataBindings.Add(binding2);
            //this.dataTypeTxt.DataBindings.Add(binding3);
            //this.GetTypeTxt.DataBindings.Add(binding4);
            //this.BandCountTxt.DataBindings.Add(binding5);
            //this.HeadFileTxt.DataBindings.Add(binding6);
            //BindCommon.SetBinding(this.HeadFileTxt, "Text", bb, "name");
            //this.inputFileTxt.DataBindings.Add(binding7);
            //this.outFileTxt.DataBindings.Add(binding8);
            #endregion
        }
        //存储像素的波段信息
        Pixel[,] pixInformation;
        //头文件名
        public string headFileName;
        public string newHeadFileName;
       public string inPutFileName;
        string outPutFileName;
        int pixColumCount=0;
        int pixRowCount=0;
        int dataType = 0;
        int bandCount=0;
        string orignType="";
        string transformType;
        Bitmap map;
        private void ChooseFileHeadBtt_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.openFileDialog1.ShowDialog())
            {
                this.headFileName = this.openFileDialog1.FileName;
                if(EnviConvertUtil.ReadHDR(headFileName, ref pixColumCount, ref pixRowCount, ref bandCount, ref dataType, ref orignType))
                {
                    this.HeadFileTxt.Text = this.headFileName;
                    this.pixCollumTxt.Text = this.pixColumCount.ToString();
                    this.pixRowTxt.Text = this.pixRowCount.ToString();
                    this.BandCountTxt.Text = this.bandCount.ToString();
                    this.dataTypeTxt.Text = this.dataType.ToString();
                    this.GetTypeTxt.Text = this.orignType;
                    AddBandCheckBox(bandCount);
                }
            }
        }
        private void AddBandCheckBox(int num)
        {
            foreach(var  v in this.DisPlayGroupBox.Controls)
            {
                if(v is CheckBox)
                this.DisPlayGroupBox.Controls.Remove((CheckBox)v);
            }
            int yCount=5;
           for(int i=0;i<num;i++)
           {
               CheckBox cb = new CheckBox();
               cb.Width = 120;
               cb.Height = 20;
               cb.Top = 20*i;
               cb.BringToFront();
               cb.Anchor = AnchorStyles.Left;
               cb.Name = i + "band";
               cb.Text = (i + 1).ToString() + "波段";
               this.DisPlayGroupBox.Controls.Add(cb);
               if (i == 0)
                   cb.Checked = true;
               yCount += 6;
           }
        }
        private void inputFileBtt_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.inPutFileName = this.openFileDialog1.FileName;
                this.inputFileTxt.Text = this.inPutFileName;

                if (this.orignType == "bsq")
                    this.pixInformation = EnviConvertUtil.GetPixInformationFromBsq(this.inPutFileName, this.pixColumCount, this.pixRowCount, this.bandCount, this.dataType);
                else if (this.orignType == "bil")
                    this.pixInformation = EnviConvertUtil.GetPixInformationFromBil(this.inPutFileName, this.pixColumCount, this.pixRowCount, this.bandCount, this.dataType);
                else if (this.orignType == "bip")
                    this.pixInformation = EnviConvertUtil.GetPixInformationFromBip(this.inPutFileName, this.pixColumCount, this.pixRowCount, this.bandCount, this.dataType);
                DrawImage();
            }
        }

        private void outFileBtt_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectPath = this.folderBrowserDialog1.SelectedPath;
                string[] splits = inPutFileName.Split('\\');
                string[] splits2 = headFileName.Split('\\');
                this.outPutFileName = selectPath +"\\"+ splits[splits.Length-1];
                this.newHeadFileName = selectPath + "\\" + splits2[splits2.Length - 1];
                this.outFileTxt.Text = this.outPutFileName;
            }
        }

        private void TransformBtt_Click(object sender, EventArgs e)
        {
            foreach(var v in this.groupBox4.Controls)
            {
                RadioButton rb = (RadioButton)v;
                if(rb.Checked==true)
                {
                    this.transformType = rb.Text;
                }
            }
            if(transformType==orignType)
            {
                MessageBox.Show("转换格式不能与原格式相同");
                return;
            }
            bool flag = true;
            if(orignType=="bsq"&&transformType=="bil")
            {
               flag=EnviConvertUtil.BsqToBil(inPutFileName, outPutFileName, pixColumCount, pixRowCount, bandCount, dataType);
               EnviConvertUtil.SaveHDR(headFileName, newHeadFileName, "bil");
            }
            else if (orignType == "bsq" && transformType == "bip")
            {
                flag = EnviConvertUtil.BsqToBip(inPutFileName, outPutFileName, pixColumCount, pixRowCount, bandCount, dataType);
                EnviConvertUtil.SaveHDR(headFileName, newHeadFileName, "bip");
            }
            else if (orignType == "bil" && transformType == "bsq")
            {
                flag = EnviConvertUtil.BilToBsq(inPutFileName, outPutFileName, pixColumCount, pixRowCount, bandCount, dataType);
                EnviConvertUtil.SaveHDR(headFileName, newHeadFileName, "bsq");
            }
            else if (orignType == "bil" && transformType == "bip")
            {
                flag = EnviConvertUtil.BilToBip(inPutFileName, outPutFileName, pixColumCount, pixRowCount, bandCount, dataType);
                EnviConvertUtil.SaveHDR(headFileName, newHeadFileName, "bip");
            }
            else if (orignType == "bip" && transformType == "bsq")
            {
                flag = EnviConvertUtil.BipToBsq(inPutFileName, outPutFileName, pixColumCount, pixRowCount, bandCount, dataType);
                EnviConvertUtil.SaveHDR(headFileName, newHeadFileName, "bsq");
            }
            else if (orignType == "bip" && transformType == "bil")
            {
                flag = EnviConvertUtil.BipToBil(inPutFileName, outPutFileName, pixColumCount, pixRowCount, bandCount, dataType);
                EnviConvertUtil.SaveHDR(headFileName, newHeadFileName, "bil");
            }
            if(flag)
            {
                MessageBox.Show("转换成功");
                if (this.transformType == "bsq")
                    this.pixInformation = EnviConvertUtil.GetPixInformationFromBsq(this.outPutFileName, this.pixColumCount, this.pixRowCount, this.bandCount, this.dataType);
                else if (this.transformType == "bil")
                    this.pixInformation = EnviConvertUtil.GetPixInformationFromBil(this.outPutFileName, this.pixColumCount, this.pixRowCount, this.bandCount, this.dataType);
                else if (this.transformType == "bip")
                    this.pixInformation = EnviConvertUtil.GetPixInformationFromBip(this.outPutFileName, this.pixColumCount, this.pixRowCount, this.bandCount, this.dataType);
                DrawImage();
            }
            else
            {
                MessageBox.Show("转换失败");
            }
        }
        //使用异步方法绘图
        private async void DrawImage()
        {
            await Task.Run(() =>
            {
                //设置重新读取的效果
                map = new Bitmap(1, 1);
                this.pictureBox1.Image= map;
                List<int> bandNums = new List<int>();
                foreach (var v in DisPlayGroupBox.Controls)
                {
                    if (v is CheckBox)
                    {
                        CheckBox cb = (CheckBox)v;
                        if (cb.Checked == true)
                        {
                            string[] strs = cb.Name.Split('b');
                            bandNums.Add(int.Parse(strs[0]));
                        }
                    }
                }

                if (bandNums.Count > 3)
                {
                    MessageBox.Show("不能显示超过3个波段");
                }
                this.map = new Bitmap(pixColumCount,pixRowCount);
                for (int row = 0; row < pixRowCount; row++)
                {
                    for (int columnum = 0; columnum < pixColumCount; columnum++)
                    {
                        int c1 = this.pixInformation[row, columnum].ColorNum[bandNums[0]];
                        int c2 = bandNums.Count > 1 ? this.pixInformation[row, columnum].ColorNum[bandNums[1]] : 0;
                        int c3 = bandNums.Count > 2 ? this.pixInformation[row, columnum].ColorNum[bandNums[2]] : 0;
                        Color color = Color.FromArgb(c1, c2, c3);
                        map.SetPixel(columnum,row,color);
                    }
                }
                this.pictureBox1.Image = map;
            });
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.DisPlayGroupBox.Visible = !this.DisPlayGroupBox.Visible;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.inPutFileName))
                return;
            DrawImage();
            this.DisPlayGroupBox.Visible = false;
        }

        private void GetTypeTxt_TextChanged(object sender, EventArgs e)
        {
            this.orignType = ((TextBox)sender).Text;
        }
    }

}
