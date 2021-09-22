using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Infrastructure;
using Nop.Plugin.Api.Processing.Constants;
using Nop.Plugin.Api.Processing.Models;
using Nop.Services.ExportImport;
using Nop.Services.Logging;
using Nop.Plugin.Api.Processing.Services;

namespace Nop.Plugin.Api.Processing.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IImportManager _importManager;
        private readonly IProductProcessingService _productProcessingService;
        // private IWebHostEnvironment _env;
        private readonly INopFileProvider _fileProvider;
        private readonly ILogger _logger;

        private const string UPLOADS_TEMP_PATH = "~/App_Data/TempUploads";

        public ProductsController(
            IImportManager importManager,
            INopFileProvider fileProvider,
            IProductProcessingService productProcessingService,
            ILogger logger
            // IWebHostEnvironment env
            )
            : base()
        {
            this._importManager = importManager;
            this._logger = logger;
            this._fileProvider = fileProvider;
            this._productProcessingService = productProcessingService;
           // this._env = env;
        }

        [HttpPost]
        [Route("/api/products/uploadFromXlsx")]
        public async Task<IActionResult> UploadProductsFromXlsx(
           [FromHeader(Name = HeaderConstants.AuthToken)][Required] string x_AUTH_TOKEN, 
           [FromBody] FileData fileData)
        {

            if (x_AUTH_TOKEN != GetAuthToken())
            {
                // this.logger.LogError("Invalid authentication token");
                return new ObjectResult(HttpConstants.UnauthorizedDescription)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Value = new ErrorResponse { ErrorCode = "Unauthorized", Message = "Unauthorized" }
                };
            }

            try
            {

                
                var fileId = Guid.NewGuid().ToString();
                await _logger.InformationAsync("fileId created");
                var filePath = $"{_fileProvider.MapPath(UPLOADS_TEMP_PATH)}/{fileId}_{fileData.FileName}";
                await _logger.InformationAsync("filepath created");

                byte[] bytes = Convert.FromBase64String(fileData.FileContentBase64);
                await _logger.InformationAsync("converted to byte array");

                System.IO.File.WriteAllBytes(filePath, bytes);
                await _logger.InformationAsync("file written to path");
                using (FileStream fs = System.IO.File.Open(filePath, FileMode.Open))
                {
                    await _logger.InformationAsync("importing xlsx async");
                    await _importManager.ImportProductsFromXlsxAsync(fs);
                    await _logger.InformationAsync("imported");
                }

                await _logger.InformationAsync("deleting file");
                System.IO.File.Delete(filePath);
                await _logger.InformationAsync("file deleted");
                return new ObjectResult(HttpConstants.OkDescription)
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Value = "products uploaded successfully"
                };
            }
            catch (Exception ex)
            {
                await _logger.ErrorAsync(ex.Message, ex);
                return new ObjectResult(HttpConstants.InternalServerErrorDescription)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Value = new ErrorResponse { ErrorCode = "InternalServerError", Message = "Internal Server Error" }
                };
            }
        }

        [HttpPost]
        [Route("/api/file/uploadProductImage")]
        public async Task<IActionResult> UploadProductImage(
         [FromHeader(Name = HeaderConstants.AuthToken)][Required] string x_AUTH_TOKEN,
        [FromBody] IList<FileData> filesData)
        {

            if (x_AUTH_TOKEN != GetAuthToken())
            {
                // this.logger.LogError("Invalid authentication token");
                return new ObjectResult(HttpConstants.UnauthorizedDescription)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Value = new ErrorResponse { ErrorCode = "Unauthorized", Message = "Unauthorized" }
                };
            }

            try
            {

                await this._productProcessingService.InsertProductPictureAsync(filesData);


                return new ObjectResult(HttpConstants.OkDescription)
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Value = "product image uploaded successfully"
                };
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex, "Internal Server Error");
                return new ObjectResult(HttpConstants.InternalServerErrorDescription)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Value = new ErrorResponse { ErrorCode = "InternalServerError", Message = "Internal Server Error" }
                };
            }
        }


        [HttpGet]
        [Route("/api/products/status")]
        public IActionResult ProductStatus(
           [FromHeader(Name = HeaderConstants.AuthToken)][Required] string x_AUTH_TOKEN)
        {

            if (x_AUTH_TOKEN != GetAuthToken())
            {
                // this.logger.LogError("Invalid authentication token");
                return new ObjectResult(HttpConstants.UnauthorizedDescription)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Value = new ErrorResponse { ErrorCode = "Unauthorized", Message = "Unauthorized" }
                };
            }

            try
            {
                return new ObjectResult(HttpConstants.OkDescription)
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Value = "coolio"
                };
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex, "Internal Server Error");
                return new ObjectResult(HttpConstants.InternalServerErrorDescription)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Value = new ErrorResponse { ErrorCode = "InternalServerError", Message = "Internal Server Error" }
                };
            }
        }

        private string GetAuthToken()
        {
            return "B5936C70-1F36-45EA-9FE7-CF42D0FAE84C";
            //return this.configuration.GetValue<String>("FinanceApiAuthToken");
        }

    }
}
