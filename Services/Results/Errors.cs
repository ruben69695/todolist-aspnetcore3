using System;
using System.Text;

namespace Services.Results
{
    public class GenericError
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public Exception Exception { get; set; }

        public override string ToString() {
            var errorMessage = string.Empty;
            if (Exception != null) {
                errorMessage = $"code error -> {Code} :: description -> {Description}";
            }
            else {
                var strBuilder = new StringBuilder();
                strBuilder.Append($"exception thrown, hresult -> {Exception.InnerException.HResult}");
                strBuilder.Append($":: message -> {Exception.Message} :: stacktrace -> {Exception.StackTrace}");
                errorMessage = strBuilder.ToString();
            }
            return errorMessage;
        }
    }

    public static class ErrorCodes {
        public const int Unknow = 1;
        public const int InternalError = 2;
    }
}