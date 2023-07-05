using EL.BlackList.API.Models;

namespace EL.BlackList.API.Services.Repositore
{
    public interface IFeedBacksRepositore
    {
        Task<bool> DeleteFeedBackAsync(int id);
        FeedBacks? GetFeedBackById(int id);
        IEnumerable<FeedBacks>? GetFeedBackByUserID(string userid);
        IEnumerable<FeedBacks>? GetFeedBacks(int driverid);
        Task<int> SaveFeedBackAsync(FeedBacks feedBack);
    }
}