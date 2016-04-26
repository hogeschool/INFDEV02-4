package iterator;

import java.util.function.Function;

public class Map<T, U> implements GofIterator<U> {
    private GofIterator<T> decoratedCollection;
    Function<T, U> f;

    public Map(GofIterator<T> decoratedCollection, Function<T, U> f) {
        this.decoratedCollection = decoratedCollection;
        this.f = f;
    }

    @Override
    public void first() {
        decoratedCollection.next();
    }

    @Override
    public void next() {
        decoratedCollection.next();
    }

    @Override
    public boolean isDone() {
        return decoratedCollection.isDone();
    }

    @Override
    public U currentItem() {
        return f.apply(decoratedCollection.currentItem());
    }
}
