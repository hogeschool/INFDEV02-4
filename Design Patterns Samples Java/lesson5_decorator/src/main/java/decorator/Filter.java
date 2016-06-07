/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package decorator;

import iterator1.safeCollections.Iterator;
import java.util.function.Predicate;
import visitor.optionLambda.IOption;
import visitor.optionLambda.None;
import visitor.optionLambda.Some;

class Filter extends Decorator {

    Predicate<Integer> p;

    public Filter(Iterator<Integer> collection, Predicate<Integer> p) {
        super(collection);
        this.p = p;
    }

    public IOption<Integer> getNext() {
        IOption<Integer> current = super.decorated_item.getNext();
        if (current.isNone()) {
            return new None<Integer>();
        } else if (p.test(current.getValue())) {
            return new Some<Integer>(current.getValue());
        } else {
            return this.getNext();
        }
    }
}
