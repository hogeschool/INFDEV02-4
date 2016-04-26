package lesson1_visitor.optionLambda;

import java.util.function.Function;
import java.util.function.Supplier;

public interface IOption<T> {

    <U> U visit(Supplier<U> onNone, Function<T, U> onSome);
}
