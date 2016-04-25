package musicLibrary;

import java.util.ArrayList;

public class MusicLibraryVisitor implements IMusicLibraryVisitor {

    public ArrayList<HeavyMetal> heavyMetal = new ArrayList<HeavyMetal>();
    public ArrayList<Jazz> jazz = new ArrayList<Jazz>();

    public void onHeavyMetal(HeavyMetal song) {
        heavyMetal.add(song);
    }

    public void onJazz(Jazz song) {
        jazz.add(song);
    }
}