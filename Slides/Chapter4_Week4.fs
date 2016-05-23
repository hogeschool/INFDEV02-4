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
    Section("The problem")
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
                                    (Code.IfThen(Code.Op(var "input", Operator.Equals, constInt 1),
                                                  MethodCall("animals", "Add", [newC "Cat" []]))) >>
                                    (Code.IfThen(Code.Op(var "input", Operator.Equals, constInt 2),
                                                  MethodCall("animals", "Add", [newC "Dog" []]))) >>
                                    (Code.IfThen(Code.Op(var "input", Operator.Equals, constInt 3),
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
        ! @"Such special-purpose classes are called abstract classes"
      ]
    SubSection ("About abstract classes")
    VerticalStack
      [
        ItemsBlockWithTitle ("About abstract classes") 
          [
            ! @"In OO programming it is possible to design special classes containing methods with or without bodies"
            ! @"These special classes are called \textit{abstract}"
            ! @"In the following an abstract class \texttt{Weapon} contains a concrete method \texttt{GetAmountOfBullets} and an abstract \texttt{Fire}"
            ! @"\texttt{Fire} is abstract, since different weapons might come with different kinds of firing"
          ]
        CSharpCodeBlock(TextSize.Tiny, 
                        (abstractClassDef "Weapon" 
                                          [typedDecl "amounOfBullets" "int"|> makePublic
                                           typedDef "Weapon" ["int","amounOfBullets"] "" ((("this.amounOfBullets" := var"amounOfBullets") >> endProgram)) |> makePublic
                                           typedDef "GetAmountOfBullets" [] "int" (var "this.amounOfBullets"  |> ret) |> makePublic
                                           TypedSigAbstract("Fire", [], "void") |> makePublic]))
      ]

    SubSection ("Instantiating abstract classes")
    ItemsBlock
      [
        ! @"Is not possible directly (what is the result of \texttt{new Weapon().Fire()}?)"
        ! @"Abstract classes have to be inherited in order to use their functionalities"
        ! @"All abstract methods must eventually come with an implementation"
      ]
    VerticalStack
      [ 
        ItemsBlockWithTitle "Implementing our weapon"
          [
            ! @"In the following a correct implementation of our \texttt{Weapon} is provided"
          ]
        CSharpCodeBlock(TextSize.Tiny, 
                          (classDef "Gun" 
                                    [extends "Weapon"
                                     typedDefWithBase "Gun" ["int","amounOfBullets"] "" ["amounOfBullets"] endProgram |> makePublic
                                     typedDef "Fire" [] "void" ("amounOfBullets" := Code.Op(var "amounOfBullets", Operator.Minus, ConstInt(-1)))|> makeOverride |> makePublic] >>
                           classDef "FastGun" 
                                    [extends "Weapon"
                                     typedDefWithBase "Gun" ["int","amounOfBullets"] "" ["amounOfBullets"] endProgram |> makePublic
                                     typedDef "Fire" [] "void" (("amounOfBullets" := Code.Op(var "amounOfBullets", Operator.Minus, ConstInt(-1))) >>
                                                                ("amounOfBullets" := Code.Op(var "amounOfBullets", Operator.Minus, ConstInt(-1)))) |> makeOverride |> makePublic]))
        
      ]

    SubSection ("Making our ``Animal'' abstract")
    ItemsBlock
      [
        ! @"We can of course define our \texttt{Animal} abstract"
        ! @"What follows?"
        Pause
        ! @"We can have a static method \texttt{SelectNewAnimal} that implements the instantiation mechanism, introduced at the beginning of this example, and returns a concrete animal"
        ! @"We can have leave \texttt{MakeSound} as a signature"  
        ! @"In the following we show our abstract \texttt{Animal} and we consume it"        
      ]

    CSharpCodeBlock(TextSize.Tiny, 
                        (abstractClassDef "Animal" 
                                          [typedDef "SelectNewAnimal" [] "Animal" 
                                                     
                                                             ((typedDeclAndInit "input" "int" (StaticMethodCall("Int32", "Parse", [(StaticMethodCall("Console", "ReadLine", []))]))) >>
                                                              (Code.IfThen(Code.Op(var "input", Operator.Equals, constInt 1),
                                                                            newC "Cat" [] |> ret)) >>
                                                              (Code.IfThen(Code.Op(var "input", Operator.Equals, constInt 2),
                                                                            newC "Dog" [] |> ret)) >>
                                                              (Code.IfThen(Code.Op(var "input", Operator.Equals, constInt 3),
                                                                            newC "Bird" [] |> ret)) >>
                                                              (methodCall "this" "SelectNewAnimal" [] |> ret)) |> makePublic
                                           TypedSigAbstract("MakeSound", [], "void") |> makePublic] >>
                          dots >>
                          (typedDeclAndInit "an_animal" "Animal" (staticMethodCall "Animal" "SelectNewAnimal" [])) >>
                          (methodCall "an_animal" "MakeSound" [])))    
    SubSection "Consideration"
    ItemsBlock
      [
        ! @"In the last version of our \texttt{Animal} class we managed to define instantiation logic at polymorphic level, instead of carrying such task on all clients"
        ! @"Now there is only one \textit{entry point} where we can create our concrete animals: in \texttt{Animal}!"
        ! @"Whenever a client wishes to instantiate an animal it has to ask the permission to \texttt{Animal}"
        ! @"We now can say that \texttt{Animal} is not only the polymorphic type for our concrete animals, but also a \textbf{factory} of animals"
        ! @"This instantiation mechanism, which is recurrent in many domains, is commonly referred as factory design pattern"
        ! @"More specifically, the just described mechanics is called \textit{simple factory method}"
      ]

    Section "The simple factory design pattern"
    SubSection "Formalization"    
    ItemsBlock
      [
        ! @"A simple factory is a method that called is directly from the client"
        ! @"Such method returns one of many different classes, all implementing a parent class"
        ! @"We could also include our simple factory method in this shared type, in this case it is reasonable to have such it \texttt{static}"
      ]
    UML
        [ 
          Package("Version 1",
            [
              Class("Client", -7.5, 0.0, Option.None, [], [])
              Class("SimpleFactory", 0.0, 0.0, Option.None, [], [UMLItem.Operation("Create",["args"],Some "ParentClass")])
              Class("ParentClass", 7.5, 0.0, Option.None, [], [])
              Class("ChildClass1", -5.5, -3.0, Some "ParentClass", [], [])
              Class("ChildClass2", 0.0, -3.0, Some "ParentClass", [], [])
              Class("ChildClassN", 5.5, -3.0, Some "ParentClass", [], [])
              Aggregation("Client","calls",Option.None,"SimpleFactory")
              Aggregation("SimpleFactory","returns",Option.None,"ParentClass")
            ])
        ]
    UML
        [ 
          Package("Version 2",
            [
              Class("Client", -6.5, 0.0, Option.None, [], [])
              Class("ParentClass", 1.0, 0.0, Option.None, [], [UMLItem.Operation("Create",["args"],Some "ParentClass")])
              Class("ChildClass1", -5.5, -3.0, Some "ParentClass", [], [])
              Class("ChildClass2", 0.0, -3.0, Some "ParentClass", [], [])
              Class("ChildClassN", 5.5, -3.0, Some "ParentClass", [], [])
              Aggregation("Client","calls",Option.None,"ParentClass")
            ])
        ]

    SubSection "Static methods are not enough"    
    ItemsBlock
      [
        ! @"However static methods, and in general simple factories, are not enough"
        ! @"What if we want to make the instantiation as well custom"
        ! @"Static methods cannot be overrided!"
      ]
    ItemsBlock
      [
        ! @"A solution would be that our simple factory method becomes virtual"
        ! @"Depending on the domain, a ``concrete factory'' is then selected by the client that implements such virtual methods"
        ! @"This mechanism of \textit{interchangeable} factories is called the \texttt{factory method}"
      ]    
    Section "The factory method"
    SubSection "Formalization"    
    ItemsBlock
      [
        ! @"A factory method is: ``a class which defers instantiation of an object to subclasses''"
        ! @"This is possible by means of abstract class"
        ! @"By becoming abstract our factory method become virtual, which means that a client who wants to consume it should first provide a concrete class implementing such abstract factory"
        ! @"More formally given an abstract factory class \texttt{A}, which contains a virtual method \texttt{Create}, and a series of classes \texttt{B1,..,Bn} all implementing a polymorphic type \texttt{P}"
      ]
    ItemsBlock
      [
        ! @"\texttt{Create} returns an object of type \texttt{P}"
        ! @"Our client in order to instantiate an concrete \texttt{P} needs a concrete class \texttt{C} implementing our factory \texttt{A}"
        ! @"\texttt{C} will be a special class that implements, according to some criteria, the virtual \texttt{Create}"
      ]
    ItemsBlock
      [
        ! @"By deferring instantiation of an object to subclasses a new client that has different criteria on mind for instantiating concrete \texttt{P}'s will provide a different concrete factory without changing the already existing relations"
        ! @"Exchanging concrete factories does not affect other classes structures or behaviors"        
      ]

    UML
        [ 
            Class("Client", -7.5, 0.0, Option.None, [], [])
            Class("Factory", -7.5, -2.0, Option.None, [], [UMLItem.Operation("Create",["args"],Some "ParentClass")])
            Class("Concrete1", -7.5, -5.0, Some "Factory", [], [])
            Class("Concrete2", -2.0, -5.0, Some "Factory", [], [])
            Class("ConcreteM", 3.5, -5.0, Some "Factory", [], [])

            Aggregation("Client","calls",Option.None,"Factory")

            Class("ParentClass", 5.5, -2.0, Option.None, [], [])
            Class("ChildClass1", -5.5, 3.0, Some "ParentClass", [], [])
            Class("ChildClass2", 0.0, 3.0, Some "ParentClass", [], [])
            Class("ChildClassN", 5.5, 3.0, Some "ParentClass", [], [])
            Aggregation("Factory","returns",Option.None,"ParentClass")
        ]

    Section "The abstract factory method"
    SubSection "Formalization"    
    ItemsBlock
      [
       ! @"The biggest pattern of the factories seen so far"
       ! @"Is acts the same as the factory method, except for the fact that it might contain more than one virtual instantiation method"
       ! @"Each of them returning a different but related polymorphic object"
      ]
    UML
        [ 
            Class("Client", -7.5, 0.0, Option.None, [], [])
            Class("Factory", -7.5, -2.0, Option.None, [], [UMLItem.Operation("Create1",["args"],Some "ParentClass1")
                                                           UMLItem.Operation("Create2",["args"],Some "ParentClass2")])
            Class("Concrete1", -7.5, -5.0, Some "Factory", [], [])
            Class("Concrete2", -2.0, -5.0, Some "Factory", [], [])
            Class("ConcreteM", 3.5, -5.0, Some "Factory", [], [])

            Aggregation("Client","calls",Option.None,"Factory")

            Class("ParentClass1", 5.5, 1.0, Option.None, [], [])
            Class("ChildClass11", 0.0, 3.0, Some "ParentClass1", [], [])
            Class("ChildClass1N", 5.5, 3.0, Some "ParentClass1", [], [])

            Class("ParentClass2", 5.5, 0.0, Option.None, [], [])
            Class("ChildClass21", 0.0, -2.0, Some "ParentClass2", [], [])
            Class("ChildClass2N", 5.5, -2.0, Some "ParentClass2", [], [])

            Aggregation("Factory","returns",Option.None,"ParentClass1")
            Aggregation("Factory","returns",Option.None,"ParentClass2")
        ]
    SubSection "Conclusions"
    ItemsBlock
      [
        ! @"Sometime we need interfaces to implement virtual constructor"
        ! @"Why? Because sometimes we know the polymorphic type to instantiate first and later the concrete one"
        ! @"A naive solution would see the client code implement such instantiation mechanism, but this is will yield to repetition and would make the code not maintainable"
        ! @"Factories solve such issue elegantly by promoting virtual constructors, by means of abstract classes, or via static methods"
      ]
    ItemsBlock
      [
        ! @"Static methods are less flexible when compared to abstract classes, since abstract classes allow both virtual and not virtual methods"
        ! @"Moreover, abstract classes allow the definition of multiple interchangeable concrete factories, each shaped for a specific domain"
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