namespace NAF.Domain.Requests
{
    public class RecuperarSenhaRequest
    {
        public string Token { get; set; }

        public string Login { get; set; }

        public Guid CodigoUsuario { get; set; }

        public string NovaSenha { get; set; }
    }
}
