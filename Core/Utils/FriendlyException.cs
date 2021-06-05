using Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public class FriendlyException : Exception
    {
        public FriendlyException(string message, string field, HttpStatusCode httpCode, ErrorCode code)
        {
            this.Message = message;
            this.Field = field;
            this.HttpCode = httpCode;
            this.Code = code;
        }

        public override string Message { get; }

        public string Field { get; }

        public HttpStatusCode HttpCode { get; }

        public ErrorCode Code { get; }
    }
}
