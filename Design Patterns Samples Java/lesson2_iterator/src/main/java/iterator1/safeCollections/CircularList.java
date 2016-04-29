/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package iterator1.safeCollections;

import java.util.List;
import visitor.optionLambda.IOption;
import visitor.optionLambda.Some;

public class CircularList<T> implements Iterator<T> {

    private List<T> list;
    private int index = -1;

    public CircularList(List<T> list) {
        this.list = list;
    }

    public IOption<T> getNext() {
        index = (index + 1) % list.size();

        return new Some<T>(list.get(index));
    }
}
