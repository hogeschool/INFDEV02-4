package lesson1_visitor.optionNoLambda;

import java.util.function.Supplier;
import java.util.function.Function;

public class LambdaOptionVisitor<T, U> implements IOptionVisitor<T, U> {

    Function<T, U> onSome;
    Supplier<U> onNone;

    public LambdaOptionVisitor(Function<T, U> onSome, Supplier<U> onNone) {
        this.onSome = onSome;
        this.onNone = onNone;
    }

    public U onSome(T value) {
        return onSome.apply(value);
    }

    public U onNone() {
        return onNone.get();
    }
}
