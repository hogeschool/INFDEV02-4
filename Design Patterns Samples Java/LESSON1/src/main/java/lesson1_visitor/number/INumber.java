package lesson1_visitor.number;

public interface INumber {

    void visit(INumberVisitor visitor);
}
