package visitor.optionLambda;

import java.util.function.Function;
import java.util.function.Supplier;

public class None<T> implements IOption<T> {

    public <U> U visit(Supplier<U> onNone, Function<T, U> onSome) {
        return onNone.get();
    }
}
