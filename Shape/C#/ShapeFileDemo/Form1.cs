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
        #region Fileds
        //画板宽度
        private  readonly int DRAWPANELWIDTH;
        //画板高度
        private  readonly int DRAWPANELHEIGHT;
        //画笔
        private readonly Pen pen = new Pen(Color.Black, 1);
        //用于绘图的图像
       private  Bitmap map;
        //坐标转换组件
        private IPointConvertStrategy pointConvertStrategy = new PointConvertStrategy();

        #endregion
        #region Constrctor
        public Form1()
        {
            InitializeComponent();
            DRAWPANELWIDTH = this.pictureBox1.Width;
            DRAWPANELHEIGHT = this.pictureBox1.Height;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            map = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<ShapeBaseClass> shapes;
                    var head = ShapeFileUtil.readShapeFile(openFileDialog1.FileName, out shapes);
                    WriteText(head);
                    DrawShape(head, shapes);
                }
                catch(Exception er)
                {
                    MessageBox.Show("读取错误:" + er.Message);
                }
            }
        }

        private void DrawShape(FileHead head, List<ShapeBaseClass> shapes)
        {
            var e = Graphics.FromImage(map);
            Draw(e, head, shapes);
            this.pictureBox1.Image = map;
        }
        private void Draw(Graphics e,FileHead head, List<ShapeBaseClass> shapes)
        {
            //宽度比例尺
            var widthScale = DRAWPANELWIDTH / (head.Xmax - head.Xmin);
            //高度比例尺
            var heightScale = DRAWPANELHEIGHT / (head.Ymax - head.Ymin);
            switch (head.ShapeType)
            {
                case 1://点类型
                    drawPoints(e, head, shapes,widthScale,heightScale);
                    break;
                case 3://线类型
                    drawPolylines(e, head, shapes, widthScale, heightScale);
                    break;
                case 5://面类型
                    drawPolygons(e, head, shapes, widthScale, heightScale);
                    break;
            }
        }
        /// <summary>
        /// 画多边形  
        /// 实际上shape结构当中多边形和线的结构一样，所以这儿设计的多边形是线的子类
        /// 顾可以使用绘制线的方法绘制多边形
        /// </summary>
        /// <param name="e"></param>
        /// <param name="head"></param>
        /// <param name="shapes"></param>
        /// <param name="widthScale"></param>
        /// <param name="heightScale"></param>
        private void drawPolygons(Graphics e, FileHead head, List<ShapeBaseClass> shapes, double widthScale, double heightScale)
        {
            drawPolylines(e, head, shapes, widthScale, heightScale);
        }
        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="e"></param>
        /// <param name="head"></param>
        /// <param name="shapes"></param>
        /// <param name="widthScale"></param>
        /// <param name="heightScale"></param>
        private void drawPolylines(Graphics e, FileHead head, List<ShapeBaseClass> shapes, double widthScale, double heightScale)
        {
            foreach (SPolyline spolyline in shapes)
            {
                for (int i = 0; i < spolyline.NumParts; i++)
                {
                    int startpoint = 0;
                    int endpoint = 0;
                    if (i == spolyline.NumParts - 1)
                    {
                        startpoint = (int)spolyline.Parts[i];
                        endpoint = spolyline.NumPoints;
                    }
                    else
                    {
                        startpoint = (int)spolyline.Parts[i];
                        endpoint = (int)spolyline.Parts[i + 1];
                    }
                    var points = new PointF[endpoint - startpoint];
                    for (int k = 0, j = startpoint; j < endpoint; j++, k++)
                    {
                        points[k] = pointConvertStrategy.ConvertPoint(head, spolyline.Points[j], widthScale, heightScale, DRAWPANELHEIGHT, DRAWPANELWIDTH);
                    }
                    e.DrawLines(pen, points);
                }
            }
        }

        private void drawPoints(Graphics e, FileHead head, List<ShapeBaseClass> shapes,double widthScale,double heightScale)
        {
            foreach (SPoint spoint in shapes)
            {
                var point = pointConvertStrategy.ConvertPoint(head, spoint, widthScale, heightScale,DRAWPANELHEIGHT,DRAWPANELWIDTH);
                 e.DrawEllipse(pen, point.X, point.Y, 10f, 10f);
            }
        }
        //写入文本信息
       private  void WriteText(FileHead head)
        {
            var str = "";
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
