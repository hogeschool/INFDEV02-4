package optionNoLambda;

public interface IOption<T, U> {

    U accept(IncOptionVisitor<T, U> visitor);
}
