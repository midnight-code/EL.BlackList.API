using EL.BlackList.API.Enum;
using EL.BlackList.API.Models;
using EL.BlackList.API.Repositore.Interfaces;
using EL.BlackList.API.Services.Interfaces;
using EL.BlackList.API.Services.Repositore;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Implementations
{
    public class FeedBackServices : IFeedBackServices
    {
        private readonly IFeedBacksRepositore _feedBackRepositore;
        public FeedBackServices(IFeedBacksRepositore feedBackRepositore) => _feedBackRepositore = feedBackRepositore;

        public async Task<IBaseResponse<bool>> DeleteFeedBacksByAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    bool result = await _feedBackRepositore.Delete(id);
                    if (result)
                    {
                        return await Task.Run(() => new BaseResponse<bool>()
                        {
                            Data = result,
                            StatusCode = StatusCode.OK
                        });
                    }
                    else
                    {
                        return await Task.Run(() => new BaseResponse<bool>()
                        {
                            Description = "Невозможно удалить, отзыв не найден!",
                            StatusCode = StatusCode.DeleteServiceError
                        });
                    }
                }
                else
                {
                    return await Task.Run(() => new BaseResponse<bool>()
                    {
                        Description = "Невозможно удалить отзыв с нулевым или отрецательным значением!",
                        StatusCode = StatusCode.DeleteServiceError
                    });
                }
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BaseResponse<bool>()
                {
                    Description = $"[DeleteFeedBacksByAsync] : {ex.Message}"
                });
            }
        }

        public async Task<IBaseResponse<FeedBacks>> GetFeedBackByIdAsync(int id)
        {
            try {
                if (id > 0)
                {
                    var result = await _feedBackRepositore.GetById(id);
                    if (result is not null)
                    {
                        return await Task.Run(() => new BaseResponse<FeedBacks>()
                        {
                            Data = result,
                            StatusCode = StatusCode.OK
                        });
                    }
                    else
                    {
                        return await Task.Run(() => new BaseResponse<FeedBacks>()
                        {
                            Description = "Невозможно найти отзыв!",
                            StatusCode = StatusCode.DeleteServiceError
                        });
                    }
                }
                else
                {
                    return await Task.Run(() => new BaseResponse<FeedBacks>()
                    {
                        Description = "Невозможно найти отзыв с нулевым или отрецательным значением!",
                        StatusCode = StatusCode.DeleteServiceError
                    });
                }
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BaseResponse<FeedBacks>()
                {
                    Description = $"[DeleteFeedBacksByAsync] : {ex.Message}"
                });
            }
        }

        public async Task<IBaseResponse<IEnumerable<FeedBacks>>> GetFeedBacksByDriverIdAsync(int driverId)
        {
            try
            {
                if (driverId > 0)
                {
                    var result = await _feedBackRepositore.GetFeedBacksByDriverIdAsync(driverId);
                    if (result is not null)
                    {
                        return await Task.Run(() => new BaseResponse<IEnumerable<FeedBacks>>()
                        {
                            Data = result,
                            StatusCode = StatusCode.OK
                        });
                    }
                    else
                    {
                        return await Task.Run(() => new BaseResponse<IEnumerable<FeedBacks>>()
                        {
                            Description = "Невозможно найти отзывы для данного водителя!",
                            StatusCode = StatusCode.DeleteServiceError
                        });
                    }
                }
                else
                {
                    return await Task.Run(() => new BaseResponse<IEnumerable<FeedBacks>>()
                    {
                        Description = "Невозможно найти отзыв с нулевым или отрецательным значением!",
                        StatusCode = StatusCode.DeleteServiceError
                    });
                }
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BaseResponse<IEnumerable<FeedBacks>>()
                {
                    Description = $"[DeleteFeedBacksByAsync] : {ex.Message}"
                });
            }
        }

        public async Task<IBaseResponse<IEnumerable<FeedBacks>>> GetFeedBacksByUserAsync(string userName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(userName))
                {
                    var result = await _feedBackRepositore.GetFeedBackByUserIDAsync(userName);
                    if (result is not null)
                    {
                        return await Task.Run(() => new BaseResponse<IEnumerable<FeedBacks>>()
                        {
                            Data = result,
                            StatusCode = StatusCode.OK
                        });
                    }
                    else
                    {
                        return await Task.Run(() => new BaseResponse<IEnumerable<FeedBacks>>()
                        {
                            Description = "Пользователь не оставлял отзыв!",
                            StatusCode = StatusCode.DeleteServiceError
                        });
                    }
                }
                else
                {
                    return await Task.Run(() => new BaseResponse<IEnumerable<FeedBacks>>()
                    {
                        Description = "Невозможно найти отзыв с нулевым или отрецательным значением!",
                        StatusCode = StatusCode.DeleteServiceError
                    });
                }
            }
            catch (Exception ex)
            {
                return await Task.Run(() => new BaseResponse<IEnumerable<FeedBacks>>()
                {
                    Description = $"[DeleteFeedBacksByAsync] : {ex.Message}"
                });
            }
        }

        public async Task<IBaseResponse<int>> SaveFeedBacksAsync(FeedBacks intite)
        {
            try
            {
                if (intite is not null)
                {
                    int id = await _feedBackRepositore.Save(intite);
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
                    Description = $"[SaveFeedBacksAsync] : {ex.Message}"
                });
            }
        }
    }
}
