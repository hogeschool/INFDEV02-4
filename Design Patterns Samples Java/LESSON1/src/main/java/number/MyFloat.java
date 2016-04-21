package number;

class MyFloat implements INumber {

    public void visit(INumberVisitor visitor) {
        visitor.visit(this);
    }
}

