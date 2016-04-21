/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package INFDEV4.lesson1;

import java.util.ArrayList;

import number.*;
import musicLibrary.*;
//import optionNoLambda.*;
import optionLambda.*;

public class Lesson1 {

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

//        //OPTION VISITOR version 1
//        IncOptionVisitor<Integer, Integer> opt_visitor = new IncOptionVisitor<Integer, Integer>(i -> i + 1, () -> {
//            throw new IllegalArgumentException("Expecting a value...");
//        });
//        IOption<Integer, Integer> opt = new Some<Integer, Integer>(5);
//        int res = opt.accept(opt_visitor);
//        System.out.println(res);

        //OPTION VISITOR version 2
        IOption<Integer> number = new Some<Integer>(5);
        int inc_number = number.visit(() -> {
            throw new IllegalArgumentException("Expecting a value...");
        }, i -> i + 1);
        System.out.println(inc_number);

        number = new None<Integer>();
        inc_number = number.visit(() -> {
            throw new IllegalArgumentException("Expecting a value...");
        }, i -> i + 1);
        System.out.println(inc_number);

    }
}
