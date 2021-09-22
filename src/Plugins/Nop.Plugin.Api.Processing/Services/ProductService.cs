using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Plugin.Api.Processing.Models;
using Nop.Services.Media;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Api.Processing.Services
{
    public static class ProductService
    {
        public static async Task<bool> InsertProductPictureAsync(IList<FileData> filesData, int displayOrder = 1)
        {
            var pictureService = EngineContext.Current.Resolve<IPictureService>();

            foreach(var fileData in filesData)
            {
                byte[] bytes = Convert.FromBase64String(fileData.FileContentBase64);

                var pic = await pictureService.InsertPictureAsync(bytes, MimeTypes.ImageJpeg, await pictureService.GetPictureSeNameAsync(fileData.ProductName));

                await InsertInstallationDataAsync(
                    new ProductPicture
                    {
                        ProductId = fileData.ProductId,
                        PictureId = pic.Id,
                        DisplayOrder = displayOrder
                    });
            }

            return true;
        }

        private static async Task<T> InsertInstallationDataAsync<T>(T entity) where T : BaseEntity
        {
            return await _dataProvider.InsertEntityAsync(entity);
        }
    }
}
