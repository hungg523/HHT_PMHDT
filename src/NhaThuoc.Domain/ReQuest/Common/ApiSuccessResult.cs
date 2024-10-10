namespace NhaThuoc.Domain.ReQuest.Common
{
    public class ApiSuccessResult<T>: ApiResult<T>
    {
        public ApiSuccessResult() { 
            IsSuccessed = true;
        }

        public ApiSuccessResult(T result)
        {
            IsSuccessed = true;
            ResultObject = result;
        }
    }
}
