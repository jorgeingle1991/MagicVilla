﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models.Villa;
using MagicVilla_VillaAPI.Models.VillaNumber;
using MagicVilla_VillaAPI.Repository.IRepository.IVilla;
using MagicVilla_VillaAPI.Repository.IRepository.IVillaNumber;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : Repository<VillaModel>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task<VillaModel> UpdateAsync(VillaModel entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

    }
}
