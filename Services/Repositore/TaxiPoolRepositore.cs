using Microsoft.EntityFrameworkCore.Storage;

namespace EL.BlackList.API.Services.Repositore
{
    public class TaxiPoolRepositore
    {
        private readonly Database _context;
        public TaxiPoolRepositore(Database context)=>_context = context;
    }
}
