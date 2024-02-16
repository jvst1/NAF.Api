using NAF.Domain.Entities;
using NAF.Domain.Requests;

namespace NAF.Application.Interfaces
{
    public interface IPerguntaFrequenteAppService
    {
        PerguntaFrequente CreatePerguntaFrequente(CreatePerguntaFrequenteRequest request);
        List<PerguntaFrequente> GetAllPerguntaFrequente();
        PerguntaFrequente GetPerguntaFrequente(Guid id);
        void UpdatePerguntaFrequente(UpdatePerguntaFrequenteRequest request);
        void DeletePerguntaFrequente(Guid id);
    }
}
