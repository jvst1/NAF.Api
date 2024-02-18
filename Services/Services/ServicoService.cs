using NAF.Domain.Entities;
using NAF.Domain.Interface.Services;

namespace NAF.Domain.Services.Services
{
    public class ServicoService : IServicoService
    {
        public ServicoService()
        {
        }

        public void ValidateServico(Servico servico)
        {
            if (string.IsNullOrEmpty(servico.Nome))
                throw new ArgumentException("O nome do serviço é obrigatório.");

            if (!string.IsNullOrEmpty(servico.Nome) && servico.Nome.Length > 255)
                throw new ArgumentException("O nome do servico não pode conter mais que 255 caracteres.");

            if (string.IsNullOrEmpty(servico.Descricao))
                throw new ArgumentException("Uma breve descrição é obrigatória.");

            if (servico.CodigoArea.Equals(Guid.Empty))
                throw new ArgumentException("Uma area deve ser selecionada. O preenchimento do campo é obrigatório.");

            if (servico.HoraComplementar < 0)
                throw new ArgumentException("Hora Complementar deve ser informada. O preenchimento do campo é obrigatório");
        }
    }
}

