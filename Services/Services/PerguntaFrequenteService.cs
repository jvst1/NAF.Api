using NAF.Domain.Entities;
using NAF.Domain.Interface.Services;

namespace NAF.Domain.Services
{
    public class PerguntaFrequenteService : IPerguntaFrequenteService
    {
        public PerguntaFrequenteService()
        {
        }

        public void ValidatePerguntaFrequente(PerguntaFrequente entity)
        {
            if (string.IsNullOrEmpty(entity.Pergunta))
                throw new ArgumentException("A pergunta é obrigatória.");

            if (string.IsNullOrEmpty(entity.Resposta))
                throw new ArgumentException("A resposta é obrigatória.");
        }
    }
}
