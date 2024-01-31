namespace NAF.Domain.Requests
{
    public class CreateChamadoComentarioRequest
    {
        public Guid CodigoUsuario { get; set; }
        public string Mensagem { get; set; }
    }
}
