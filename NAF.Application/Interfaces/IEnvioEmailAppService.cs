using NAF.Domain.Entities;

namespace NAF.Application.Interfaces
{
    public interface IEnvioEmailAppService
    {
        Task<string> SendAsync(EnvioEmail email);
        EnvioEmail RegistrarEmailRecuperarSenha(Usuario usuario, string token);
    }
}

