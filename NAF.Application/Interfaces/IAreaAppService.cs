using NAF.Domain.Entities;
using NAF.Domain.Requests;

namespace NAF.Application.Interfaces
{
    public interface IAreaAppService
    {
        Area CreateArea(CreateAreaRequest request);
        List<Area> GetAllArea();
        Area GetArea(Guid id);
        void UpdateArea(UpdateAreaRequest request);
        void DeleteArea(Guid id);
    }
}
