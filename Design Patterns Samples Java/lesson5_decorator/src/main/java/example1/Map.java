package example1;


import example1.Decorator;
import iterator1.safeCollections.Iterator;
import java.util.function.Function;

import visitor.optionLambda.IOption;
import visitor.optionLambda.None;
import visitor.optionLambda.Some;

public class Map<T, U> implements Decorator<U> {

    Function<T, U> t;
    protected Iterator<T> iterator;

    public Map(Iterator<T> iterator, Function<T, U> t) {
        this.iterator = iterator;
        this.t = t;
    }

    public IOption<U> getNext() {
        return iterator.getNext().visit(() -> new None(), value -> new Some(t.apply(value)));
    }
}
