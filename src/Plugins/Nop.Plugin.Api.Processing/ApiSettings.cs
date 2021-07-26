using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Configuration;

namespace Nop.Plugin.Api.Processing
{
    public class ApiSettings : ISettings
    {
        public string AuthenticationToken { get; set; }
    }
}
