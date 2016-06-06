/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package factory;

import java.util.ArrayList;
import java.util.List;

/**
 *
 * @author busal
 */
public class Program {

    public static void main(String[] args) {

        FiguurFactory shapeFactory = new FiguurFactory();

        //get an object of Circle and call its draw method.
        List<Figuur> figuren = new ArrayList();
        figuren.add(shapeFactory.create("CIRCLE"));
        figuren.add(shapeFactory.create("RECTANGLE"));
        figuren.add(shapeFactory.create("SQUARE"));

        for (Figuur f : figuren) {
            f.draw();
            System.out.println(f.getSurface());
        }
    }
}
