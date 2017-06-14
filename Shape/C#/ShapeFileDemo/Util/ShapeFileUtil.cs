using ShapeFileDeal.ShapeClass;
using ShapeFileDemo.ShapeClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeFileDemo.Util
{
    class ShapeFileUtil
    {
        /// <summary>
        /// 读取shape文件
        /// 2017/04/06 fhr
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="shapes"></param>
        /// <returns></returns>
        public static FileHead readShapeFile(String fileName, out List<ShapeBaseClass> shapes)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            using (var br = new BinaryReader(stream))
            {
                var head = readFileHead(br);
                shapes = new List<ShapeBaseClass>();
                switch (head.ShapeType)
                {
                    case 1:
                        readPoints(shapes, br);
                        break;
                    case 3:
                        readPolylines(shapes, br);
                        break;
                    case 5:
                        readPolygons(shapes, br);
                        break;
                }
                return head;
            }
        }
        /// <summary>
        /// 读取多边形
        /// </summary>
        /// <param name="shapes"></param>
        /// <param name="br"></param>
        private static void readPolygons(List<ShapeBaseClass> shapes, BinaryReader br)
        {
            while (br.PeekChar() != -1)
            {
                var polygon = new SPolygon();
                polygon.Parts = new List<int>();
                polygon.Points = new List<SPoint>();
                polygon.RecordNum = br.ReadInt32();
                polygon.DataLength = br.ReadInt32();
                //读取第i个记录
                int m = br.ReadInt32();
                for (int i = 0; i < 4; i++)
                {
                    polygon.Box[i] = br.ReadDouble();
                }
                polygon.NumParts = br.ReadInt32();
                polygon.NumPoints = br.ReadInt32();
                for (int j = 0; j < polygon.NumParts; j++)
                {
                    int parts = new int();
                    parts = br.ReadInt32();
                    polygon.Parts.Add(parts);
                }
                for (int j = 0; j < polygon.NumPoints; j++)
                {
                    var pointtemp = new SPoint();
                    pointtemp.X = br.ReadDouble();
                    pointtemp.Y = br.ReadDouble();
                    polygon.Points.Add(pointtemp);
                }
                shapes.Add(polygon);
            }
        }
        /// <summary>
        /// 读取线
        /// </summary>
        /// <param name="shapes"></param>
        /// <param name="br"></param>
        private static void readPolylines(List<ShapeBaseClass> shapes, BinaryReader br)
        {
            while (br.PeekChar() != -1)
            {
                var polyline = new SPolyline();
                polyline.Box = new double[4];
                polyline.Parts = new List<int>();
                polyline.Points = new List<SPoint>();
                polyline.RecordNum = br.ReadInt32();
                polyline.DataLength = br.ReadInt32();
                //读取第i个记录
                br.ReadInt32();
                polyline.Box[0] = br.ReadDouble();
                polyline.Box[1] = br.ReadDouble();
                polyline.Box[2] = br.ReadDouble();
                polyline.Box[3] = br.ReadDouble();
                polyline.NumParts = br.ReadInt32();
                polyline.NumPoints = br.ReadInt32();
                for (int i = 0; i < polyline.NumParts; i++)
                {
                    int parts = new int();
                    parts = br.ReadInt32();
                    polyline.Parts.Add(parts);
                }
                for (int j = 0; j < polyline.NumPoints; j++)
                {
                    var pointtemp = new SPoint();
                    pointtemp.X = br.ReadDouble();
                    pointtemp.Y = br.ReadDouble();
                    polyline.Points.Add(pointtemp);
                }
                shapes.Add(polyline);
            }
        }
        /// <summary>
        /// 读取点
        /// </summary>
        /// <param name="shapes"></param>
        /// <param name="br"></param>
        private static void readPoints(List<ShapeBaseClass> shapes, BinaryReader br)
        {
            while (br.PeekChar() != -1)
            {
                var point = new SPoint();
                point.RecordNum = br.ReadInt32();
                point.DataLength = br.ReadInt32();
                //读取第i个记录
                br.ReadInt32();
                point.X = br.ReadDouble();
                point.Y = br.ReadDouble();
                shapes.Add(point);
            }
        }

        private static FileHead readFileHead(BinaryReader br)
        {
            var head = new FileHead();
            //读取文件过程
            head.FileCode = br.ReadInt32();
            for (int i = 0; i < 5; i++)
            {
                head.Unused = br.ReadInt32();
            }
            head.FileLength = br.ReadInt32();//<0代表数据长度未知
            head.Version = br.ReadInt32();
            head.ShapeType = br.ReadInt32();
            head.Xmin = br.ReadDouble();
            head.Ymin = br.ReadDouble();
            head.Xmax = br.ReadDouble();
            head.Ymax = br.ReadDouble();
            head.Zmin = br.ReadDouble();
            head.Zmax = br.ReadDouble();
            head.Mmin = br.ReadDouble();
            head.Mmax = br.ReadDouble();
            return head;
        }
    }
}