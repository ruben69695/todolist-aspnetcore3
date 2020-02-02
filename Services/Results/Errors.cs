using System;

namespace Services.Results
{
    public class GenericError
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public Exception Exception { get; set; }
    }

    public static class ErrorCodes {
        public const int Unknow = 1;
        public const int InternalError = 2;
    }
}