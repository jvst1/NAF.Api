namespace NAF.Domain.Requests
{
    public class UpdateChamadoSituacaoRequest
    {
        public Guid Codigo { get; set; }
        public string Titulo { get; set; }
        public int Situacao { get; set; }
    }
}
