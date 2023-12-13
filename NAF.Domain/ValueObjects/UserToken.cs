namespace NAF.Domain.ValueObjects
{
    public record UserToken
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
