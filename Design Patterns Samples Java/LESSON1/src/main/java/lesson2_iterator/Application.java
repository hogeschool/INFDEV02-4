package lesson2_iterator;

import java.util.Arrays;

public class Application {

    public static void main(String[] args) {
//        runNaturalNumbers();
        runInfiniteLoop();
    }


    private static void runInfiniteLoop(){
        InfiniteLoopListIterator illi =
                new InfiniteLoopListIterator(Arrays.asList(0,1,2,3));

        int i = 0;
        while ( !illi.isDone() && i < 10){
            illi.next();
            System.out.println(illi.currentItem());
            i++;
        }

    }
    private static void runNaturalNumbers(){
        NaturalNumbers naturalNumbers = new NaturalNumbers();

        int i = 0;
        while(! naturalNumbers.isDone() && i < 10){
            naturalNumbers.next();
            System.out.println(naturalNumbers.currentItem());
            i++;
        }
    }
}
