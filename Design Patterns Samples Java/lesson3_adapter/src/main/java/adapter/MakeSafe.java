/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package adapter;

import iterator1.safeCollections.Iterator;
import iterator1.unsafeCollections.ITraditionalIterator;
import visitor.optionLambda.IOption;
import visitor.optionLambda.None;
import visitor.optionLambda.Some;

public class MakeSafe<T> implements Iterator<T> {

    private ITraditionalIterator<T> iterator;

    public MakeSafe(ITraditionalIterator<T> iterator) {
        this.iterator = iterator;
    }

    @Override
    public IOption<T> getNext() {
        if (iterator.moveNext()) {
            return new Some<T>(iterator.getCurrent());
        } else {
            return new None<T>();
        }
    }

}
