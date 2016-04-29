package iterator1;

import iterator1.safeCollections.Iterator;
import iterator1.unsafeCollections.IUnsafeIterator;
import iterator1.unsafeCollections.Map;
import iterator1.unsafeCollections.NaturalList;

class Program {

    static void main(String[] args) throws InterruptedException {
        Iterator<Integer> list = new UnsafeIteratorAdapter<Integer>(new iterator1.unsafeCollections.NaturalList());

        IUnsafeIterator<Integer> elems = new NaturalList();
        Map<Integer, String> mapped_elems = new Map<Integer, String>(elems, x -> x.toString() + " is a string now..");
        mapped_elems.moveNext();
        while (true) {
            System.out.println(mapped_elems.getCurrent());
            mapped_elems.moveNext();
            Thread.sleep(100);
        }
    }
}
