
using BaseLibrary.Responses;

namespace ServerLibrary.Repositories.Contracts;

public interface IGenericRepositoryInterface<T>
{
    Task<List<T>> GetAll();
    Task<T> Get(int id);
    Task<GeneralResponse> Insert(T item);
    Task<GeneralResponse> Update(T item);
    Task<GeneralResponse> DeleteById(int id);

}
