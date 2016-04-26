package number;

public class MyFloat implements INumber {

    public void visit(INumberVisitor visitor) {
        visitor.onFloat(this);
    }
}

