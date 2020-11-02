using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppChallenge.Models
{
    public class Response
    {
        public Response()
        {

        }
        public string Code { get; set; }
        public string Message { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }

        public object Data { get; set; }
        public Response(string code, string message)
        {
            this.Code = code;
            Message = message;
        }
        public Response(string code, string message, object data)
        {
            this.Code = code;
            Message = message;
            Data = data;
        }

        public Response(string code, string message, object data, Dictionary<string, string[]> modelState)
        {
            this.Code = code;
            Message = message;
            Data = data;
            Errors = modelState;
        }
    }
}
