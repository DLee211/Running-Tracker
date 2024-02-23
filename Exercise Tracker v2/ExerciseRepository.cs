using Microsoft.EntityFrameworkCore;

namespace Exercise_Tracker;

public class ExerciseRepository<T> : IExerciseRepository<T> where T : class
{
    private readonly ExerciseDbContext _context;
    private readonly DbSet<T> _dbSet;

    public ExerciseRepository(ExerciseDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<T>();
    }

    public ExerciseDbContext GetContext()
    {
        return _context;
    }
    
    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _context.SaveChanges();
    }
}
