package lesson1_visitor.musicLibrary;

public class Jazz implements ISong {

    String title;

    public Jazz(String title) {
        this.title = title;
    }

   public void visit(IMusicLibraryVisitor visitor) {
        visitor.onJazz(this);
    }
}
