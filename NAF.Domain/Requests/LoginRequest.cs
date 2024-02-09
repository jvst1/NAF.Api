namespace NAF.Domain.Requests
{
    public class LoginRequest
    {
        public string? DocumentoFederal { get; set; }
        public string? Password { get; set; }
    }
}
