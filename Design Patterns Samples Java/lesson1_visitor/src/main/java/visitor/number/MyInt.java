package visitor.number;

public class MyInt implements INumber {

    public void visit(INumberVisitor visitor) {
        visitor.onInt(this);
    }
}
