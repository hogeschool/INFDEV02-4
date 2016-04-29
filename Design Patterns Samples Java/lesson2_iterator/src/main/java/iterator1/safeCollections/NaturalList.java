package iterator1.safeCollections;

import visitor.optionLambda.IOption;
import visitor.optionLambda.Some;

public class NaturalList implements Iterator<Integer> {

    private int current = -1;

    public IOption<Integer>getNext() {
        current++;
        return new Some<Integer>(current);
    }
}
