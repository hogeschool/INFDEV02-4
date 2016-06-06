package iterator1.safeCollections;

import visitor.optionLambda.IOption;
import visitor.optionLambda.Some;

public class NaturalList implements Iterator<Integer> {

    private int current = -1;

    @Override
    public IOption<Integer> getNext() {
        current++;
        return new Some<>(current);
    }
}
