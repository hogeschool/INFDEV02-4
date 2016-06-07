/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package decorator;

import iterator1.safeCollections.Iterator;
import java.util.function.Function;
import visitor.optionLambda.IOption;
import visitor.optionLambda.None;
import visitor.optionLambda.Some;

public class MapLambda extends Decorator {

    Function<Integer, Integer> t;

    public MapLambda( Iterator<Integer> collection,Function<Integer, Integer> t) {
        super(collection);

        this.t = t;
    }

    @Override
    public IOption<Integer> getNext() {
        IOption<Integer> current = super.decorated_item.getNext();
        return current.visit(() -> new None<Integer>(), c -> new Some<Integer>(t.apply(c)));
    }
}
