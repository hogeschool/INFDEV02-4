/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package decorator;

import iterator1.safeCollections.Iterator;
import visitor.optionLambda.IOption;
import visitor.optionLambda.None;
import visitor.optionLambda.Some;

public class OffsetLambda extends Decorator {

    private int offset;

    public OffsetLambda(int offset, Iterator<Integer> collection) {
        super(collection);
        this.offset = offset;
    }

    @Override
    public IOption<Integer> getNext() {
        IOption<Integer> current = super.decorated_item.getNext();
        return current.visit(() -> new None<Integer>(),
                c -> new Some<Integer>((c + offset)));
    }
}
