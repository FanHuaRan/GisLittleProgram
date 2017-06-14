using ShapeFileDemo.ShapeClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFileDeal.ShapeClass
{
    /// <summary>
    /// 线
    /// 2017/04/06 fhr
    /// </summary>
   public class SPolyline : ShapeBaseClass
    {
        public double[] Box = new double[4];
        public int NumParts;
        public int NumPoints;
        public List<int> Parts; //在部分中第一个点的索引
        public List<SPoint> Points; //所有部分的点
    }
}
