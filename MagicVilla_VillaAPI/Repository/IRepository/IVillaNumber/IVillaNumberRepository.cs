using MagicVilla_VillaAPI.Models.Villa;
using MagicVilla_VillaAPI.Models.VillaNumber;

namespace MagicVilla_VillaAPI.Repository.IRepository.IVillaNumber
{
    public interface IVillaNumberRepository : IRepository<VillaNumberModel>
    {
        Task<VillaNumberModel> UpdateAsync(VillaNumberModel entity);

    }
}
