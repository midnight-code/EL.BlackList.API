using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Services.Repositore
{
    public class FeedBacksRepositore : IFeedBacksRepositore
    {
        private readonly DataContext _context;
        public FeedBacksRepositore(DataContext context) => _context = context;

        /// <summary>
        /// Возвпащаем коллекцию отзывов по идентификатору водителя
        /// </summary>
        /// <param name="driverid">Идентификатор водителя</param>
        /// <returns></returns>
        public IEnumerable<FeedBacks>? GetFeedBacks(int driverid)
        {
            if (driverid > 0)
            {
                return _context.FeedBacks.Include(t => t.TaxiPools).Include(c => c.City).Where(p => p.DriverId == driverid);
            }
            return null;

        }

        /// <summary>
        /// Возвращаем отзыв по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отзыва</param>
        /// <returns></returns>
        public FeedBacks? GetFeedBackById(int id)
        {
            if (id > 0) { return _context.FeedBacks.Include(t => t.TaxiPools).Include(c => c.City).FirstOrDefault(p => p.FeedBackId == id); }
            return null;
        }

        /// <summary>
        /// Возврат коллекции отзывов зарегистрированного пользователя
        /// </summary>
        /// <param name="userid">Идентификатор зарегистриованнного пользователя</param>
        /// <returns></returns>
        public IEnumerable<FeedBacks>? GetFeedBackByUserID(string userid)
        {
            if (userid is not null) { return _context.FeedBacks.Include(t => t.TaxiPools).Include(c => c.City).Where(p => p.UserGuid == userid); }
            return null;
        }

        /// <summary>
        /// Сохроняем или удаляем отзыв 
        /// </summary>
        /// <param name="feedBack">Отзыв</param>
        /// <returns></returns>
        public async Task<int> SaveFeedBackAsync(FeedBacks feedBack)
        {
            if (feedBack is not null)
            {
                if (feedBack.FeedBackId > 0)
                {
                    if (_context.FeedBacks.Contains(feedBack) == true)
                        _context.FeedBacks.Update(feedBack);
                }
                else
                    await _context.FeedBacks.AddAsync(feedBack);

                _context.SaveChanges();
                return feedBack.FeedBackId;
            }
            return 0;

        }

        /// <summary>
        /// Удаляем отзыв
        /// </summary>
        /// <param name="id">Идентификатор отзыва</param>
        /// <returns></returns>
        public async Task<bool> DeleteFeedBackAsync(int id)
        {
            if (id > 0)
            {
                FeedBacks? result = await _context.FeedBacks.FindAsync(id);

                if (result is not null)
                {
                    result.City = null;
                    result.TaxiPools = null;
                    _context.Remove(result);
                    _context.SaveChanges();
                    return await Task.Run(() => true);
                }
                return await Task.Run(() => false);
            }
            return await Task.Run(() => false);

        }
    }
}
