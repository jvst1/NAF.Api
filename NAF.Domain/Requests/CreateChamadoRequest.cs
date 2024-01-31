namespace NAF.Domain.Requests
{
    public class CreateChamadoRequest
    {
        public Guid CodigoUsuario { get; set; }
        public Guid CodigoServico { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}
