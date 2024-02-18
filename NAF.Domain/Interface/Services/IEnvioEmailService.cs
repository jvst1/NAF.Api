using NAF.Domain.Entities;

namespace NAF.Domain.Interface.Services
{
    public interface IEnvioEmailService
    {
        Task<string> SendAsync(EnvioEmail email);
        EnvioEmail RegistrarEmailRecuperarSenha(Usuario usuario, string token);
    }
}
