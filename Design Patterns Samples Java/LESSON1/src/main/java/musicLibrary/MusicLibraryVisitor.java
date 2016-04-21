package musicLibrary;

import java.util.ArrayList;

public class MusicLibraryVisitor implements IMusicLibraryVisitor {

    public ArrayList<HeavyMetal> heavyMetal = new ArrayList<HeavyMetal>();
    public ArrayList<Jazz> jazz = new ArrayList<Jazz>();

    public void visit(HeavyMetal song) {
        heavyMetal.add(song);
    }

    public void visit(Jazz song) {
        jazz.add(song);
    }
}