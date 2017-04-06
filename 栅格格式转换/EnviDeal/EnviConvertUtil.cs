using EnviDeal;
using System;
using System.Collections.Generic;
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
            bool blnSuccess = false;
            iColumnsCount = -1;
            iLinesCount = -1;
            iBandsCount = -1;
            iType = -1;
            strInterLeave = "";
            //初始化各个变量
            StreamReader hdrfile = null;
            try
            {
                hdrfile = new StreamReader(strFileName);
                string content = "";
                while (hdrfile.EndOfStream != true)
                {//获取像素列数
                    content = hdrfile.ReadLine();
                    if (content.Contains("samples"))
                    {
                        String samples = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        iColumnsCount = Convert.ToInt32(samples);
                        break;
                    }
                }
                while (hdrfile.EndOfStream != true)
                {//获取像素行数
                    content = hdrfile.ReadLine();
                    if (content.Contains("lines"))
                    {
                        String lines = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        iLinesCount = Convert.ToInt32(lines);
                        break;
                    }
                }
                while (hdrfile.EndOfStream != true)
                {//获取波段个数
                    content = hdrfile.ReadLine();
                    if (content.Contains("bands"))
                    {
                        String bands = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        iBandsCount = Convert.ToInt32(bands);
                        break;
                    }
                }
                while (hdrfile.EndOfStream != true)
                { //获取数据种类
                    content = hdrfile.ReadLine();
                    if (content.Contains("data type"))
                    {
                        String type = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        iType = Convert.ToInt32(type);
                        break;
                    }
                }
                while (hdrfile.EndOfStream != true)
                { //获取数据解译方式
                    content = hdrfile.ReadLine();
                    if (content.Contains("interleave"))
                    {
                        String interleve = content.Substring(content.IndexOf("=") + 1, content.Length - content.IndexOf("=") - 1).Trim();
                        strInterLeave = interleve;
                        blnSuccess = true;
                        break;
                    }
                }
            }
            catch
            {//读取失败
                hdrfile.Close();
                hdrfile.Dispose();
                return false;
            }
            hdrfile.Close();
            hdrfile.Dispose();
            //关闭文件流，释放内存
            return blnSuccess;
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
                throw new Exception("文件格式不符合要求");
            try
            {
                using (StreamReader reader = new StreamReader(origiPath))
                using (FileStream fileStream = new FileStream(outPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter((Stream)fileStream);
                    while (!reader.EndOfStream)
                    {
                        string rowStr = reader.ReadLine();
                        if (rowStr.Contains("interleave"))
                        {
                           string[] strArrys = rowStr.Split('=');
                           rowStr = rowStr.Replace(strArrys[1], " " + newType);
                        }
                        writer.WriteLine(rowStr);
                    }
                    writer.Close();
                    writer.Dispose();
                }
            }
            catch (IOException er)
            {
                throw new Exception("文件操作异常");
            }
            catch (Exception er)
            {
                throw new Exception();
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
            bool blnSuccess = true;
            FileStream inputF = new FileStream(strInputFile, FileMode.Open);
            FileStream outputF = new FileStream(strOutputFile, FileMode.CreateNew);
            int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
            if (totalsize != inputF.Length)
            {
                return false;
            }
            byte[] bts = new byte[totalsize];
            int num = 0, bt;
            //读取出全部字节数据，存储在数组中
            while ((bt = inputF.ReadByte()) > -1)
            {
                bts[num] = (byte)bt;
                num++;
            }
            //   BinaryReader br = new BinaryReader(); 使用这个

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
            closeFiles(inputF, outputF);
            return blnSuccess;
        }
        public static bool BipToBil(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            bool blnSuccess = true;
            FileStream inputF = new FileStream(strInputFile, FileMode.Open);
            FileStream outputF = new FileStream(strOutputFile, FileMode.CreateNew);
            int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
            if (totalsize != inputF.Length)
            {
                return false;
            }
            byte[] bts = new byte[totalsize];
            int num = 0, bt;
            //读取出全部字节数据，存储在数组中
            while ((bt = inputF.ReadByte()) > -1)
            {
                bts[num] = (byte)bt;
                num++;
            }
            //按行写入
            for (int row = 0; row < pixLineCounts; row++)
            {
                //按波段写入
                for (int bandnum = 0; bandnum < bands; bandnum++)
                {
                    //按列写入
                    for (int columnum = 0; columnum < pixComCounts; columnum++)
                    {
                        int startpos = pixComCounts * row * bands * type + columnum * bands * type + bandnum * type;
                        //按数据基本单元类型输入数据
                        for (int typenum = 0; typenum < type; typenum++)
                        {
                            outputF.WriteByte(bts[startpos + typenum]);
                        }
                    }
                }
            }
            closeFiles(inputF, outputF);
            return blnSuccess;
        }
        public static bool BsqToBil(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            bool blnSuccess = true;
            FileStream inputF = new FileStream(strInputFile, FileMode.Open);
            FileStream outputF = new FileStream(strOutputFile, FileMode.CreateNew);
            int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
            if (totalsize != inputF.Length)
            {
                return false;
            }
            byte[] bts = new byte[totalsize];
            int num = 0, bt;
            //读取出全部字节数据，存储在数组中
            while ((bt = inputF.ReadByte()) > -1)
            {
                bts[num] = (byte)bt;
                num++;
            }
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
            closeFiles(inputF, outputF);
            return blnSuccess;
        }
        public static bool BsqToBip(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            bool blnSuccess = true;
            FileStream inputF = new FileStream(strInputFile, FileMode.Open);
            FileStream outputF = new FileStream(strOutputFile, FileMode.CreateNew);
            int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
            if (totalsize != inputF.Length)
            {
                return false;
            }
            byte[] bts = new byte[totalsize];
            int num = 0, bt = 0;
            while ((bt = inputF.ReadByte()) > -1)
            {//读取出全部字节数据，存储在数组中
                bts[num] = (byte)bt;
                num++;
            }
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
            closeFiles(inputF, outputF);
            return blnSuccess;
        }
        public static bool BilToBip(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            bool blnSuccess = true;
            FileStream inputF = new FileStream(strInputFile, FileMode.Open);
            FileStream outputF = new FileStream(strOutputFile, FileMode.CreateNew);
            int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
            if (totalsize != inputF.Length)
            {
                return false;
            }
            byte[] bts = new byte[totalsize];
            int num = 0, bt;
            //读取出全部字节数据，存储在数组中
            while ((bt = inputF.ReadByte()) > -1)
            {
                bts[num] = (byte)bt;
                num++;
            }
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
            closeFiles(inputF, outputF);
            return blnSuccess;
        }
        public static bool BilToBsq(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            bool blnSuccess = true;
            FileStream inputF = new FileStream(strInputFile, FileMode.Open);
            FileStream outputF = new FileStream(strOutputFile, FileMode.CreateNew);
            int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
            if (totalsize != inputF.Length)
            {
                return false;
            }
            byte[] bts = new byte[totalsize];
            int num = 0, bt;
            while ((bt = inputF.ReadByte()) > -1)
            {//读取出全部字节数据，存储在数组中
                bts[num] = (byte)bt;
                num++;
            }
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
            closeFiles(inputF, outputF);
            return blnSuccess;
        }
        public static Pixel[,] GetPixInformationFromBsq(string strInputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            Pixel[,] picels = new Pixel[pixLineCounts, pixComCounts];
            FileStream inputF = new FileStream(strInputFile, FileMode.Open);
            int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
            if (totalsize != inputF.Length)
            {
                return null;
            }
            byte[] bts = new byte[totalsize];
            int num = 0, bt;
            //读取出全部字节数据，存储在数组中
            while ((bt = inputF.ReadByte()) > -1)
            {
                bts[num] = (byte)bt;
                num++;
            }
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
            inputF.Close();
            return picels;
        }
        public static Pixel[,] GetPixInformationFromBil(string strInputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            Pixel[,] picels = new Pixel[pixLineCounts, pixComCounts];
            FileStream inputF = new FileStream(strInputFile, FileMode.Open);
            int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
            if (totalsize != inputF.Length)
            {
                return null;
            }
            byte[] bts = new byte[totalsize];
            int num = 0, bt;
            //读取出全部字节数据，存储在数组中
            while ((bt = inputF.ReadByte()) > -1)
            {
                bts[num] = (byte)bt;
                num++;
            }
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
            inputF.Close();
            return picels;
        }
        public static Pixel[,] GetPixInformationFromBip(string strInputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            Pixel[,] picels = new Pixel[pixLineCounts, pixComCounts];
            FileStream inputF = new FileStream(strInputFile, FileMode.Open);
            int totalsize = pixComCounts * pixLineCounts * bands * type;//计算输入文件总字节数
            if (totalsize != inputF.Length)
            {
                return null;
            }
            byte[] bts = new byte[totalsize];
            int num = 0, bt;
            //读取出全部字节数据，存储在数组中
            while ((bt = inputF.ReadByte()) > -1)
            {
                bts[num] = (byte)bt;
                num++;
            }
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
            inputF.Close();
            return picels;
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
