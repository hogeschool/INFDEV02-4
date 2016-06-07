/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package decorator;

import iterator1.safeCollections.Iterator;
import visitor.optionLambda.IOption;
import visitor.optionLambda.Some;


public class Naturals implements Iterator<Integer> {
    private int current;
    
    public Naturals()
    {
        this.current = -1;
    }
    @Override
    public IOption getNext() {
        current = current + 1;
        return new Some(current);
    }
    
}
