using NAF.Domain.Base.Entity;

namespace NAF.Domain.Entities
{
    public class EnvioEmail : EntityBase
    {
        public string De { get; set; }

        public string Para { get; set; }

        public string Copia { get; set; }

        public string CopiaOculta { get; set; }

        public DateTime DtInclusao { get; set; }

        public string Assunto { get; set; }

        public string Texto { get; set; } = string.Empty;

        public bool Enviado { get; set; }

        public DateTime? DataEnvio { get; set; }

        public string Erro { get; set; }

        public string ReplyTo { get; set; }

        public string MessageId { get; set; }
        public string Replace { get; set; }
    }
}
