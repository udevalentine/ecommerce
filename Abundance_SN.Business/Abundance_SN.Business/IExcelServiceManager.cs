using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundance_SN.Business
{
    public interface IExcelServiceManager
    {
        MemoryStream DownloadExcel<T>(List<T> list) where T : class;
        DataSet UploadExcel(string filePath);
    }
}
