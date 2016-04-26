package iterator;

public class NaturalNumbers implements GofIterator<Integer>{
    private final int FIRST = -1;
    private int current = FIRST;

    @Override
    public void first() {
        current = FIRST;
    }

    @Override
    public void next() {
        current++;
    }

    @Override
    public boolean isDone() {
        return false;
    }

    @Override
    public Integer currentItem() {
        if (current < 0) {
            throw new RuntimeException("call next() first");
        }else{
            return current;
        }
    }
}
