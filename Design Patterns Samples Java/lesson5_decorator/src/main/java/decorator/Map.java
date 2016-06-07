/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package decorator;

import iterator1.safeCollections.Iterator;
import java.security.cert.PKIXRevocationChecker.Option;
import java.util.function.Function;
import visitor.optionLambda.IOption;
import visitor.optionLambda.None;
import visitor.optionLambda.Some;

/**
 *
 * @author busal
 */
public class Map extends Decorator {

    Function<Integer, Integer> t;

    public Map(Function<Integer, Integer> t, Iterator<Integer> collection) {
        super(collection);
        this.t = t;
    }

    public IOption<Integer> getNext() {
        IOption<Integer> current = super.decorated_item.getNext();
        if (current.isNone()) {
            return new None<Integer>();
        } else {
            return new Some<Integer>(t.apply(current.getValue()));
        }
    }
}
