package lesson1_visitor.number;

public class MyInt implements INumber {

    public void visit(INumberVisitor visitor) {
        visitor.onInt(this);
    }
}
