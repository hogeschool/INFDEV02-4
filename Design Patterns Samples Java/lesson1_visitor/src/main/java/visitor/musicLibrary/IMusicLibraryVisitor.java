package visitor.musicLibrary;


public interface IMusicLibraryVisitor {

    void onHeavyMetal(HeavyMetal number);

    void onJazz(Jazz number);
}
