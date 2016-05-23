package iterator1.unsafeCollections;

import java.util.function.Function;

public class Map<T, U> implements ITraditionalIterator<U> {

    private ITraditionalIterator<T> decoratedCollection;
    private Function<T, U> f;

    public Map(ITraditionalIterator<T> collection, Function<T, U> f) {
        this.decoratedCollection = collection;
        this.f = f;
    }

    public U getCurrent() {
        return f.apply(decoratedCollection.getCurrent());
    }

    public boolean moveNext() {
        return decoratedCollection.moveNext();
    }
}
