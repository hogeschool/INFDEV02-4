package optionNoLambda;

import java.util.function.Supplier;
import java.util.function.Function;

public class LambdaOptionVisitor<T, U> implements IOptionVisitor<T, U> {

    Function<T, U> onSome;
    Supplier<U> onNone;

    public LambdaOptionVisitor(Function<T, U> onSome, Supplier<U> onNone) {
        this.onSome = onSome;
        this.onNone = onNone;
    }

    public U visit(Some<T> option) {
        return onSome.apply(option.value);
    }

    public U visit(None<T> option) {
        return onNone.get();
    }
}
