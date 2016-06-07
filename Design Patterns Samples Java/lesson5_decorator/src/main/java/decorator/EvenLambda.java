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

public class EvenLambda extends Decorator {

    public EvenLambda(Iterator<Integer> collection) {
        super(collection);
    }

    @Override
    public IOption<Integer> getNext() {
        IOption<Integer> current = super.decorated_item.getNext();

        return current.visit(
                () -> new None<Integer>(),
                c -> {
                    if (((c % 2) == 0)) {
                        return new Some<Integer>(c);
                    } else {
                        return this.getNext();
                    }
                });
    }
}
