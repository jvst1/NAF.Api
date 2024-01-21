using NAF.Domain.Entities;
using NAF.Domain.Requests;

namespace NAF.Application.Interfaces
{
    public interface IPerguntaFrequenteAppService
    {
        void CreatePerguntaFrequente(CreatePerguntaFrequenteRequest request);
        PerguntaFrequente GetPerguntaFrequente(Guid id);
        void UpdatePerguntaFrequente(UpdatePerguntaFrequenteRequest request);
        void DeletePerguntaFrequente(Guid id);
    }
}
