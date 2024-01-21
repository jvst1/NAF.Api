using NAF.Domain.Entities;
using NAF.Domain.Requests;

namespace NAF.Application.Interfaces
{
    public interface IAreaAppService
    {
        void CreateArea(CreateAreaRequest request);
        Area GetArea(Guid id);
        void UpdateArea(UpdateAreaRequest request);
        void DeleteArea(Guid id);
    }
}
