using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Processing.Models
{
    public class FileData
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string FileName { get; set; }

        public string FileContentBase64 { get; set; }

        public string FileExtension { get; set; }
    }
}
