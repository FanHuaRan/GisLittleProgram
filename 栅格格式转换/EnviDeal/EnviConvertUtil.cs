using EnviDeal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrFan.Tool.EnviDeal
{
    /// <summary>
    /// envi文件转换程序
    /// 2017/04/06 fhr
    /// </summary>
    public class EnviConvertUtil
    {
        /// <summary>
        /// 读取头文件信息
        /// </summary>
        /// <param name="strFileName"></param>头文件路径和名称
        /// <param name="iColumnsCount"></param>像素列数
        /// <param name="iLinesCount"></param>像素行数
        /// <param name="iBandsCount"></param>波段数
        /// <param name="iType"></param>基本数据类型代码
        /// <param name="strInterLeave"></param>文件组织格式
        /// <returns></returns>返回是否读取成功
        public static bool ReadHDR(String strFileName, ref int iColumnsCount, ref int iLinesCount, ref int iBandsCount, ref int iType, ref String strInterLeave)
        {
            var blnSuccess = false;
            iColumnsCount = -1;
            iLinesCount = -1;
            iBandsCount = -1;
            iType = -1;
            strInterLeave = "";
            //初始化各个变量
            using (var hdrfile = new StreamReader(strFileName))
            {
                var content = "";
                while (hdrfile.EndOfStream != true)
                {
                    content = hdrfile.ReadLine();
                    //获取像素列数
                    if (content.Contains("samples"))
                    {
                        var samples = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        iColumnsCount = Convert.ToInt32(samples);
                    }
                    //获取像素行数
                    else if (content.Contains("lines"))
                    {
                        var lines = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        iLinesCount = Convert.ToInt32(lines);
                    }
                    //获取波段个数
                    else if (content.Contains("bands"))
                    {
                        var bands = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        iBandsCount = Convert.ToInt32(bands);
                    }
                    //获取数据种类
                    else if (content.Contains("data type"))
                    {
                        var type = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        iType = Convert.ToInt32(type);
                    }
                    //获取数据解译方式
                    else if (content.Contains("interleave"))
                    {
                        var interleve = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        strInterLeave = interleve;
                        blnSuccess = true;
                    }
                }
                return blnSuccess;
            }
        }
        /// <summary>
        /// 另存新格式头文件
        /// </summary>
        /// <param name="origiPath"></param>
        /// <param name="outPath"></param>
        /// <param name="newType"></param>
        public static void SaveHDR(string origiPath, string outPath, string newType)
        {
            newType = newType.ToLower();
            if (newType != "bsq" && newType != "bil" && newType != "bip")
            {
                throw new Exception("文件格式不符合要求");
            }
            try
            {
                using (var reader = new StreamReader(origiPath))
                using (var fileStream = new FileStream(outPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var writer = new StreamWriter((Stream)fileStream);
                    while (!reader.EndOfStream)
                    {
                        string rowStr = reader.ReadLine();
                        if (rowStr.Contains("interleave"))
                        {
                            var strArrys = rowStr.Split('=');
                           rowStr = rowStr.Replace(strArrys[1], " " + newType);
                        }
                        writer.WriteLine(rowStr);
                    }
                }
            }
            catch (Exception er)
            {
                Debug.Print(er.Message);
                throw er;
            }
        }
         /// <summary>
        /// bip转换为bsq
        /// </summary>
        /// <param name="strInputFile"></param>源文件名称与路径
        /// <param name="strOutputFile"></param>目标文件名称与路径
        /// <param name="pixComCounts"></param>像素行数
        /// <param name="pixLineCounts"></param>像素列数
        /// <param name="bands"></param>波段数
        /// <param name="type"></param>基本数据类型代码 1为1字节 byte 2为2字节 short
        /// <returns></returns>是否转换成功
        public static bool BipToBsq(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            var blnSuccess = true;
            using (var inputF = new FileStream(strInputFile, FileMode.Open))
            using (var outputF = new FileStream(strOutputFile, FileMode.CreateNew))
            {
                    var totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
                    if (totalsize != inputF.Length)
                    {
                        return false;
                    }
                    var bts = readFileBytes(inputF, totalsize);
                    //按波段写入
                    for (int bandnum = 0; bandnum < bands; bandnum++)
                    {
                        //按行写入
                        for (int row = 0; row < pixLineCounts; row++)
                        {
                            //按列写入
                            for (int columnum = 0; columnum < pixComCounts; columnum++)
                            {
                                int startpos = pixComCounts * type * bands * row + columnum * type * bands + bandnum * type;
                                //按数据基本单元类型输入数据
                                for (int typenum = 0; typenum < type; typenum++)
                                {
                                    outputF.WriteByte(bts[startpos + typenum]);
                                }
                            }
                        }
                    }
                    return blnSuccess;
            }
        }
        /// <summary>
        /// bip转换为bil
        /// </summary>
        /// <param name="strInputFile"></param>
        /// <param name="strOutputFile"></param>
        /// <param name="pixComCounts"></param>
        /// <param name="pixLineCounts"></param>
        /// <param name="bands"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool BipToBil(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            var blnSuccess = true;
            using (var inputF = new FileStream(strInputFile, FileMode.Open))
            using (var outputF = new FileStream(strOutputFile, FileMode.CreateNew))
            {
                    var totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
                    if (totalsize != inputF.Length)
                    {
                        return false;
                    }
                    var bts = readFileBytes(inputF, totalsize);
                    //按行写入
                    for (int row = 0; row < pixLineCounts; row++)
                    {
                        //按波段写入
                        for (int bandnum = 0; bandnum < bands; bandnum++)
                        {
                            //按列写入
                            for (int columnum = 0; columnum < pixComCounts; columnum++)
                            {
                                var startpos = pixComCounts * row * bands * type + columnum * bands * type + bandnum * type;
                                //按数据基本单元类型输入数据
                                for (int typenum = 0; typenum < type; typenum++)
                                {
                                    outputF.WriteByte(bts[startpos + typenum]);
                                }
                            }
                        }
                    }
                return blnSuccess;
            }
        }
        /// <summary>
        /// bsq转换为bil
        /// </summary>
        /// <param name="strInputFile"></param>
        /// <param name="strOutputFile"></param>
        /// <param name="pixComCounts"></param>
        /// <param name="pixLineCounts"></param>
        /// <param name="bands"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool BsqToBil(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            var blnSuccess = true;
            using (var inputF = new FileStream(strInputFile, FileMode.Open))
            using (var outputF = new FileStream(strOutputFile, FileMode.CreateNew))
            {
                var totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
                if (totalsize != inputF.Length)
                {
                    return false;
                }
                var bts = readFileBytes(inputF, totalsize);
                //按行写入数据
                for (int row = 0; row < pixLineCounts; row++)
                {
                    //按波段写入数据
                    for (int bandnum = 0; bandnum < bands; bandnum++)
                    {
                        int startpos = pixComCounts * pixLineCounts * type * bandnum + row * pixComCounts * type;
                        //写入每一列数据
                        for (int columnum = 0; columnum < pixComCounts; columnum++)
                        {
                            //按数据基本单元类型输入数据
                            for (int typenum = 0; typenum < type; typenum++)
                            {
                                outputF.WriteByte(bts[startpos + columnum * type + typenum]);
                            }
                        }
                    }
                }
                return blnSuccess;
            }
        }
        /// <summary>
        /// bsq转换为bip
        /// </summary>
        /// <param name="strInputFile"></param>
        /// <param name="strOutputFile"></param>
        /// <param name="pixComCounts"></param>
        /// <param name="pixLineCounts"></param>
        /// <param name="bands"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool BsqToBip(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            var blnSuccess = true;
            using (var inputF = new FileStream(strInputFile, FileMode.Open))
            using (var outputF = new FileStream(strOutputFile, FileMode.CreateNew))
            {
                var totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
                if (totalsize != inputF.Length)
                {
                    return false;
                }
                var bts = readFileBytes(inputF, totalsize);
                //按行写入
                for (int row = 0; row < pixLineCounts; row++)
                {
                    //按列写入
                    for (int columnum = 0; columnum < pixComCounts; columnum++)
                    {
                        //按波段写入
                        for (int bandnum = 0; bandnum < bands; bandnum++)
                        {
                            int startpos = pixComCounts * pixLineCounts * type * bandnum + row * pixComCounts * type + columnum * type;
                            //按数据基本单元类型输入数据
                            for (int typenum = 0; typenum < type; typenum++)
                            {
                                outputF.WriteByte(bts[startpos + typenum]);
                            }
                        }
                    }
                }
                return blnSuccess;
            }
        }
        /// <summary>
        /// bil转换为bip
        /// </summary>
        /// <param name="strInputFile"></param>
        /// <param name="strOutputFile"></param>
        /// <param name="pixComCounts"></param>
        /// <param name="pixLineCounts"></param>
        /// <param name="bands"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool BilToBip(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            var blnSuccess = true;
            using (var inputF = new FileStream(strInputFile, FileMode.Open))
            using (var outputF = new FileStream(strOutputFile, FileMode.CreateNew))
            {
                var totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
                if (totalsize != inputF.Length)
                {
                    return false;
                }
                var bts = readFileBytes(inputF, totalsize);
                //按行写入
                for (int row = 0; row < pixLineCounts; row++)
                {
                    //按列写入
                    for (int columnum = 0; columnum < pixComCounts; columnum++)
                    {
                        //按波段写入
                        for (int bandnum = 0; bandnum < bands; bandnum++)
                        {
                            int startpos = pixComCounts * type * row * bands + pixComCounts * type * bandnum + type * columnum;//获取基准位置
                            //按数据基本单元类型输入数据
                            for (int typenum = 0; typenum < type; typenum++)
                            {
                                outputF.WriteByte(bts[startpos + typenum]);
                            }
                        }
                    }
                }
                return blnSuccess;
            }
        }
        /// <summary>
        /// Bil转换为Bsq
        /// </summary>
        /// <param name="strInputFile"></param>
        /// <param name="strOutputFile"></param>
        /// <param name="pixComCounts"></param>
        /// <param name="pixLineCounts"></param>
        /// <param name="bands"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool BilToBsq(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            var blnSuccess = true;
            using (var inputF = new FileStream(strInputFile, FileMode.Open))
            using (var outputF = new FileStream(strOutputFile, FileMode.CreateNew))
            {
                var totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
                if (totalsize != inputF.Length)
                {
                    return false;
                }
                var bts = readFileBytes(inputF, totalsize);
                //读取波段写入
                for (int bandnum = 0; bandnum < bands; bandnum++)
                {
                    //按行写入
                    for (int row = 0; row < pixLineCounts; row++)
                    {
                        //按列写入
                        for (int columnum = 0; columnum < pixComCounts; columnum++)
                        {
                            int startpos = pixComCounts * type * row * bands + pixComCounts * type * bandnum + type * columnum;//获取基准位置
                            //按数据基本单元类型输入数据
                            for (int typenum = 0; typenum < type; typenum++)
                            {
                                outputF.WriteByte(bts[startpos + typenum]);
                            }
                        }
                    }
                }
                return blnSuccess;
            }
        }
        /// <summary>
        /// 从bsq文件中获取像素信息
        /// </summary>
        /// <param name="strInputFile"></param>
        /// <param name="pixComCounts"></param>
        /// <param name="pixLineCounts"></param>
        /// <param name="bands"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Pixel[,] GetPixInformationFromBsq(string strInputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            var picels = new Pixel[pixLineCounts, pixComCounts];
            using (var inputF = new FileStream(strInputFile, FileMode.Open))
            {
                int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
                if (totalsize != inputF.Length)
                {
                    return null;
                }
                var bts = readFileBytes(inputF, totalsize);
                //按行写入
                for (int row = 0; row < pixLineCounts; row++)
                {
                    //按列写入
                    for (int columnum = 0; columnum < pixComCounts; columnum++)
                    {
                        picels[row, columnum] = new Pixel();
                        picels[row, columnum].ColorNum = new int[bands];
                        //按波段写入
                        for (int bandnum = 0; bandnum < bands; bandnum++)
                        {
                            int startpos = pixComCounts * pixLineCounts * type * bandnum + row * pixComCounts * type + columnum * type;
                            //按数据基本单元类型输入数据
                            int value = 0;
                            for (int i = 0; i < type; i++)
                            {
                                value += bts[startpos + i] << 8 * (type - i - 1);
                            }
                            picels[row, columnum].ColorNum[bandnum] = value;
                        }
                    }
                }
                return picels;
            }
        }
        /// <summary>
        /// 从bil文件中获取像素信息
        /// </summary>
        /// <param name="strInputFile"></param>
        /// <param name="pixComCounts"></param>
        /// <param name="pixLineCounts"></param>
        /// <param name="bands"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Pixel[,] GetPixInformationFromBil(string strInputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            var picels = new Pixel[pixLineCounts, pixComCounts];
            using (var inputF = new FileStream(strInputFile, FileMode.Open))
            {
                int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
                if (totalsize != inputF.Length)
                {
                    return null;
                }
                var bts = readFileBytes(inputF, totalsize);
                //按行写入
                for (int row = 0; row < pixLineCounts; row++)
                {
                    //按列写入
                    for (int columnum = 0; columnum < pixComCounts; columnum++)
                    {
                        picels[row, columnum] = new Pixel();
                        picels[row, columnum].ColorNum = new int[bands];
                        //按波段写入
                        for (int bandnum = 0; bandnum < bands; bandnum++)
                        {
                            int startpos = pixComCounts * type * row * bands + pixComCounts * type * bandnum + type * columnum;//获取基准位置
                            //按数据基本单元类型输入数据
                            int value = 0;
                            for (int i = 0; i < type; i++)
                            {
                                value += bts[startpos + i] << 8 * (type - i - 1);
                            }
                            picels[row, columnum].ColorNum[bandnum] = value;
                        }
                    }
                }
                return picels;
            }
        }
        /// <summary>
        /// 从bip文件中获取像素信息
        /// </summary>
        /// <param name="strInputFile"></param>
        /// <param name="pixComCounts"></param>
        /// <param name="pixLineCounts"></param>
        /// <param name="bands"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Pixel[,] GetPixInformationFromBip(string strInputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
           var picels = new Pixel[pixLineCounts, pixComCounts];
           using (var inputF = new FileStream(strInputFile, FileMode.Open))
           {
               int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
               if (totalsize != inputF.Length)
               {
                   return null;
               }
               var bts = readFileBytes(inputF, totalsize);
               //按行写入
               for (int row = 0; row < pixLineCounts; row++)
               {
                   //按列写入
                   for (int columnum = 0; columnum < pixComCounts; columnum++)
                   {
                       picels[row, columnum] = new Pixel();
                       picels[row, columnum].ColorNum = new int[bands];
                       //按波段写入
                       for (int bandnum = 0; bandnum < bands; bandnum++)
                       {
                           int startpos = row * pixComCounts * bands * type + columnum * bands * type + type * bandnum;
                           //按数据基本单元类型输入数据
                           int value = 0;
                           for (int i = 0; i < type; i++)
                           {
                               value += bts[startpos + i] << 8 * (type - i - 1);
                           }
                           picels[row, columnum].ColorNum[bandnum] = value;
                       }
                   }
               }
               return picels;
           }
        }
        /// <summary>
        /// 读取字节文件
        /// </summary>
        /// <param name="inputF"></param>
        /// <param name="totalsize"></param>
        /// <returns></returns>
        private static byte[] readFileBytes(FileStream inputF, int totalsize)
        {
            byte[] bts = new byte[totalsize];
            int num = 0, bt;
            //读取出全部字节数据，存储在数组中
            while ((bt = inputF.ReadByte()) > -1)
            {
                bts[num] = (byte)bt;
                num++;
            }
            return bts;
        }
        /// <summary>
        /// 关闭相关文件
        /// </summary>
        /// <param name="inputF"></param>
        /// <param name="outputF"></param>
        private static void closeFiles(FileStream inputF, FileStream outputF)
        {
            outputF.Flush();//保存缓存文件
            outputF.Close();//关闭撤销变量文件
            outputF.Dispose();
            inputF.Close();
            inputF.Dispose();
        }
    }
}
