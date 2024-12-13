using System;

namespace Unione.Net.Exceptions;

public class EmptyApiConfigurationException : Exception
{
    public EmptyApiConfigurationException()
    {
    }

    public EmptyApiConfigurationException(string message) : base(message)
    {
    }

    public EmptyApiConfigurationException(string message, Exception inner) : base(message, inner)
    {
    }
}