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
    /// 矢量坐标转换策略
    /// </summary>
    public interface IPointConvertStrategy
    {
        PointF ConvertPoint(FileHead head,SPoint spoint,double widthScale, double heightScale,int drawPanelWidth,int drawPanelHeight);
    }
}
