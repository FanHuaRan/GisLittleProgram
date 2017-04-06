using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFileDemo.ShapeClass
{
    /// <summary>
    /// 文件头信息
    /// 2017/04/06 fhr
    /// </summary>
    public class FileHead
    {
       public int FileCode;
       public int Unused;
       public  int FileLength;
       public int Version;
       public int ShapeType;
       public  double Xmin;
       public double Ymin;
       public double Xmax;
       public double Ymax;
       public double Zmin;
       public double Zmax;
       public  double Mmin;
       public double Mmax;
    }
}
