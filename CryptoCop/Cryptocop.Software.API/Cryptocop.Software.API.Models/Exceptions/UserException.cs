using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocop.Software.API.Models.Exceptions
{
    public class UserException : Exception
    {
        public UserException() : base("Error logging in.") { }
        public UserException(string message) : base(message) { }
        public UserException(string message, Exception innerException) : base(message, innerException) { }
    }
}

