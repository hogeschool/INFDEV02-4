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

class Offset extends Decorator {

    private Integer offset;

    public Offset(Integer offset, Iterator<Integer> collection) {
        super(collection);
        this.offset = offset;
        
    }
    public  IOption <Integer> getNext() {
        IOption<Integer> current = super.decorated_item.getNext();
        if (current.isNone()) {
            return new None<Integer>();
        } else {
            return new Some<Integer>((current.getValue() + offset));
        }
    }
}
