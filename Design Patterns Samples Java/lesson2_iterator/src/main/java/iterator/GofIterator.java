package iterator;

import java.util.Iterator;

public interface GofIterator<T> {
    void first();
    void next();
    boolean isDone();
    T currentItem();
}
