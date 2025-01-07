namespace WebKlient.Model

{
    public class User
    {
        public required string UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

    }
}

