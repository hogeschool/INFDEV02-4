/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package factory;

/**
 *
 * @author busal
 */
public class FiguurFactory {

	
   //use getShape method to get object of type shape 
   public Figuur create(String shapeType){
      if(shapeType == null){
         return null;
      }		
      if(shapeType.equalsIgnoreCase("CIRCLE")){
         return new Cirkel();
         
      } else if(shapeType.equalsIgnoreCase("RECTANGLE")){
         return new Rechthoek();
         
      } else if(shapeType.equalsIgnoreCase("SQUARE")){
         return new Vierkant();
      }
      
      return null;
   }
}
