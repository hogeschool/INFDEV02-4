package musicLibrary;

public class HeavyMetal implements ISong {

    String title;

    public HeavyMetal(String title) {
        this.title = title;
    }

    public void visit(IMusicLibraryVisitor visitor) {
        visitor.visit(this);
    }
}