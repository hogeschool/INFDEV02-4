package iterator1;

public class NaturalList implements IEnumerator<Integer> {

    private Integer current = -1;

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
