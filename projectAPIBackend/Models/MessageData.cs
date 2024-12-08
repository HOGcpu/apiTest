namespace ProjectApiBackend.Models;

public class MessageData
{
    public required string Subject { get; set; }
    public required string Message { get; set; }
}

public class PostResponseData
{
        public bool Status { get; set; } 
}

public class GetResponseData
{
        public bool Status { get; set; }
        public required string Message { get; set; }
        public List<string>? NumericalMessages { get; set; } 
}
