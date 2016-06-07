
import decorator.*;
import iterator1.safeCollections.Iterator;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author busal
 */
public class Program1 {
    public static void main (String[] args) {
        Iterator<Integer> ns1 = new EvenLambda(new Naturals());
        ns1.getNext();
        Iterator<Integer> ns2 = new EvenLambda(new OffsetLambda(3,new Naturals()));
        ns2.getNext();
        ns2.getNext();
        Iterator<Integer> ns3 = new FilterLambda(new MapLambda(new Naturals(), c -> c + 3 ), c -> (c % 2) == 0);
        ns3.getNext();
    }
    
}
