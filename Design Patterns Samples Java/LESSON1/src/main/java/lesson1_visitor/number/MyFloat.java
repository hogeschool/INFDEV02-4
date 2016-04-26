package lesson1_visitor.number;

import lesson1_visitor.number.INumber;

public class MyFloat implements INumber {

    public void visit(INumberVisitor visitor) {
        visitor.onFloat(this);
    }
}

