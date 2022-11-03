using System.Runtime.Serialization;

[Serializable]
internal class InputFileNotFoundException : Exception
{
    public InputFileNotFoundException()
    {
    }

    public InputFileNotFoundException(string? message) : base(message)
    {
    }

    public InputFileNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected InputFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}