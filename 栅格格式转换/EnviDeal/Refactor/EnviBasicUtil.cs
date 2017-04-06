using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviDeal.Refactor
{
    class EnviBasicUtil
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
    }
}
