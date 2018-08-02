using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoneShopAPI.Data.Access.DAL;
using PhoneShopAPI.Models;

namespace PhoneShopAPI.Data.Access.Imp
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly PhoneContext _dbContext;

        public PhoneRepository(PhoneContext context)
        {
            _dbContext = context;

            if (_dbContext.PhoneItems.Count() == 0)
            {
                _dbContext.PhoneItems.AddRangeAsync(
                    new Phone() { Name = "iPhone X", Description = "Access your work directory, email or calendar with iPhone X by Apple." },
                    new Phone() { Name = "Galaxy S9+", Description = "Access your work directory, email or calendar with Galaxy S9+ by Samsung." },
                    new Phone() { Name = "iPhone 8 Plus", Description = "Access your work directory, email or calendar with iPhone 8 Plus by Apple." },
                    new Phone() { Name = "Galaxy Note8", Description = "Access your work directory, email or calendar with Galaxy Note8 by Samsung." });
                _dbContext.SaveChangesAsync();
            }
        }

        public IEnumerable<Phone> GetAll() => _dbContext.PhoneItems.Select(s => s);

        public Phone GetById(int id) => _dbContext.PhoneItems.Find(id);

        public async Task<Phone> GetByIdAsync(int id) => await _dbContext.PhoneItems.AsNoTracking().FirstAsync(f => f.Id == id);

        public void Add(Phone entity) => _dbContext.PhoneItems.Add(entity);

        public async Task AddAsync(Phone entity) => await _dbContext.PhoneItems.AddAsync(entity);

        public void Update(Phone entity)
        {
            _dbContext.PhoneItems.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Phone entity)
        {
            _dbContext.PhoneItems.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Commit() => _dbContext.SaveChanges();

        public async Task CommitAsync() => await _dbContext.SaveChangesAsync();
    }
}