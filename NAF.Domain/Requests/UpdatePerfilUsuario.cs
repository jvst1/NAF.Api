using NAF.Domain.Enum;

namespace NAF.Domain.Requests
{
    public class UpdatePerfilUsuario
    {
        public string? Nome { get; set; }
        public string? Identificador { get; set; }
        public string? Email { get; set; }
        public string? TelefoneCelular { get; set; }
        public string? DocumentoFederal { get; set; }
        public TipoPerfil TipoPerfil { get; set; }
    }
}
