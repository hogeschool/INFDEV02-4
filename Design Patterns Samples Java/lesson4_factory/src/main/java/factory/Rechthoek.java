package factory;

public class Rechthoek implements Figuur {

    public Rechthoek() {
        super();
    }

    @Override
    public void draw() {
        System.out.println("This is a rectangle");
    }

    @Override
    public float getSurface() {
        return 0;
    }

}
