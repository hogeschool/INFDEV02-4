module Chapter4.Week4.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime


// GENERAL IDEA
let slides (title : string) = 
  [
    Section(sprintf "%s" title)
    Section("Introduction")
    SubSection("Lecture topics")
    ItemsBlock
      [
        ! @"..."
      ]
    Section("The factory design pattern")
    SubSection("Introduction")
    ItemsBlock
      [
        ! @"Instantiating derived types sometimes requires complex instantiation mechanisms not appropriate (or even not possible) to include their constructors, since this may lead to significant code duplication or the need for not accessible information"
        ! @"The factory design pattern (a creational pattern) tackles such limitations by promoting virtual constructors that are able express the general shape of such mechanisms and leave concrete details to the concrete classes"
        ! @"This design pattern is going to be the topic of this lecture"
      ]
    SubSection "Motivations"
    ItemsBlock
      [
        ! @"Sometimes we might have some logic that is tightly related to the creation of concrete classes that share some common type"
      ]
    SubSection "Our first example"
    VerticalStack
      [
        ItemsBlockWithTitle "Our first example"
          [
            ! @"For example"
            ! @"Consider the following classes all inheriting a polymorphic type \texttt{Animal}"            
          ]
        CSharpCodeBlock( TextSize.Tiny,
                      (interfaceDef "Animal"
                        [
                         typedSig "MakeSound" [] "void"
                        ] >>
                       classDef "Cat"  [ implements "Animal";  typedDef "MakeSound" [] "void" (Code.Dots) |> makePublic ] >>
                       classDef "Dog"  [ implements "Animal";  typedDef "MakeSound" [] "void" (Code.Dots) |> makePublic ] >>
                       classDef "Dolphin"  [ implements "Animal";  typedDef "MakeSound" [] "void" (Code.Dots) |> makePublic ]
                       )) |> Unrepeated
      ]

    VerticalStack
      [
        ItemsBlockWithTitle "Consuming our ``animals'' issue with constructors"
          [
            ! @"Consider, a scenario where animals are selected and instantiate based on unique number read from the console"
            ! @"Unfortunately, such logic cannot be encapsulated inside the constructors of our animals, since once we enter a constructor we have to returns its instance"
            ! @"Imagine we are inside the constructor of \texttt{Dog} and the read number is 3, the system will anyways return an instance of \texttt{Dog} instead of \texttt{Dolphin}"
            ! @"So we need the caller to encapsulate such mechanism"
          ]
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Consuming our ``animals'' from the caller"
          [
            ! @"Consider, a client program that reads a series of integers from the console and uses such integers to create instances of animal"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (typedDeclAndInit "animals" "LinkedList<Animal>" (Code.New("LinkedList<Animal>", [])) >>
                         typedDeclAndInit "input" "int" (constInt -1) >>
                         //Code.MethodCall("animals","Add", [newC "LegacyLine" []]) >>
                         Code.While(Code.Op(var "input", Operator.NotEquals, constInt 0),
                                    ("input" := StaticMethodCall("Int32", "Parse", [(StaticMethodCall("Console", "ReadLine", []))])) >>
                                    (Code.IfThen(Code.Op(var "input", Operator.NotEquals, constInt 1),
                                                  MethodCall("animals", "Add", [newC "Cat" []]))) >>
                                    (Code.IfThen(Code.Op(var "input", Operator.NotEquals, constInt 2),
                                                  MethodCall("animals", "Add", [newC "Dog" []]))) >>
                                    (Code.IfThen(Code.Op(var "input", Operator.NotEquals, constInt 3),
                                                  MethodCall("animals", "Add", [newC "Animal" []])))))) |> Unrepeated
      ]
    ItemsBlock
      [
        ! @"What about all other programs that potentially might use our animals. Are they all aware of such classification (number to animal)?"
        ! @"Are all programs supposed to repeat such mechanism every time?"
        ! @"What if the map changes and now 2 maps to \texttt{Dolphin} ad 3 to \texttt{Dog}. How do we notify all clients of such change?"
        ! @"Evidently such solution does not take into consideration maintainability and is not flexible for changes"
      ]
    SubSection ("Achieving flexibility and maintainability")
    ItemsBlock
      [
        ! @"A solution would be to add to the Animal a method/function that implements the above logic and returns an instance of concrete animal (we write it once and use it everywhere)"
        ! @"To avoid clients to use different, potentially wrong, entry points for our animal we define all constructors private"
      ]
    SubSection "Another example"
    ItemsBlock
      [
    
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
    ]
    SubSection "Consideration"
    ItemsBlock
      [
        ! @"The two sample are from the high-level point of view the same,only the first one uses input to select the concrete class and the second one inheritance (through dispatching)"
        ! @"However in both cases we managed to decouple the instantiation mechanism of general polymorphic types from the derived concrete classes"
        ! @"The just described mechanism is commonly referred as factory design pattern"
      ]

    Section "The factory design pattern"
    SubSection "Formalization"
    ItemsBlock
      [
        ! @"Formalization"
      ]
    SubSection "Conclusions"
    ItemsBlock
      [
        ! @"Conclusions"
      ]
  ]