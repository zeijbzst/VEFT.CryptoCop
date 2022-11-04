using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocop.Software.API.Models.Exceptions
{
    public class ResourceDoesNotBelongException : Exception
    {
        public ResourceDoesNotBelongException() : base("This resource does not belong to you.") { }
        public ResourceDoesNotBelongException(string message) : base(message) { }
        public ResourceDoesNotBelongException(string message, Exception innerException) : base(message, innerException) { }
    }
}
