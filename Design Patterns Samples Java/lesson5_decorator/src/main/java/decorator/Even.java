/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package decorator;

import iterator1.safeCollections.Iterator;
import visitor.optionNoLambda.IOption;
import visitor.optionNoLambda.None;
import visitor.optionNoLambda.Some;

public class Even extends Decorator {

    public Even(Iterator<Integer> collection) {
        super(collection);
    }

    public IOption<Integer> getNext() {
        IOption<Integer> current = super.decorated_item.getNext();
        if (current.isNone()) {
            return new None<Integer>();
        } else if (((current.getValue() % 2) == 0)) {
            return new Some<Integer>(current.getValue());
        } else {
            return this.getNext();
        }
    }

 }
