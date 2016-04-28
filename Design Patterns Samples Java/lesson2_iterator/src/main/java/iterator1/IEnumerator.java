package iterator1;

public interface IEnumerator<T> {

    boolean moveNext();

    T getCurrent();

    void reset();
}
