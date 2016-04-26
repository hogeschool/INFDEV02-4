/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package visitor;

import visitor.optionNoLambda.Some;
import visitor.optionNoLambda.LambdaOptionVisitor;
import visitor.optionNoLambda.IOption;
import visitor.optionNoLambda.IOptionVisitor;
//import optionLambda.*;

public class Application {

    public static void main(String[] args) {

//        NumberVisitor n_visitor = new NumberVisitor();
//        INumber n = new MyInt();
//        n.visit(n_visitor);
//        MusicLibraryVisitor music_library_visitor = new MusicLibraryVisitor();
//        ArrayList<ISong> songs = new ArrayList<ISong>();
//        songs.add(new HeavyMetal("Hallowed Be Thy Name"));
//        songs.add(new Jazz("Autumn Leaves"));
//        songs.add(new HeavyMetal("War Pigs"));
//        for (ISong song : songs) {
//            song.visit(music_library_visitor);
//        }
//        //songs.stream().forEach((song) -> {
//        //    song.visit(music_library_visitor);
//        //});
//        System.out.println("Amount of heavy metal music: " + music_library_visitor.heavyMetal.size());
//        System.out.println("Amount of jazz music: " + music_library_visitor.jazz.size());

        //OPTION VISITOR version 1
        IOptionVisitor<Integer, Integer> opt_visitor = new LambdaOptionVisitor<>(i -> i + 1, () -> {
            throw new IllegalArgumentException("Expecting a value...");
        });
        IOption<Integer> opt = new Some<>(5);
        int res = opt.visit(opt_visitor);
        System.out.println(res);

//        //OPTION VISITOR version 2
//        IOption<Integer> number = new Some<Integer>(5);
//        int inc_number = number.visit(() -> {
//            throw new IllegalArgumentException("Expecting a value...");
//        }, i -> i + 1);
//        System.out.println(inc_number);
//
//        number = new None<Integer>();
//        inc_number = number.visit(() -> {
//            throw new IllegalArgumentException("Expecting a value...");
//        }, i -> i + 1);
//        System.out.println(inc_number);

    }
}
