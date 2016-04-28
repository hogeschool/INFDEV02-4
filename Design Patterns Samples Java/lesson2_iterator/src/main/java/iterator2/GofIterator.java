package iterator2;

public interface GofIterator<T> {
    void first();
    void next();
    boolean isDone();
    T currentItem();
}
