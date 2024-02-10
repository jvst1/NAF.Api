using NAF.Domain.Enum;

namespace NAF.Domain.ValueObjects
{
    public record UserToken
    {
        public Guid CodigoUsuario { get; set; }
        public string Email { get; set; }
        public TipoPerfil TipoPerfil { get; set; }
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
