package iterator;

import java.util.List;

public class InfiniteLoopListIterator<T> implements GofIterator<T> {

    private List<T> list;
    private int index = -1;

    public InfiniteLoopListIterator(List<T> list) {
        this.list = list;
    }

    @Override
    public void first() {
        index = -1;
    }

    @Override
    public void next() {
        if (++index >= list.size()) {
            first();
            next();
        }
    }

    @Override
    public boolean isDone() {
        return false;
    }

    @Override
    public T currentItem() {
        if (index < 0) {
            throw new RuntimeException("call next() first");
        } else {
            return list.get(index);
        }
    }
}
