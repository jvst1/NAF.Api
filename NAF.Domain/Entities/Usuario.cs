using Microsoft.AspNetCore.Identity;
using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public string? Nome { get; set; }
        public string? Identificador { get; set; }
        public string? Email { get; set; }
        public string? TelefoneCelular { get; set; }
        public string? DocumentoFederal { get; set; }
        public int TipoPerfil { get; set; }
        public int Situacao { get; set; }

        public string? IdentityUserId { get; set; }
        public IdentityUser? IdentityUser { get; set; }
    }
}
