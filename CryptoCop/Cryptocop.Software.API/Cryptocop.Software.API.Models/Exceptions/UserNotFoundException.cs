using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptocop.Software.API.Models.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("The user was not found.") { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
