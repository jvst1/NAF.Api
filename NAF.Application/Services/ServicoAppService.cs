using NAF.Application.Interfaces;
using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;

namespace NAF.Application.Services
{
    public class ServicoAppService : IServicoAppService
    {
        private readonly IServicoService _servicoService;
        private readonly IAreaAppService _areaAppService;
        private readonly IServicoRepository _servicoRepository;

        public ServicoAppService(IServicoService servicoService, IAreaAppService areaAppService, IServicoRepository servicoRepository)
        {
            _servicoService = servicoService;
            _areaAppService = areaAppService;
            _servicoRepository = servicoRepository;
        }

        public Servico CreateServico(CreateServicoRequest request)
        {
            var entity = new Servico
            {
                Codigo = Guid.NewGuid(),
                CodigoArea = request.CodigoArea,
                Nome = request.Nome,
                Descricao = request.Descricao,
                DtInclusao = DateTime.Now
            };

            _servicoService.ValidateServico(entity);

            _servicoRepository.Insert(entity);
            _servicoRepository.SaveChanges();

            return entity;
        }

        public List<Servico> GetAllServico()
        {
            var servicos = _servicoRepository.GetAll();
            return servicos.ToList();
        }

        public Servico GetServico(Guid id)
        {
            var servico = _servicoRepository.GetByCodigo(id);

            if (servico is null)
                throw new KeyNotFoundException($"Servico com o codigo {id} não foi encontrada.");

            if (servico.CodigoArea != Guid.Empty)
                servico.Area = _areaAppService.GetArea(servico.CodigoArea);

            return servico;
        }

        public void UpdateServico(UpdateServicoRequest request)
        {
            var entity = GetServico(request.Codigo);

            entity.CodigoArea = request.CodigoArea;
            entity.Nome = request.Nome;
            entity.Descricao = request.Descricao;
            
            _servicoService.ValidateServico(entity);

            entity.DtAlteracao = DateTime.Now;

            _servicoRepository.Update(entity);
            _servicoRepository.SaveChanges();
        }

        public void DeleteServico(Guid id)
        {
            var entity = GetServico(id);
            _servicoRepository.Remove(entity);
        }
    }
}
