using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_training.Exceptions;

public class ExistsException : Exception
{
    public ExistsException(string message)
    : base(message) { }
    public ExistsException(string message, Exception innerException)
    : base(message, innerException) { }
}
