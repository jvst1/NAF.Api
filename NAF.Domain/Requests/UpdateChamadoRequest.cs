namespace NAF.Domain.Requests
{
    public class UpdateChamadoRequest
    {
        public Guid Codigo { get; set; }
        public Guid CodigoUsuario { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
    }
}
