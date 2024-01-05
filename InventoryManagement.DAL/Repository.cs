using Microsoft.EntityFrameworkCore;
using InventoryManagement.DAL;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly InventoryContext context;
    private DbSet<T> entities;

    public Repository(InventoryContext context)
    {
        this.context = context;
        entities = context.Set<T>();
    }

    public T GetById(int id)
    {
        return entities.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return entities.ToList();
    }

    public void Add(T entity)
    {
        entities.Add(entity);
    }

    public void Remove(T entity)
    {
        entities.Remove(entity);
    }

}
