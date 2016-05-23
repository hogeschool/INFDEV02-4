/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package iterator1.unsafeCollections;

import java.util.List;

public class CircularList<T> implements ITraditionalIterator<T> {

    private List<T> list;
    private int index = -1;

    public CircularList(List<T> list) {
        this.list = list;
    }

    private CircularList() {
    }

    public T getCurrent() {
        if (index < 0) {
            throw new RuntimeException("moveNext first...");
        }
        return list.get(index);
    }

    public boolean moveNext() {
        index = (index + 1) % list.size();
        return true;
    }

    public void reset() {
        index = -1;
    }
}
