namespace NAF.Domain.Requests
{
    public class UpdateChamadoComentarioRequest
    {
        public Guid Codigo { get; set; }
        public Guid CodigoUsuario { get; set; }
        public string Mensagem { get; set; }
    }
}
