using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviDeal.Refactor
{
    public class BsqToBipConvertClass : IDataConvertStrategy
    {
        public void ConvertAlo(int pixComCounts, int pixLineCounts, int bands, int type, System.IO.FileStream outputF, byte[] bts)
        {
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
        }
    }
}
