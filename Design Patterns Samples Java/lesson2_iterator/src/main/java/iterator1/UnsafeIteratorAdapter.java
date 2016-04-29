/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package iterator1;

import iterator1.safeCollections.Iterator;
import iterator1.unsafeCollections.IUnsafeIterator;
import visitor.optionLambda.*;

public class UnsafeIteratorAdapter<T> implements Iterator<T> {

    private IUnsafeIterator<T> iterator;

    public UnsafeIteratorAdapter(IUnsafeIterator<T> iterator) {
        this.iterator = iterator;
    }

    public IOption<T> getNext() {
        if (iterator.moveNext()) {
            return new Some<T>(iterator.getCurrent());
        } else {
            return new None<T>();
        }
    }

}
