/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package iterator1.safeCollections;

import visitor.optionLambda.IOption;
import visitor.optionLambda.None;
import visitor.optionLambda.Some;

public class Array<T> implements Iterator<T> {

    private T[] array;
    private int index = -1;

    public Array(T[] array) {
        this.array = array;
    }

    public IOption<T> getNext() {
        if (index + 1 < array.length) {
            return new None<T>();
        }
        index++;
        return new Some<T>(array[index]);
    }
}
