package factory;

public class Vierkant implements Figuur {

    public Vierkant() {
        super();
    }

    @Override
    public void draw() {
        System.out.println("This is a square");
    }

    @Override
    public float getSurface() {
        return 0;
    }

}
