package musicLibrary;


interface IMusicLibraryVisitor {

    void visit(HeavyMetal number);

    void visit(Jazz number);
}
