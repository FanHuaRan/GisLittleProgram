using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviDeal.Refactor.Readpixels
{
    public class ReadPixelBilClass : IReadPixelStrategy
    {
        public void GetInfos(int pixComCounts, int pixLineCounts, int bands, int type, Pixel[,] picels, byte[] bts)
        {
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
        }
    }
}
