using NAF.Domain.Entities;
using NAF.Application.Interfaces;
using NAF.Domain.Interface.Services;

namespace NAF.Application.Services
{
    public class EnvioEmailAppService : IEnvioEmailAppService
    {
        private readonly IEnvioEmailService _envioEmailService;

        public EnvioEmailAppService(IEnvioEmailService envioEmailService)
        {
            _envioEmailService = envioEmailService;
        }

        public EnvioEmail RegistrarEmailRecuperarSenha(Usuario usuario, string token)
        {
            return _envioEmailService.RegistrarEmailRecuperarSenha(usuario, token);
        }
        public async Task<string> SendAsync(EnvioEmail email)
        {
            return await _envioEmailService.SendAsync(email);
        }
    }
}
