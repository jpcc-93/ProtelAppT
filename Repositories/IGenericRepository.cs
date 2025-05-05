namespace ProtelAppT.Repositories
{
    public interface IGenericRepository
    {
        Task<T> GetByIdAsync<T>(int id) where T : class;
        Task<List<T>> GetAllAsync<T>() where T : class;
        Task AddAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task SaveChangesAsync();
    }

}
