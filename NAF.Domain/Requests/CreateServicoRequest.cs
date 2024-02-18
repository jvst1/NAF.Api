namespace NAF.Domain.Requests
{
    public class CreateServicoRequest
    {
        public Guid CodigoArea { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int HoraComplementar { get; set; }
    }
}
