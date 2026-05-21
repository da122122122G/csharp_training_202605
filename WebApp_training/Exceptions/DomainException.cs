using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_training.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message)
    : base() { }
    public DomainException(string message, Exception innerException)
    : base(message, innerException) { }
}