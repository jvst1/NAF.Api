using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Infra.Data.External_Dependence;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using NAF.Domain.Interface.Services;

namespace NAF.Domain.Services.Services
{
    public class EnvioEmailService : IEnvioEmailService
    {
        private const string EmailDe = "catolicasc.naf@gmail.com";
        private const string ReplyTo = "catolicasc.naf@gmail.com";

        private readonly AppSettings _appSettings;
        private readonly IEnvioEmailRepository _emailRepository;
        private readonly ILogger<EnvioEmailService> _logger;

        public EnvioEmailService(IOptions<AppSettings> options,
                                       IEnvioEmailRepository emailRepository,
                                       ILogger<EnvioEmailService> logger)
        {
            _appSettings = options.Value;
            _emailRepository = emailRepository;
            _logger = logger;
        }

        public EnvioEmail RegistrarEmailRecuperarSenha(Usuario usuario, string token)
        {
            var email = GetEmail(usuario, new Dictionary<string, string>
            {
                {"{{TOKENSENHA}}", token},
                {"{{CODIGOUSUARIO}}", usuario.Codigo.ToString()},
                {"{{USUARIOEMAIL}}", usuario.Email}
            });

            email.Assunto = "Recuperação de senha";
            email.Texto = "<!DOCTYPE html><html lang=\"pt-br\"><head> <meta charset=\"utf-8\" /> <title>Recuperação de Senha</title> <style> body { font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; } .container { max-width: 600px; margin: 0 auto; padding: 20px; background-color: #fff; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); } .header { text-align: center; background-color: #007bff; padding: 20px 0; } .header h1 { color: #fff; font-size: 24px; } .content { padding: 20px; } .content p { font-size: 16px; line-height: 1.5; margin-bottom: 20px; } .button { display: inline-block; padding: 10px 20px; background-color: #007bff; color: #fff; text-decoration: none; border-radius: 5px; transition: background-color 0.3s; } .button:hover { background-color: #0056b3; } .footer { text-align: center; margin-top: 20px; color: #777; } </style></head><body><div class=\"container\"> <div class=\"header\"> <h1>Recuperação de Senha</h1> </div> <div class=\"content\"> <p>Prezado(a), {{NOME}},</p> <p>Para prosseguir com o primeiro acesso ou a recuperação de senha, clique no botão abaixo:</p> <a href=\"{{BASEURL}}/recuperar?c={{CODIGOUSUARIO}}&t={{TOKENSENHA}}&e={{USUARIOEMAIL}}\" class=\"button\">Recuperação de Senha</a> <p>Se o botão não funcionar, copie e cole o seguinte endereço em seu navegador:</p> <p>{{BASEURL}}/recuperar?c={{CODIGOUSUARIO}}&t={{TOKENSENHA}}&e={{USUARIOEMAIL}}</p> </div> <div class=\"footer\"> Atenciosamente, <br> <a href=\"{{BASEURL}}\">Clique aqui para acessar nosso site</a> </div></div></body></html>";

            _emailRepository.Insert(email);
            _emailRepository.SaveChanges();

            return email;
        }
        public async Task<string> SendAsync(EnvioEmail email)
        {
            try
            {
                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(EmailDe);
                    message.Subject = email.Assunto;
                    message.To.Add(new MailAddress(email.Para));

                    if (!string.IsNullOrWhiteSpace(email.Copia))
                        message.CC.Add(new MailAddress(email.Copia));

                    if (!string.IsNullOrWhiteSpace(email.CopiaOculta))
                        message.Bcc.Add(new MailAddress(email.CopiaOculta));

                    message.IsBodyHtml = true;
                    if (!string.IsNullOrWhiteSpace(email.Replace))
                    {
                        message.Body = email.Texto;
                        var variaveisParaReplace = JsonConvert.DeserializeObject<Dictionary<string, string>>(email.Replace);
                        foreach (var variable in variaveisParaReplace)
                            message.Body = message.Body.Replace(variable.Key, variable.Value);
                    }
                    else
                        message.Body = email.Texto;

                    using (var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(_appSettings.Gmail.AccessKeyId, _appSettings.Gmail.SecretAccessKey),
                        EnableSsl = true
                    })
                    {
                        await smtpClient.SendMailAsync(message).ConfigureAwait(false);
                    }

                    email.MessageId = $"{DateTime.Now:yyyyMMddHHmmssFFFFF}{DateTimeOffset.Now.ToUnixTimeMilliseconds()}".Substring(5, 7);
                    email.Enviado = true;
                    email.DataEnvio = DateTime.Now;
                    _emailRepository.Update(email);
                    _emailRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ErrorMessage: {error}; Exception: {exception}", ex.Message, ex);
                email.MessageId = null;
                email.Enviado = false;
                email.DataEnvio = DateTime.Now;
                email.Erro = $"{ex.Message};;; {ex}";
                _emailRepository.Update(email);
                _emailRepository.SaveChanges();
            }

            return email.MessageId;
        }

        private EnvioEmail GetEmail(Usuario usuario, Dictionary<string, string> parametros = null)
        {
            parametros.Add("{{BASEURL}}", _appSettings.WebUrl!);
            parametros.Add("{{NOME}}", usuario.Nome!);

            var envioEmail = new EnvioEmail
            {
                Codigo = Guid.NewGuid(),
                DtInclusao = DateTime.Now,
                Para = usuario.Email!,
                De = EmailDe,
                ReplyTo = ReplyTo,
            };

            if (parametros != null)
                envioEmail.Replace = JsonConvert.SerializeObject(parametros);

            return envioEmail;
        }
    }
}
