namespace TryitterAPI.Services.Exception
{
    using System;

    [Serializable]
    public class InvalidTokenException : Exception
    {
        public InvalidTokenException() : base()
        {

        }
        public InvalidTokenException(string messagem) : base(messagem)
        {
        }

        public InvalidTokenException(string message, Exception inner) : base(message, inner) { }

    }
}
