using Microsoft.AspNetCore.Identity;
using NAF.Domain.Base.Entity;
using NAF.Domain.Enum;

namespace NAF.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string? Nome { get; set; }
        public string? Identificador { get; set; }
        public string? Email { get; set; }
        public string? TelefoneCelular { get; set; }
        public string? DocumentoFederal { get; set; }
        public TipoPerfil? TipoPerfil { get; set; }
        public SituacaoUsuario? Situacao { get; set; }

        public string? IdentityUserId { get; set; }
        public IdentityUser? IdentityUser { get; set; }
    }
}
