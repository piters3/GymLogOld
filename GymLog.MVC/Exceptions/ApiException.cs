using System;
using System.Net;

namespace GymLog.MVC.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, string jsonData)
        {
            StatusCode = statusCode;
            JsonData = jsonData;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public string JsonData { get; private set; }
    }
}