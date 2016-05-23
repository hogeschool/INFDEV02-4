/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package adapter;

import iterator1.unsafeCollections.ITraditionalIterator;
import iterator1.safeCollections.Iterator;
import visitor.optionLambda.IOption;

public class MakeUnsafe<T> implements ITraditionalIterator<T> {

    private Iterator<T> iterator;

    public MakeUnsafe(Iterator<T> iterator) {
        this.iterator = iterator;
    }
    private T _current;

    @Override
    public T getCurrent() {

        return _current;

    }

    @Override
    public boolean moveNext() {
        IOption<T> opt = iterator.getNext();

        if (opt.visit(() -> false, x -> true)) {
            _current = opt.visit(() -> null, v -> v);
            return true;
        }
        return false;
    }
}
