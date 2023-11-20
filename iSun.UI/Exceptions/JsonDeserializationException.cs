namespace iSun.UI.Exceptions
{
    public class JsonDeserializationException : Exception
    {
        public JsonDeserializationException() { }

        public JsonDeserializationException(string message) : base(message) { }

        public JsonDeserializationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
