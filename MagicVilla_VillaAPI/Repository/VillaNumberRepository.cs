using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models.Villa;
using MagicVilla_VillaAPI.Models.VillaNumber;
using MagicVilla_VillaAPI.Repository.IRepository;
using MagicVilla_VillaAPI.Repository.IRepository.IVillaNumber;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumberModel>,IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<VillaNumberModel> UpdateAsync(VillaNumberModel entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumberModel.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
