using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrifyLibrary.Business
{
    // TODO : Implement bad request for bad requests on the client side
    // https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-5.0#use-exceptions-to-modify-the-response
    public class BadRequestException : Exception
    {
        public BadRequestException() { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, Exception inner) : base(message, inner) { }
    }
}
