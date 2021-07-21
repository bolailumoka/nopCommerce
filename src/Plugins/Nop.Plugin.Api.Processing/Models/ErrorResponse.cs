using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nop.Plugin.Api.Processing.Models
{
    [GeneratedCode("NJsonSchema", "10.1.4.0 (Newtonsoft.Json v12.0.0.0)")]
    public class ErrorResponse
    {
        [JsonProperty("message", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("error_code", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; }
    }
}
