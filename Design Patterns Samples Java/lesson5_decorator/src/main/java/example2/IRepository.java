package example2;

 public interface IRepository<T>
    {
      void add(T entity);
      void delete(T entity);
      void update(T entity);
      Iterable<T> getAll();
      T getById(int id);
    }