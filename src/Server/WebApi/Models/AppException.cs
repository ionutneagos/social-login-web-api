namespace WebApi.Models
{
    using System;

    public class AppException : Exception
    {
        public int Code { get; }

        protected AppException()
        {
        }

        public AppException(int code)
        {
            Code = code;
        }

        public AppException(int code, string message) : base(message)
        {
            Code = code;
        }

        public AppException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public AppException(int code, string message, Exception innerException) : base(message, innerException)
        {
            Code = code;
        }

        public AppException(string message) : base(message)
        {
        }
    }

    public class ErrorResponse
    {
        public int Status { get; set; } = 500;

        public string Message { get; set; }
    }
}
