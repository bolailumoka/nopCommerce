using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Api.Processing
{
    public class ApiStartup : INopStartup
    {
        public int Order => new AuthenticationStartup().Order + 1;

        public void Configure(IApplicationBuilder application)
        {

        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
