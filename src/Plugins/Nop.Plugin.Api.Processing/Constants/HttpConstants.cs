using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Api.Processing.Constants
{
    public class HttpConstants
    {
        public const string OkDescription = "On success.";

        public const string ContentNotFoundDescription = "Resource has been removed.";

        public const string CreatedDescription = "Return 201 (create) OK.";

        public const string BadRequestDescription = "Bad request.";

        public const string UnauthorizedDescription = "The supplied Application access token invalid.";

        public const string ForbiddenDescription = "Forbidden.";

        public const string NotFoundDescription = "Bad Path. Resource not found.";

        public const string InternalServerErrorDescription = "Internal error.";
    }
}
