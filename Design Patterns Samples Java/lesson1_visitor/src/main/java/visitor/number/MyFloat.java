package visitor.number;

import visitor.number.INumber;

public class MyFloat implements INumber {

    public void visit(INumberVisitor visitor) {
        visitor.onFloat(this);
    }
}

