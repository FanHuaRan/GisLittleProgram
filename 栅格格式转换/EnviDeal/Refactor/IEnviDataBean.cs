using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnviDeal.Refactor
{
    /// <summary>
    /// 主接口
    /// </summary>
    public interface IEnviDataBean
    {
        IDataConvertStrategy DataConvertStrategy{ set;}
        IReadPixelStrategy ReadPixelStrategy { set; }
        bool Convert(string strInputFile, string strOutputFile, int pixComCounts, int pixLineCounts, int bands, int type);
        Pixel[,] GetPixInformation(string strInputFile, int pixComCounts, int pixLineCounts, int bands, int type);
    }
}
