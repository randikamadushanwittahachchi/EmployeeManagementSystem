using BaseLibrary.Responses;

namespace ServerLibrary.Repositores.Contracts;

public interface IGenericRepositoryInterface<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T?> GetById(int id);
    Task<GeneralResponse> Create(T model);
    Task<GeneralResponse> Update(T model);
    Task<GeneralResponse> DeleteById(int id);
}
