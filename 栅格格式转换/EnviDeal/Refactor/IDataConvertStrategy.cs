using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviDeal.Refactor
{
    /// <summary>
    /// 数据转换策略
    /// </summary>
    public interface IDataConvertStrategy
    {
        void ConvertAlo(int pixComCounts, int pixLineCounts, int bands, int type, FileStream outputF, byte[] bts);
    }
}
