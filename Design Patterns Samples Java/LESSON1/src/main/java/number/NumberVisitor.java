package number;

public class NumberVisitor implements INumberVisitor {

    public void visit(MyFloat number) {
        System.out.println("Found a float and now?");
    }

    public void visit(MyInt number) {
        System.out.println("Found an int and now?!");
    }
}
