package lesson1_visitor.optionNoLambda;

public interface IOption<T> {

   <U> U visit(IOptionVisitor<T, U> visitor);
}
