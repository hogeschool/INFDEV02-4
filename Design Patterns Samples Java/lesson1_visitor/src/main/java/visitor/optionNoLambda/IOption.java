package visitor.optionNoLambda;

public interface IOption<T> {

   <U> U visit(IOptionVisitor<T, U> visitor);
   T getValue();
   Boolean isNone();
}
