namespace iSun.UI.Exceptions
{
    internal class HttpRequestFailedException : Exception
    {
        public HttpRequestFailedException() { }

        public HttpRequestFailedException(string message) : base(message) { }

        public HttpRequestFailedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
