package visitor.musicLibrary;

import java.util.ArrayList;

public class MusicLibraryVisitor implements IMusicLibraryVisitor {

    public ArrayList<HeavyMetal> heavyMetal = new ArrayList<>();
    public ArrayList<Jazz> jazz = new ArrayList<>();

    public void onHeavyMetal(HeavyMetal song) {
        heavyMetal.add(song);
    }

    public void onJazz(Jazz song) {
        jazz.add(song);
    }
}