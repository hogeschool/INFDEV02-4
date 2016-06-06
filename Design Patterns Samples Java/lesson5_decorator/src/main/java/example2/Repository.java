package example2;

public class Repository<T> implements IRepository<T> {

    @Override
    public void add(T entity) {
        System.out.printf("Adding %s\n", entity);
    }

    @Override
    public void delete(T entity) {
        System.out.printf("Deleting %s\n", entity);
    }

    @Override
    public Iterable<T> getAll() {
        System.out.printf("Returning all...\n");
        return null;
    }

    @Override
    public T getById(int id) {
        System.out.printf("Getting entity with id: %d\n", id);
        return null;
    }

    @Override
    public void update(T entity) {
        System.out.printf("Updating %s\n", entity);
    }
}
