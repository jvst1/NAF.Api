using NAF.Application.Interfaces;
using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;

namespace NAF.Application.Services
{
    public class PerguntaFrequenteAppService : IPerguntaFrequenteAppService
    {
        private readonly IPerguntaFrequenteService _perguntaFrequenteService;
        private readonly IPerguntaFrequenteRepository _perguntaFrequenteRepository;

        public PerguntaFrequenteAppService(IPerguntaFrequenteService perguntaFrequenteService, IPerguntaFrequenteRepository perguntaFrequenteRepository)
        {
            _perguntaFrequenteService = perguntaFrequenteService;
            _perguntaFrequenteRepository = perguntaFrequenteRepository;
        }

        public PerguntaFrequente CreatePerguntaFrequente(CreatePerguntaFrequenteRequest request)
        {
            var entity = new PerguntaFrequente
            {
                Codigo = Guid.NewGuid(),
                Pergunta = request.Pergunta,
                Resposta = request.Resposta,
                DtInclusao = DateTime.Now
            };

            _perguntaFrequenteService.ValidatePerguntaFrequente(entity);

            _perguntaFrequenteRepository.Insert(entity);
            _perguntaFrequenteRepository.SaveChanges();

            return entity;
        }

        public List<PerguntaFrequente> GetAllPerguntaFrequente()
        {
            var perguntasFrequentes = _perguntaFrequenteRepository.GetAll();
            return perguntasFrequentes.ToList();
        }

        public PerguntaFrequente GetPerguntaFrequente(Guid id)
        {
            var perguntaFrequente = _perguntaFrequenteRepository.GetByCodigo(id);

            if (perguntaFrequente is null)
                throw new KeyNotFoundException($"Pergunta frequente com o codigo {id} não foi encontrada.");
            return perguntaFrequente;
        }

        public void UpdatePerguntaFrequente(UpdatePerguntaFrequenteRequest request)
        {
            var entity = GetPerguntaFrequente(request.Codigo);

            entity.Pergunta = request.Pergunta;
            entity.Resposta = request.Resposta;

            _perguntaFrequenteService.ValidatePerguntaFrequente(entity);

            entity.DtAlteracao = DateTime.Now;

            _perguntaFrequenteRepository.Update(entity);
            _perguntaFrequenteRepository.SaveChanges();
        }

        public void DeletePerguntaFrequente(Guid id)
        {
            var entity = GetPerguntaFrequente(id);
            _perguntaFrequenteRepository.Remove(entity);
        }
    }
}
