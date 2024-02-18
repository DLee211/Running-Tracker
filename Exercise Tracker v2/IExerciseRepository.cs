namespace Exercise_Tracker;

public interface IExerciseRepository<T>
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Delete(T entity);
}