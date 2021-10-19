using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lpr.Common.Models
{
    public class ErrorMessage
    {
        public string Title {  get; set; }
        public string Message {  get; set; }
        public string? TechnicalMessage {  get; set; }

        /// <summary>
        ///     Custom Error Message
        /// </summary>
        public ErrorMessage(string title, string message, string? technicalMessage = null)
        {
            Title = title;
            Message = message;
            TechnicalMessage = technicalMessage;
        }

        /// <summary>
        ///     Base Error Message
        /// </summary>
        public ErrorMessage(string? technicalMessage = null)
        {
            Title = "An Error Occured";
            Message = "An unknown error occured.";
            TechnicalMessage = technicalMessage;
        }
    }
}
