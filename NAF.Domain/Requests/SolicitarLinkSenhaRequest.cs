namespace NAF.Domain.Requests
{
    public class SolicitarLinkSenhaRequest
    {
        public string Login { get; set; }

        public bool PrimeiroAcesso { get; set; }
    }
}
