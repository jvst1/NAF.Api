using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class ChamadoComentario : EntityBase
    {
        public string? Mensagem { get; set; }
        public DateTime DtAlteracao { get; set; }

        public Guid CodigoUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public Guid CodigoChamado { get; set; }
        public Chamado? Chamado { get; set; }
    }
}
