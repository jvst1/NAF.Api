namespace NAF.Domain.Requests
{
    public class UpdatePerguntaFrequenteRequest
    {
        public Guid Codigo { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
    }
}
