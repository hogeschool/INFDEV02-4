package iterator1;

class Program {

    static void main(String[] args) throws InterruptedException {
        IEnumerator<Integer> elems = new NaturalList();
        Map<Integer, String> mapped_elems = new Map<Integer, String>(elems, x -> x.toString() + " is a string now..");
        mapped_elems.moveNext();
        while (true) {
            System.out.println(mapped_elems.getCurrent());
            mapped_elems.moveNext();
            Thread.sleep(100);
        }

    }
}
