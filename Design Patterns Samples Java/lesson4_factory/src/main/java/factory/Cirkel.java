package factory;

public class Cirkel implements Figuur {

    public Cirkel() {
        super();
    }

    @Override
    public void draw() {
        System.out.println("This is a circle");
    }

    @Override
    public float getSurface() {
        return 0;
    }

}
