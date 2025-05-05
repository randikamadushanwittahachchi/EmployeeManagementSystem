using BaseLibrary.Responses;

namespace ClientLibrary.Services.Contracts
{
    public interface IGenericServiceInterface<T> where T : class
    {
        Task<List<T>> GetAll(string baseUrl);
        Task<T> GetById(string baseUrl);
        Task<GeneralResponse> Create(T model,string baseUrl);
        Task<GeneralResponse> Update(T model , string baseUrl);
        Task<GeneralResponse> DeleteById(string baseUrl);

    }
}
