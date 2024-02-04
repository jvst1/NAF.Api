using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class PerguntaFrequente : EntityBase
    {
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public DateTime DtAlteracao { get; set; }
    }
}
