/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package decorator;

import iterator1.safeCollections.Iterator;
import visitor.optionLambda.IOption;

abstract class Decorator implements Iterator<Integer> {

    protected Iterator<Integer> decorated_item;

    public Decorator(Iterator<Integer> decorated_item) {
        this.decorated_item = decorated_item;
    }

    @Override
    public abstract IOption<Integer> getNext();
}
