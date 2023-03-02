using MagicVilla_VillaAPI.Models.Villa;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository.IVilla
{
    public interface IVillaRepository : IRepository<VillaModel>
    {
        Task<VillaModel> UpdateAsync(VillaModel entity);


    }
}
