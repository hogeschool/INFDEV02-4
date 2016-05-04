module Chapter4.Week4.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime


// GENERAL IDEA
let slides = 
  [
    ! @"In this second part we are going to study a creational pattern: the factory design pattern"
    ! @"Creating objects often require complex instantiation code not appropriate to include within composing objects. Moreover, objects creation may lead to significant code duplication, which may require not accessible information."

    ! @"In particular having virtual constructors is not possible by means of traditional constructors"
    ! @"Why do we need it? For example we might have some logic that is tightly related to the creation of concrete classes that share some common type"

    ! @"For example"
    ! @"We finally managed to decouple the client from concrete Animals"
    ! @"public interface Animal { MakeSound(); }"
    ! @"class Cat : Animal {} class Dog : Animal {} class Dolphin : Animal {}"
    ! @"Imagine a logic that associates a number to every concrete class. We use such numbers so to allow the user to select the concrete animal: 0 -> car, 1 -> dog, dolphin -> 2"
    ! @"Our client program has to capture such description since it not possible to decide inside a construct: once we are inside a constructor the object is instantiated in the heap :("
    ! @"list<animal> animals;
        animals.add(match input: 0 -> new cat | 1 -> new dog | 2 -> new dolphin)"
    ! @"What about all other programs that might use our animals. Are they all aware of such classification int -> animal? Are they supposed to repeat such code every time?"
    ! @"A solution would be to add to the Animal a method/function that implements the above logic and returns an instance of concrete animal (we write it once and use it everywhere)"
    ! @"To avoid clients to use different, potentially wrong, entry points for our animal we define all constructors private"

    
    ! @"Another example"
    ! @"A concrete class is not aware how it is going to be used, however it uses methods protected for its internal instantiation mechanism"
    ! @"Again concrete classes are not aware how they are going to be combined, a general class is not aware of all concrete instances and in particular of what is the next concrete which is going to be used"
    ! @"A solution to such issue would be to provide a separate method for the creation, which subclasses can then ``override'' to specify the derived type object that will be created"
    ! @" // MazeGame offers a general logic mechanism for creating ordinary mazes. makeRoom is our factory method that encapsulates the logic for creating rooms that can 
         // used by MazeGame subclasses
public class MazeGame {
  public MazeGame() {
     Room room1 = makeRoom();
     Room room2 = makeRoom();
     room1.connect(room2);
     this.addRoom(room1);
     this.addRoom(room2);
  }

  protected Room makeRoom() {
     return new OrdinaryRoom();
  }
}
      //to define new modes we simply need to redefine the make room factory method
public class MagicMazeGame extends MazeGame {
  @Override
  protected Room makeRoom() {
      return new MagicRoom();
  }
}


"    
    ! @"The two sample are from the high-level point of view the same,only the first one uses input to select the concrete class and the second one inheritance (through dispatching)"
    ! @"However in both cases we managed to decouple the instantiation mechanism of general polymorphic types from the derived concrete classes"

    ! @"The just described mechanism is commonly referred as factory design pattern"
    ! @"Formalization"
    ! @"Conclusions"


     
  ]