using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Services.Repositore
{
    public class TaxiPoolRepositore : ITaxiPoolRepositore
    {
        private readonly DataContext _context;
        public TaxiPoolRepositore(DataContext context) => _context = context;


        /// <summary>
        /// Получаем данные по выбранному таксопарку
        /// </summary>
        /// <param name="id">Идентификатор таксопарка</param>
        /// <returns></returns>
        public TaxiPools? GetTaxiPoolById(int id)
        {
            if (id > 0)
            {
                var result = _context.TaxiPools.Include(c => c.City).FirstOrDefault(t => t.TaxiPoolsId == id);
                return result;
            }
            return null;
        }

        /// <summary>
        /// Сохраняет или обновляет название таксопарка в базе
        /// </summary>
        /// <param name="drivers">Таксопарк</param>
        /// <returns></returns>
        public int SaveTaxiPool(TaxiPools taxiPools)
        {
            if (taxiPools is not null)
            {
                if (taxiPools.TaxiPoolsId > 0)
                {
                    if (_context.TaxiPools.Contains(taxiPools) == true)
                        _context.TaxiPools.Update(taxiPools);
                }

                else
                    _context.TaxiPools.Add(taxiPools);

                _context.SaveChanges();
                return taxiPools.TaxiPoolsId;
            }
            else
                return 0;
        }

        /// <summary>
        /// Удаляет выбранный таксопарк из базы
        /// </summary>
        /// <param name="id">Идентификатор таксопарка</param>
        /// <returns></returns>
        public async Task<bool> DeleteTaxiPool(int id)
        {
            TaxiPools? result = await _context.TaxiPools.FirstOrDefaultAsync(d => d.TaxiPoolsId == id);
            if (result is not null)
            {
                _context.TaxiPools.Remove(result);
                _context.SaveChanges();
                return await Task.Run(() => true);
            }
            else
                return await Task.Run(() => false);

        }

    }
}
