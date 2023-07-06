using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Services.Repositore
{
    public class TaxiPoolRepositore : ITaxiPoolRepositore
    {
        private readonly DataContext _context;
        public TaxiPoolRepositore(DataContext context) => _context = context;

        public async Task<bool> Delete(int id)
        {
            TaxiPools? result = await _context.TaxiPools.FirstOrDefaultAsync(d => d.TaxiPoolsId == id);
            if (result is not null)
            {
                _context.TaxiPools.Remove(result);
                await _context.SaveChangesAsync();
                return await Task.Run(() => true);
            }
            else
                return await Task.Run(() => false);
        }

        public Task<IEnumerable<TaxiPools>> Select()
        {
            throw new NotImplementedException();
        }

        public async Task<TaxiPools?> GetById(int id)
        {
            if (id > 0)
            {
                var result = await _context.TaxiPools.Include(c => c.City).FirstOrDefaultAsync(t => t.TaxiPoolsId == id);
                return await Task.Run(() => result);
            }
            return null;
        }

        public async Task<int> Save(TaxiPools intite)
        {
            if (intite is not null)
            {
                if (intite.TaxiPoolsId > 0)
                {
                    if (await _context.TaxiPools.ContainsAsync(intite) == true)
                        _context.TaxiPools.Update(intite);
                }
                else
                    await _context.TaxiPools.AddAsync(intite);

                await _context.SaveChangesAsync();
                return await Task.Run(() => intite.TaxiPoolsId);
            }
            else
                return await Task.Run(() => 0);
        }
    }
}
