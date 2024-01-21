using NAF.Domain.Entities;
using NAF.Domain.Interface.Services;

namespace NAF.Domain.Services
{
    public class AreaService : IAreaService
    {
        public AreaService()
        {
        }

        public void ValidateArea(Area area)
        {
            if (string.IsNullOrEmpty(area.Nome))
                throw new ArgumentException("O nome da area é obrigatório.");

            if (!string.IsNullOrEmpty(area.Nome) && area.Nome.Length > 255)
                throw new ArgumentException("O nome da area não pode conter mais que 255 caracteres.");
        }
    }
}
