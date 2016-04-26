package lesson1_visitor.number;

public class NumberVisitor implements INumberVisitor {

    public void onFloat(MyFloat number) {
        System.out.println("Found a float and now?");
    }

    public void onInt(MyInt number) {
        System.out.println("Found an int and now?!");
    }
}
