/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package iterator1.unsafeCollections;

public class Array<T> implements ITraditionalIterator<T> {

    private T[] array;
    private int index = -1;

    public Array(T[] array) {
        this.array = array;
    }

    private Array() {
    }

    public T getCurrent() {
        if (index < 0) {
            throw new RuntimeException("moveNext first...");
        }
        return array[index];

    }

    public boolean moveNext() {
        if (index + 1 < array.length) {
            return false;
        }
        index++;
        return true;
    }

    public void reset() {
        index = -1;
    }

}
