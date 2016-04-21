package optionNoLambda;


import java.util.function.Supplier;
import java.util.function.Function;


public class IncOptionVisitor<T, U> implements IOptionVisitor<T, U> {

    Function<T, U> onSome;
    Supplier<U> onNone;

    public IncOptionVisitor(Function<T, U> onSome, Supplier<U> onNone) {
        this.onSome = onSome;
        this.onNone = onNone;
    }

    public U visit(None<T, U> option) {
        return onNone.get();
    }

    public U visit(Some<T, U> option) {
        return onSome.apply(option.value);
    }

}




