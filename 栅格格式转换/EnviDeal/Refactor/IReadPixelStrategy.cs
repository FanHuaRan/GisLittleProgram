using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviDeal.Refactor
{
    /// <summary>
    /// 像素读取策略
    /// </summary>
    public interface IReadPixelStrategy
    {
        void GetInfos(int pixComCounts, int pixLineCounts, int bands, int type, Pixel[,] picels, byte[] bts);
    }
}
