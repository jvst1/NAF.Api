using NAF.Application.Interfaces;
using NAF.Domain.Entities;
using NAF.Domain.Interface.Repositories;
using NAF.Domain.Interface.Services;
using NAF.Domain.Requests;

namespace NAF.Application.Services
{
    public class AreaAppService : IAreaAppService
    {
        private readonly IAreaService _areaService;
        private readonly IAreaRepository _areaRepository;

        public AreaAppService(IAreaService areaService, IAreaRepository areaRepository)
        {
            _areaService = areaService;
            _areaRepository = areaRepository;
        }

        public void CreateArea(CreateAreaRequest request)
        {
            var entity = new Area
            {
                Codigo = Guid.NewGuid(),
                Nome = request.Nome,
                DtInclusao = DateTime.Now
            };

            _areaService.ValidateArea(entity);

            _areaRepository.Insert(entity);
            _areaRepository.SaveChanges();
        }

        public Area GetArea(Guid id)
        {
            var area = _areaRepository.GetByCodigo(id);

            if (area is null)
                throw new KeyNotFoundException($"Area com o codigo {id} não foi encontrada.");
            return area;
        }

        public void UpdateArea(UpdateAreaRequest request)
        {
            var entity = GetArea(request.Codigo);
            entity.Nome = request.Nome;

            _areaService.ValidateArea(entity);

            entity.DtAlteracao = DateTime.Now;

            _areaRepository.Update(entity);
            _areaRepository.SaveChanges();
        }

        public void DeleteArea(Guid id)
        {
            var entity = GetArea(id);
            _areaRepository.Remove(entity);
        }
    }
}