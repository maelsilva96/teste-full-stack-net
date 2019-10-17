using System;

namespace FI.AtividadeEntrevista.Exceptions
{
    class DataBaseException : Exception
    {
        public DataBaseException(string message) : base(message)
        {
        }
    }
}
