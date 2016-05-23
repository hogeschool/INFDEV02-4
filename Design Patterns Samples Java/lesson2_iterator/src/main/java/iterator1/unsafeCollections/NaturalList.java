package iterator1.unsafeCollections;

public class NaturalList implements ITraditionalIterator<Integer> {

    private int current = -1;

    public Integer getCurrent() {
        if (current < 0) {
            throw new RuntimeException("moveNext first...");
        }
        return current;
    }

    public boolean moveNext() {
        current++;
        return true;
    }

    public void reset() {
        current = -1;
    }
}
