using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Services.Repositore
{
    public class TaxiPoolRepositore : ITaxiPoolRepositore
    {
        private readonly DataContext _context;
        public TaxiPoolRepositore(DataContext context) => _context = context;

        public TaxiPools? GetTaxiPoolById(int id)
        {
            if (id > 0)
            {
                var result = _context.TaxiPools.Include(c => c.City).FirstOrDefault(t => t.TaxiPoolsId == id);
                return result;
            }
            return null;
        }
    }
}
