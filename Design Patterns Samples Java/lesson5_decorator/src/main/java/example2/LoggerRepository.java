package example2;

public class LoggerRepository<T> implements IRepository<T> {

    public static final String ANSI_RESET = "\u001B[0m";
    public static final String ANSI_BLACK = "\u001B[30m";
    public static final String ANSI_RED = "\u001B[31m";

    private final IRepository<T> repository;

    public LoggerRepository(IRepository<T> repository) {
        this.repository = repository;
    }

    public void Log(String msg, Object arg) {
        System.out.print(ANSI_RED);
        System.out.printf(msg, arg);
        System.out.print(ANSI_RESET);
    }

    @Override
    public void add(T entity) {
        Log("In decorator: before adding %s\n", entity);
        repository.add(entity);
        Log("In decorator: after adding %s\n", entity);
    }

    @Override
    public void delete(T entity) {
        Log("In decorator: before deleting %s\n", entity);
        repository.delete(entity);
        Log("In decorator: after deleting %s\n", entity);
    }

    @Override
    public Iterable<T> getAll() {
        Log("In decorator: before get all\n", null);
        Iterable<T> res = repository.getAll();
        Log("In decorator: after get all\n", null);
        return res;
    }

    @Override
    public T getById(int id) {
        Log("In decorator: before get by id %d\n", id);
        T res = repository.getById(id);
        Log("In decorator: after get by id %d\n", id);
        return res;
    }

    @Override
    public void update(T entity) {
        Log("In decorator: before update %s\n", entity);
        repository.update(entity);
        Log("In decorator: after update %s\n", entity);
    }
}
