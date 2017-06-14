using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShapeFileDeal.ShapeClass;
using ShapeFileDemo.ShapeClass;

namespace ShapeFileDemo
{
    /// <summary>
    /// 矢量坐标转换实现
    /// </summary>
    public class PointConvertStrategy : IPointConvertStrategy
    {
         //宽度起始量
        private  const float XSTART = 10;
        //高度起始量
        private const float YSTART = 10;

        public PointF ConvertPoint(FileHead head, SPoint spoint, double widthScale, double heightScale, int drawPanelWidth, int drawPanelHeight)
        {
            PointF point = new PointF();
            point.X = (XSTART+(float)(widthScale * (spoint.X - head.Xmin)))/2;
            point.Y = (YSTART+drawPanelHeight - (float)(heightScale * (spoint.Y - head.Ymin)))/2;
            return point;
        }
    }
}
