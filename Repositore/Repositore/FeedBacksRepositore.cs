using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Repositore.Repositore
{
    public class FeedBacksRepositore : IFeedBacksRepositore
    {
        private readonly DataContext _context;
        public FeedBacksRepositore(DataContext context) => _context = context;

        public async Task<bool> Delete(int id)
        {
            if (id > 0)
            {
                FeedBacks? result = await _context.FeedBacks.FindAsync(id);

                if (result is not null)
                {
                    result.City = null;
                    result.TaxiPools = null;
                    _context.Remove(result);
                    await _context.SaveChangesAsync();
                    return await Task.Run(() => true);
                }
                return await Task.Run(() => false);
            }
            return await Task.Run(() => false);
        }

        public async Task<FeedBacks?> GetById(int id)
        {
            if (id > 0) {
                return await Task.Run(() => _context.FeedBacks.Include(t => t.TaxiPools).ThenInclude(c => c.City).Include(c => c.City).FirstOrDefaultAsync(p => p.FeedBackId == id));
            }
            else
               return null;
        }

        public async Task<IEnumerable<FeedBacks>?> GetFeedBackByUserIDAsync(string userid)
        {
            if (userid is not null)
            {
                return await Task.Run(() => _context.FeedBacks.Include(t => t.TaxiPools).Include(c => c.City).Where(p => p.UserGuid == userid));
            }
            else
                return null;
        }

        public async Task<IEnumerable<FeedBacks>?> GetFeedBacksByDriverIdAsync(int driverid)
        {
            if (driverid > 0)
            {
                return await Task.Run(() => _context.FeedBacks.Include(t => t.TaxiPools).Include(c => c.City).Where(p => p.DriversId == driverid));
            }
            else
                return null;
        }

        public async Task<int> Save(FeedBacks intite)
        {
            if (intite is not null)
            {
                if (intite.FeedBackId > 0)
                {
                    if (_context.FeedBacks.Contains(intite) == true)
                        _context.FeedBacks.Update(intite);
                }
                else
                    await _context.FeedBacks.AddAsync(intite);

                await _context.SaveChangesAsync();
                return await Task.Run(() => intite.FeedBackId);
            }
            return 0;
        }

        public Task<IEnumerable<FeedBacks>> Select()
        {
            throw new NotImplementedException();
        }
    }
}
