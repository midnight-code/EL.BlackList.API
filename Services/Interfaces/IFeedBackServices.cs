using EL.BlackList.API.Models;
using EL.BlackList.API.Services.Response;

namespace EL.BlackList.API.Services.Interfaces
{
    public interface IFeedBackServices
    {
        Task<IBaseResponse<IEnumerable<FeedBacks>>> GetFeedBacksByUserAsync(string userName);
        Task<IBaseResponse<IEnumerable<FeedBacks>>> GetFeedBacksByDriverIdAsync(int driverId);
        Task<IBaseResponse<FeedBacks>> GetFeedBackByIdAsync(int id);
        Task<IBaseResponse<int>> SaveFeedBacksAsync(FeedBacks intite);
        Task<IBaseResponse<bool>> DeleteFeedBacksByAsync(int id);
    }
}
