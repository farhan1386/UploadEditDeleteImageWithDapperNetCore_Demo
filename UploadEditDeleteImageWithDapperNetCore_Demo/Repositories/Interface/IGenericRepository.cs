namespace UploadEditDeleteImageWithDapperNetCore_Demo.Repositories.Interface
{
    public interface IGenericRepository<T>where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> Find(Guid uid);
        Task<T> Add(T model);
        Task<T> Update(T model);
        Task<int> Remove(T model);
    }
}
