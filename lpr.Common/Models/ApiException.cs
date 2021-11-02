using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class ApiException : Exception
    {
        public int ErrorCode {  get; set; }
        public ErrorMessage ErrorMessage { get; set; }
        public ApiException(int errorCode, ErrorMessage errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
