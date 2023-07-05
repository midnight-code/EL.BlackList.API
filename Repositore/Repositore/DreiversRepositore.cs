using EL.BlackList.API.Data;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EL.BlackList.API.Services.Repositore
{
    public class DriversRepositore : IDriversRepositore
    {
        private readonly DataContext _context;
        public DriversRepositore(DataContext context) => _context = context;

        public async Task<bool> Delete(int id)
        {
            Drivers? result = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
            if (result is not null)
            {
                _context.Drivers.Remove(result);
                await _context.SaveChangesAsync();
                return await Task.Run(() => true);
            }
            else
                return await Task.Run(() => false);
        }

        public async Task<Drivers?> GetById(int id)
        {
            if (id > 0)
            {
                var result = await _context.Drivers.Include(d => d.FeedBacks)
                    .Include(f => f.TaxiPools).ThenInclude(t => t.City)
                    .Include(f => f.FeedBacks).ThenInclude(c => c.City)
                    .Include(r => r.FeedBacks).ThenInclude(f => f.TaxiPools)
                    .FirstOrDefaultAsync(d => d.Id == id);
                return await Task.Run(() => result);
            }
            return null;
        }

        public async Task<IEnumerable<Drivers>?> GetDriverByDateAsunc(DateTime? dateTime)
        {
            if (dateTime is null)
                return null;
            else
            {
                var driver = _context.Drivers.Where(d => d.DateRogden == dateTime);
                if (driver is not null) return await Task.Run(() => driver);
                else return null;
            }
        }

        public async Task<IEnumerable<Drivers>?> GetDriverByName(string firstName, string? lastName, string? secondName, DateTime? dateTime)
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
                    return await Task.Run(() => driver);
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<int> Save(Drivers drivers)
        {
            if (drivers is not null)
            {
                if (drivers.Id > 0)
                {
                    if (await _context.Drivers.ContainsAsync(drivers) == true)
                        _context.Drivers.Update(drivers);
                }

                else
                    await _context.Drivers.AddAsync(drivers);

                await _context.SaveChangesAsync();
                return await Task.Run(() => drivers.Id);
            }
            else
                return 0;
        }

        public Task<IEnumerable<Drivers>> Select()
        {
            throw new NotImplementedException();
        }

        #region old code
        //    /// <summary>
        //    ///  Возвращает все совпадения по фамилии имени отчеству(приналичии)
        //    /// </summary>
        //    /// <param name="firstName">Фамилия</param>
        //    /// <param name="lastName">Имя</param>
        //    /// <param name="secondName">Отчество(не обязательный параметр)</param>
        //    /// /// <param name="dateTime">Дата рождения(не обязательный параметр)</param>
        //    /// <returns></returns>
        //    public IEnumerable<Drivers>? GetDriverByName(string firstName, string? lastName, string? secondName, DateTime? dateTime)
        //    {
        //        IEnumerable<Drivers> driver;

        //        if (firstName is not null)
        //        {
        //            if (lastName is not null)
        //                driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(p => p.LastName.ToLower() == lastName.ToLower());
        //            else if (lastName is not null && secondName is not null)
        //                driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(p => p.LastName.ToLower() == lastName.ToLower()).Where(s => s.SecondName.ToLower() == secondName.ToLower());
        //            else if (lastName is not null && secondName is not null && dateTime is not null)
        //                driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(p => p.LastName.ToLower() == lastName.ToLower()).Where(s => s.SecondName.ToLower() == secondName.ToLower()).Where(d => d.DateRogden == dateTime);
        //            else if (lastName is not null && dateTime is not null)
        //                driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(p => p.LastName.ToLower() == lastName.ToLower()).Where(d => d.DateRogden == dateTime);
        //            else if (dateTime is not null)
        //                driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower()).Where(d => d.DateRogden == dateTime);
        //            else
        //                driver = _context.Drivers.Where(p => p.FirstName.ToLower() == firstName.ToLower());

        //            if (driver is not null)
        //                return driver;
        //            else
        //                return null;
        //        }
        //        else
        //            return null;
        //    }

        //    /// <summary>
        //    /// Возвращает список водителей по дате рождения
        //    /// </summary>
        //    /// <param name="dateTime">Дата рождения</param>
        //    /// <returns></returns>
        //    public IEnumerable<Drivers>? GetDriverByDate(DateTime? dateTime)
        //    {
        //        if (dateTime is null)
        //            return null;
        //        else
        //        {
        //            var driver = _context.Drivers.Where(d => d.DateRogden == dateTime);
        //            if (driver is not null) return driver;
        //            else return null;
        //        }
        //    }

        //    /// <summary>
        //    /// Возвращает данные водителя по его идентификатору
        //    /// </summary>
        //    /// <param name="id">Идентификатор</param>
        //    /// <returns></returns>
        //    public Drivers? GetDriversId(int id)
        //    {
        //        if (id > 0)
        //        {
        //            var result = _context.Drivers.Include(d => d.FeedBacks)
        //                .Include(f => f.TaxiPools).ThenInclude(t => t.City)
        //                .Include(f => f.FeedBacks).ThenInclude(c => c.City)
        //                .Include(r => r.FeedBacks).ThenInclude(f => f.TaxiPools)
        //                .FirstOrDefault(d => d.Id == id);
        //            return result;
        //        }
        //        return null;
        //    }

        //    /// <summary>
        //    /// Сохраняет или обновляет данные о водителе в базе
        //    /// </summary>
        //    /// <param name="drivers">Данные водителя</param>
        //    /// <returns></returns>
        //    public int SaveDriver(Drivers drivers)
        //    {
        //        if (drivers is not null)
        //        {
        //            if (drivers.Id > 0)
        //            {
        //                if (_context.Drivers.Contains(drivers) == true)
        //                    _context.Drivers.Update(drivers);
        //            }

        //            else
        //                _context.Drivers.Add(drivers);

        //            _context.SaveChanges();
        //            return drivers.Id;
        //        }
        //        else
        //            return 0;
        //    }

        //    /// <summary>
        //    /// Удаляет выбранного водителя из базы
        //    /// </summary>
        //    /// <param name="cmd">Данные водителя</param>
        //    /// <returns></returns>
        //    public async Task<bool> DeleteDriver(int id)
        //    {
        //        Drivers? result = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == id);
        //        if (result is not null)
        //        {
        //            _context.Drivers.Remove(result);
        //            _context.SaveChanges();
        //            return await Task.Run(()=> true);
        //        }
        //        else
        //            return await Task.Run(() => false);

        //   }
        #endregion
    }
}