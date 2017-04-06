using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShapeFileDeal.ShapeClass;
using ShapeFileDemo.ShapeClass;
using ShapeFileDemo.Util;
namespace ShapeFileDemo
{
    /// <summary>
    /// shape文件读取重构
    /// 2017/04/06 fhr
    /// </summary>
    public partial class Form1 : Form
    {
        #region Constrctor
        public Form1()
        {
            InitializeComponent();
        }
        #endregion
        #region Fileds
        Pen pen = new Pen(Color.Black, 1);//定义画笔
        Bitmap map;
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            map = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            openFileDialog1.Filter = "shapefiles(*.shp)|*.shp|All files(*.*)|*.*";//打开文件路径
            FileHead head = null;
            List<ShapeBaseClass> shapes;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                head=ShapeFileUtil.readShapeFile(openFileDialog1.OpenFile(), out shapes);
                WirteTxt(head);
                DrawShape(head, shapes);
            }
        }

        private void DrawShape(FileHead head, List<ShapeBaseClass> shapes)
        {
            Graphics myE = Graphics.FromImage(map);
            Draw(myE, head, shapes);
            this.pictureBox1.Image = map;
        }
        private void Draw(Graphics e,FileHead head, List<ShapeBaseClass> shapes)
        {
            switch (head.ShapeType)
            {
                case 1://点类型
                    drawPoints(e, head, shapes);
                    break;
                case 3://线类型
                    drawPolylines(e, head, shapes);
                    break;
                case 5://面类型
                    drawPolygons(e, head, shapes);
                    break;
            }
        }

        private void drawPolygons(Graphics e, FileHead head, List<ShapeBaseClass> shapes)
        {
            foreach (SPolygon p in shapes)
            {
                for (int i = 0; i < p.NumParts; i++)
                {
                    int startpoint = 0;
                    int endpoint = 0;
                    if (i == p.NumParts - 1)
                    {
                        startpoint = (int)p.Parts[i];
                        endpoint = p.NumPoints;
                    }
                    else
                    {
                        startpoint = (int)p.Parts[i];
                        endpoint = (int)p.Parts[i + 1];
                    }
                    PointF[] points = new PointF[endpoint - startpoint];
                    for (int k = 0, j = startpoint; j < endpoint; j++, k++)
                    {
                        SPoint ps = p.Points[j];
                        points[k].X = (float)(4 * (50 + ps.X - head.Xmin));
                        points[k].Y = (float)(4 * (100 - ps.Y + head.Ymin));
                    }
                    e.DrawPolygon(pen, points);
                }
            }
        }

        private void drawPolylines(Graphics e, FileHead head, List<ShapeBaseClass> shapes)
        {
            foreach (SPolyline p in shapes)
            {
                for (int i = 0; i < p.NumParts; i++)
                {
                    int startpoint = 0;
                    int endpoint = 0;
                    if (i == p.NumParts - 1)
                    {
                        startpoint = (int)p.Parts[i];
                        endpoint = p.NumPoints;
                    }
                    else
                    {
                        startpoint = (int)p.Parts[i];
                        endpoint = (int)p.Parts[i + 1];
                    }
                    PointF[] points = new PointF[endpoint - startpoint];
                    for (int k = 0, j = startpoint; j < endpoint; j++, k++)
                    {
                        SPoint ps = p.Points[j];
                        points[k].X = (float)(4 * (50 + ps.X - head.Xmin));
                        points[k].Y = (float)(4 * (100 - ps.Y + head.Ymin));
                    }
                    e.DrawLines(pen, points);
                }
            }
        }

        private void drawPoints(Graphics e, FileHead head, List<ShapeBaseClass> shapes)
        {
            foreach (SPoint p in shapes)
            {
                PointF pp = new PointF();
                pp.X = (float)(4 * (50 + p.X - head.Xmin)); ;
                pp.Y = (float)(4 * (100 - p.Y + head.Ymin));
                e.DrawEllipse(pen, pp.X, pp.Y, 1.5f, 1.5f);
            }
        }
        //写入文本信息
       private  void WirteTxt(FileHead head)
        {
            string str = "";
            switch(head.ShapeType)
            {
                case 1:
                    str = string.Format("shpe文件类型：点\r\n");
                    break;
                case 3:
                    str = string.Format("shpe文件类型：线\r\n");
                    break;
                case 5:
                    str = string.Format("shpe文件类型：面\r\n");
                    break;
                default:
                    str = string.Format("暂不支持该文件读写\r\n");
                    break;
            }
            this.richTextBox1.Text = str;
            this.richTextBox1.ReadOnly = true;
        }
    }
}
