using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Api.Processing.Models;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Discounts;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Api.Processing.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class ApiProcessingController : BasePluginController
    {
        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        public ApiProcessingController(
            ICustomerService customerService,
           ILocalizationService localizationService,
           IPermissionService permissionService,
           ISettingService settingService)
        {
            _customerService = customerService;
            _localizationService = localizationService;
            _permissionService = permissionService;
            _settingService = settingService;
        }
        public async Task<IActionResult> Configure()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.AccessAdminPanel))
                return Content("Access denied");

            //prepare model
            var model = new ConfigureViewModel
            {
               
            };

          
            return View("~/Plugins/Api.Processing/Views/Configure.cshtml", model);
        }
    }
}
