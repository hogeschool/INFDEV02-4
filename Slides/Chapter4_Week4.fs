module Chapter4.Week4.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime


let slides (title : string) = 
  [
    Section(sprintf "%s" title)
    Section("Introduction")
    SubSection("Lecture topics")
    ItemsBlock
      [
        ! @"The necessity for constructors at interface level"
        ! @"The factory design pattern"
        ! @"Abstract factory"
        ! @"Conclusions"
      ]
    Section("The factory design pattern")
    SubSection("Introduction")
    ItemsBlock
      [
        ! @"Sometimes, we know which interface to instantiate, but not its concrete class"
        ! @"Interfaces specify no constructors, external code is necessary to express such mechanism"
        ! @"This leads to conditionals in client code to determine which concrete class to instantiate"
        ItemsBlock
          [
            ! @"\texttt{switch (classToInstantiate) { ... }}"
            ! @"Hard to read"
            ! @"Repeated\footnote{Error prone, hard to modify and maintain.} wherever instantiation happens"
          ]
      ]

    ItemsBlock
      [
        ! @"In particular, we will study the \textbf{factory design pattern} (a creational pattern)"
        ! @"This moves the construction logic to a new class, thereby simulating virtual constructors"
        ! @"This design pattern is going to be the topic of this lecture"
      ]

    SubSection "Motivations"
    ItemsBlock
      [
        // not very clear
        ! @"We wish to standardize our application across different domains, so to provide a unique way for instantiating entities"
      ]
    SubSection "Our first example"
    VerticalStack
      [
        ItemsBlockWithTitle "Our first example"
          [
            ! @"Consider the following implementations of \texttt{Animal}"            
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
        ItemsBlockWithTitle "Consuming our ``animals'': issue with constructors"
          [
            ! @"We read the id of an animal from the console, and then want to instantiate it"
            ! @"Such logic cannot be expressed inside the \texttt{Animal} interface"
            ! @"Therefore, we need the client code to explicitly implement the selection mechanism"
          ]
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Consuming our ``animals'': from the client"
          [
            ! @"Our client now reads the input and uses it to instantiate a concrete animal"
            ! @"Note the collection contains only \texttt{Animal}s"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (typedDeclAndInit "animals" "LinkedList<Animal>" (Code.New("LinkedList<Animal>", [])) >>
                         typedDeclAndInit "input" "int" (constInt -1) >>
                         Code.While(Code.Op(var "input", Operator.NotEquals, constInt 0),
                                    ("input" := StaticMethodCall("Int32", "Parse", [(StaticMethodCall("Console", "ReadLine", []))])) >>
                                    (Code.IfThen(Code.Op(var "input", Operator.NotEquals, constInt 1),
                                                  MethodCall("animals", "Add", [newC "Cat" []]))) >>
                                    (Code.IfThen(Code.Op(var "input", Operator.NotEquals, constInt 2),
                                                  MethodCall("animals", "Add", [newC "Dog" []]))) >>
                                    (Code.IfThen(Code.Op(var "input", Operator.NotEquals, constInt 3),
                                                  MethodCall("animals", "Add", [newC "Bird" []]))))))
      ]
    SubSection "Consuming our ``animals'': from different clients"
    ItemsBlock
      [
        ! @"What about all other clients interested with consuming our animals?"
        ! @"Repeating code is: error prone and not maintainable"
        ! @"What about adding new animals? Does it still work? How do we notify the other clients about such change?"
        ! @"The manual solution just seen is neither maintainable, nor flexible"
      ]
    SubSection ("Defining instantiation logic once")
    ItemsBlock
      [
        ! @"We wish to isolate instantiation logic so that it becomes reusable" 
        ! @"It would be ideal to add such logic in the only point that is common to all our concrete animals: the interface" 
        ! @"Unfortunately, interfaces do not allow constructors\footnote{And it actually makes sense!}"
      ]
    SubSection ("Defining instantiation logic once")
    ItemsBlock 
      [
        ! @"We can use special-purpose classes to express such instantiation mechanism"
        ! @"How? "
        Pause
        ! @"By defining special methods that create and return concrete classes belonging to some polymorphic type"
      ]
    
    SubSection ("Upgrading our ``Animal'' to abstract class")
    ItemsBlock
      [
        ! @"From interface to class"
        ! @"In particular we define our new \texttt{Animal} abstract"
        ! @"Abstract classes allow the declaration of classes that can contain both empty and not empty methods"
        ! @"Indeed we can literally copy the signature of \texttt{MakeSound} in it"
      ]
    SubSection ("Adding the instantiation logic to our new Animal")
    ItemsBlock
      [
        ! @"Abstract classes provide an almost complete blueprint that cannot be instantiated directly"
        ! @"Abstract classes can be instantiated only though its derived types"


        ! @"For this reason we can only capture our instantiation logic through a new function \texttt{SelectNewAnimal} that returns a polymorphic type \texttt{Animal} and implements the above switch logic"
        ! @"Note the function is static, since we cannot instantiate directly an object of type \textttt{Animal}"
      ]
    ItemsBlock
      [
        ! @"CODE"
      ]    
    SubSection "Consideration"
    ItemsBlock
      [
        ! @"In the last version of our \texttt{Animal} class we managed to decouple the instantiation logic of general polymorphic types from the derived concrete classes"
        ! @"The just described mechanism is commonly referred as factory design pattern"
      ]

    Section "The factory design pattern"
    SubSection "Formalization"

    Section "The abstract factory"
    SubSection "Static methods are not enough"    
    SubSection "We wish to have different classes defining instantiation mechanism (each for a different context) for the same polymorphic type"
    SubSection "Formalization"

    SubSection "Conclusions"
    ItemsBlock
      [
        ! @"Conclusions"
      ]
  ]


  (*
  
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
  *)