using System;
using System.Collections.Generic;

namespace Domain.Util
{
    [Serializable]
    public class ParametroException : Exception
    {
        public List<string> Erros { get; init; }

        public ParametroException() { }

        public ParametroException(List<string> erros)
        {
            Erros = erros;
        }

        public ParametroException(string message) : base(message) { }

        public ParametroException(string message, Exception innerException) : base(message, innerException) { }
    }
}