using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Services.Common;
using Nop.Services.Plugins;

namespace Nop.Plugin.Api.Processing
{
    public class ApiProcessing : BasePlugin, IMiscPlugin
    {
        public ApiProcessing()
        {

        }

        public override async Task InstallAsync()
        {
           await base.InstallAsync();
        }

        public override async Task UninstallAsync()
        {
          await base.UninstallAsync();
        }

        public override async Task UpdateAsync(string currentVersion, string targetVersion)
        {
           await base.UpdateAsync(currentVersion, targetVersion);
        }

    }
}
