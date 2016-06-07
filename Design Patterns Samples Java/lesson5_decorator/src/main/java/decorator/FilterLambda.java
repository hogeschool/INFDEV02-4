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

public class FilterLambda extends Decorator {

    Predicate<Integer> p;

    public FilterLambda(Iterator<Integer> collection, Predicate<Integer> p) {
        super(collection);
        this.p = p;
    }

    @Override
    public IOption<Integer> getNext() {
        IOption<Integer> current = super.decorated_item.getNext();
        return current.visit(
                () -> new None<Integer>(),
                c
                -> {
            if (p.test(c)) {
                return new Some<Integer>(c);
            } else {
                return this.getNext();
            }
        });
    }
}
