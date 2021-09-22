using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Api.Processing.Models;

namespace Nop.Plugin.Api.Processing.Services
{
    public interface IProductProcessingService
    {
        Task<bool> InsertProductPictureAsync(IList<FileData> filesData, int displayOrder = 1);
    }
}
