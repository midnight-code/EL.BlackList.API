using EL.BlackList.API.Enum;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Implementations
{
    public class TaxiPoolServices : ITaxiPoolServices
    {
        private readonly ITaxiPoolRepositore _taxPoolRepositore;
        public TaxiPoolServices(ITaxiPoolRepositore taxPoolRepositore) => _taxPoolRepositore = taxPoolRepositore;
        public async Task<IBaseResponse<bool>> DeleteTaxiPoolAsync(int id)
        {
            if(id>0)
            {
                var result = await _taxPoolRepositore.Delete(id);
                if (result)
                {
                    return await Task.Run(() => new BaseResponse<bool>()
                    {
                        Data = result,
                        StatusCode = StatusCode.OK
                    });
                }
            }
            return await Task.Run(() => new BaseResponse<bool>()
            {
                StatusCode = StatusCode.DeleteServiceError,
                Description=$"[DeleteTaxiPoolBAsync] ; Ошибка удаления!"
            });
        }

        public async Task<IBaseResponse<TaxiPools>> GetTaxiPoolByIdAsync(int id)
        {
            if (id > 0)
            {
                var result= await _taxPoolRepositore.GetById(id);
                if(result is not null)
                {
                    return await Task.Run(() => new BaseResponse<TaxiPools>()
                    {
                        Data = result,
                        StatusCode = StatusCode.OK
                    });
                }
            }
            return await Task.Run(() => new BaseResponse<TaxiPools>()
            {
                StatusCode = StatusCode.InternalServverErroe,
                Description = "Невозможно найти заданный эллемент"
            });
        }

        public async Task<IBaseResponse<int>> SaveTaxiPoolAsync(TaxiPools driver)
        {
            if(driver is not null)
            {
                var result = await _taxPoolRepositore.Save(driver);
                if(result>0)
                    return await Task.Run(() => new BaseResponse<int>()
                    {
                        Data = result,
                        StatusCode = StatusCode.OK
                    });

            }
            return await Task.Run(() => new BaseResponse<int>()
            {
                StatusCode = StatusCode.InternalServverErroe,
                Description = "Невозможно сохранить или обновить запись!!!"
            });
        }
    }
}
