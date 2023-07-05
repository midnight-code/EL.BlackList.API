using EL.BlackList.API.Models;

namespace EL.BlackList.API.Repositore.Interfaces
{
    public interface IFeedBacksRepositore : IBaseRepositore<FeedBacks>
    {
        Task<IEnumerable<FeedBacks>?> GetFeedBacksByDriverIdAsync(int driverid);
        Task<IEnumerable<FeedBacks>?> GetFeedBackByUserIDAsync(string userid);
    }
}
