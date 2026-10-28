namespace DataLayer
{
    
    public interface IRepository<T> 
    {
        bool Add(T entity);        
        bool Update(T entity);
        bool Delete(T entity);
        List<T> GetAll();
        T? Get(int id);             
    }
}
