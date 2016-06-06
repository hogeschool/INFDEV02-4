package example1;




import example1.Decorator;
import iterator1.safeCollections.Iterator;
import java.util.function.Function;
import visitor.optionLambda.IOption;

public class Filter<T> implements Decorator<T> {

    Function<T, Boolean> f;
    protected Iterator<T> iterator;

    public Filter(Iterator<T> iterator, Function<T, Boolean> f) {
        this.iterator = iterator;
        this.f = f;
    }

    public IOption<T> getNext() {
        IOption<T> next = iterator.getNext();
        if (next.visit(() -> true, x -> false)) {
            return next;
        }
        return next.visit(() -> next, value
                -> {
            if (f.apply(value)) {
                return next;
            } else {
                return getNext();
            }
        });
    }
}
