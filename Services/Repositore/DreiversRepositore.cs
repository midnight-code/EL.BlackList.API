using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Services.Repositore
{
    public class DriversRepositore : IDriversRepositore
    {
        private readonly DataContext _context;
        public DriversRepositore(DataContext context) => _context = context;

        /// <summary>
        ///  Возвращает все совпадения по фамилии имени отчеству(приналичии)
        /// </summary>
        /// <param name="firstName">Фамилия</param>
        /// <param name="lastName">Имя</param>
        /// <param name="secondName">Отчество(не обязательный параметр)</param>
        /// /// <param name="dateTime">Дата рождения(не обязательный параметр)</param>
        /// <returns></returns>
        public IEnumerable<Drivers>? GetDriverByName(string firstName, string? lastName, string? secondName, DateTime? dateTime)
        {
            IEnumerable<Drivers> driver;

            if (firstName is not null)
            {
                if (lastName is not null)
                    driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(p => p.LastName.ToLower() == lastName.ToLower());
                else if (lastName is not null && secondName is not null)
                    driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(p => p.LastName.ToLower() == lastName.ToLower()).Where(s => s.SecondName.ToLower() == secondName.ToLower());
                else if (lastName is not null && secondName is not null && dateTime is not null)
                    driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(p => p.LastName.ToLower() == lastName.ToLower()).Where(s => s.SecondName.ToLower() == secondName.ToLower()).Where(d => d.DateRogden == dateTime);
                else if (lastName is not null && dateTime is not null)
                    driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(p => p.LastName.ToLower() == lastName.ToLower()).Where(d => d.DateRogden == dateTime);
                else if (dateTime is not null)
                    driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(d => d.DateRogden == dateTime);
                else
                    driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower());

                if (driver is not null)
                    return driver;
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Возвращает список водителей по дате рождения
        /// </summary>
        /// <param name="dateTime">Дата рождения</param>
        /// <returns></returns>
        public IEnumerable<Drivers>? GetDriverByDate(DateTime? dateTime)
        {
            if (dateTime is null)
                return null;
            else
            {
                var driver = _context.Drivers.Where(d => d.DateRogden == dateTime);
                if (driver is not null) return driver;
                else return null;
            }
        }

        /// <summary>
        /// Возвращает данные водителя по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public Drivers? GetDriversId(int id)
        {
            if (id > 0)
            {
                var result = _context.Drivers.Include(d => d.FeedBacks).ThenInclude(f => f.TaxiPools).ThenInclude(t => t.City).Include(fb => fb.FeedBacks).ThenInclude(c => c.City).FirstOrDefault(d => d.DriverId == id);
                return result;
            }
            return null;
        }

        /// <summary>
        /// Сохраняет или обновляет данные о водителе в базе
        /// </summary>
        /// <param name="drivers">Данные водителя</param>
        /// <returns></returns>
        public int SaveDriver(Drivers drivers)
        {
            if (drivers is not null)
            {
                if (drivers.DriverId > 0)
                {
                    if (_context.Drivers.Contains(drivers) == true)
                        _context.Drivers.Update(drivers);
                }

                else
                    _context.Drivers.Add(drivers);

                _context.SaveChanges();
                return drivers.DriverId;
            }
            else
                return 0;
        }

        /// <summary>
        /// Удаляет выбранного водителя из базы
        /// </summary>
        /// <param name="cmd">Данные водителя</param>
        /// <returns></returns>
        public async Task<bool> DeleteDriver(int id)
        {
            Drivers? result = await _context.Drivers.FirstOrDefaultAsync(d => d.DriverId == id);
            if (result is not null)
            {
                _context.Drivers.Remove(result);
                _context.SaveChanges();
                return await Task.Run(()=> true);
            }
            else
                return await Task.Run(() => false);

        }
    }
}