package lesson1_visitor.optionLambda;

import java.util.function.Function;
import java.util.function.Supplier;

public class Some<T> implements IOption<T> {

    T value;

    public Some(T value) {
        this.value = value;
    }

    public <U> U visit(Supplier<U> onNone, Function<T, U> onSome) {
        return onSome.apply(value);
    }
}
