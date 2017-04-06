using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviDeal.Refactor
{
    public class EnviDataBeanClass : IEnviDataBean
    {
        #region 转换策略字段
        private IDataConvertStrategy dataConvertStrategy;
        public IDataConvertStrategy DataConvertStrategy
        {
            set { this.dataConvertStrategy = value; }
        }
        #endregion
        #region 像素读取策略字段
        private IReadPixelStrategy readPixelStrategy;
        public IReadPixelStrategy ReadPixelStrategy
        {
            set { this.readPixelStrategy = value; }
        }
        #endregion

        public bool Convert(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type)
        {
            if (dataConvertStrategy == null)
            {
                return false;
            }
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
                dataConvertStrategy.ConvertAlo(pixComCounts, pixLineCounts, bands, type, outputF, bts);
                return blnSuccess;
            }
        }
        public Pixel[,] GetPixInformation(string strInputFile, int pixComCounts, int pixLineCounts, int bands, int type)
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
                readPixelStrategy.GetInfos(pixComCounts, pixLineCounts, bands, type, picels, bts);
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
    }
}
