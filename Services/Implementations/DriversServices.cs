using EL.BlackList.API.Enum;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Response;
using System;

namespace EL.BlackList.API.Services.Implementations
{
    public class DriversServices : IDriversServices
    {
        private readonly IDriversRepositore _driversRepositore;

        public DriversServices(IDriversRepositore driversRepositore) => _driversRepositore = driversRepositore;

        public async Task<IBaseResponse<bool>> DeleteDriverBAsync(int id)
        {
            if (id>0)
            {
                bool result = await _driversRepositore.Delete(id);
                return await Task.Run(() => new BaseResponse<bool>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                });
            }
            return await Task.Run(() => new BaseResponse<bool>()
            {
                Description = $"Not Found"
            });
        }

        public async Task<IBaseResponse<IEnumerable<Drivers>>> GetDriversByDateAsync(DateTime? date)
        {
            try
            {
                var driver = await _driversRepositore.GetDriverByDateAsunc(date);
                if (driver is not null)
                {
                    return await Task.Run(() => new BaseResponse<IEnumerable<Drivers>>()
                    {
                        Data = driver,
                        StatusCode = StatusCode.OK
                    });
                }
                return await Task.Run(() => new BaseResponse<IEnumerable<Drivers>>()
                {
                    Description = $"[GetDriversAsync] : {StatusCode.InternalServverErroe}"
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BaseResponse<IEnumerable<Drivers>>()
                {
                    Description = $"[GetDriversAsync] : {ex.Message}"
                });
            }
        }

        public async Task<IBaseResponse<IEnumerable<Drivers>>> GetDriversByNameAsync(string firstName, string? lastName, string? secondName, DateTime? dateTime)
        {
            try
            {
                var drivers = await _driversRepositore.GetDriverByName(firstName, lastName, secondName, dateTime);

                if (drivers is not null)
                {
                    return await Task.Run(() => new BaseResponse<IEnumerable<Drivers>>()
                    {
                        Data = drivers,
                        StatusCode = StatusCode.OK
                    });
                }
                return await Task.Run(() => new BaseResponse<IEnumerable<Drivers>>()
                {
                    Description = $"[GetDriversAsync] : {StatusCode.InternalServverErroe}"
                });
            }
            catch(Exception ex)
            {
                return await Task.Run(() => new BaseResponse<IEnumerable<Drivers>>()
                {
                    Description = $"[GetDriversAsync] : {ex.Message}"
                });
            }
        }

        public async Task<IBaseResponse<Drivers>> GetDrivreByIdAsync(int id)
        {
            try
            {
                var driver = await _driversRepositore.GetById(id);
                if(driver is not null)
                {
                    return await Task.Run(() => new BaseResponse<Drivers>()
                    {
                        Data = driver,
                        StatusCode = StatusCode.OK
                    });
                }
                return await Task.Run(() => new BaseResponse<Drivers>
                {
                    Description = $"[GetDriversAsync] : {StatusCode.InternalServverErroe}"
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BaseResponse<Drivers>()
                {
                    Description = $"[GetDriversAsync] : {ex.Message}"
                });
            }
        }

        public async Task<IBaseResponse<int>> SaveDriverAsync(Drivers driver)
        {
            try
            {
                if(driver is not null)
                {
                    int id = await _driversRepositore.Save(driver);
                    return await Task.Run(() => new BaseResponse<int>()
                    {
                        Data = id,
                        Description = $"Not Found"
                    });
                }
                return await Task.Run(() => new BaseResponse<int>()
                {
                    Description = $"Not Found"
                });
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BaseResponse<int>()
                {
                    Description = $"[GetDriversAsync] : {ex.Message}"
                });
            }
        }
    }
}
