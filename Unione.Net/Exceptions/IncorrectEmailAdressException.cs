using System;

namespace Unione.Net.Exceptions;

public class IncorrectEmailAdressException : Exception
{
    public IncorrectEmailAdressException()
    {
    }

    public IncorrectEmailAdressException(string message) : base(message)
    {
    }

    public IncorrectEmailAdressException(string message, Exception inner) : base(message, inner)
    {
    }
}