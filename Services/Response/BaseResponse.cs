using EL.BlackList.API.Enum;

namespace EL.BlackList.API.Services.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; } = string.Empty;
        public StatusCode StatusCode { get; set; }

        public T? Data { get; set; }

    }

    public interface IBaseResponse<T>
    {
        T? Data { get; }
    }
}
