package visitor.musicLibrary;

import java.util.ArrayList;
import java.util.List;

public class MusicLibraryVisitor implements IMusicLibraryVisitor {

    public List<HeavyMetal> heavyMetal = new ArrayList<>();
    public List<Jazz> jazz = new ArrayList<>();

    public void onHeavyMetal(HeavyMetal song) {
        heavyMetal.add(song);
    }

    public void onJazz(Jazz song) {
        jazz.add(song);
    }
}