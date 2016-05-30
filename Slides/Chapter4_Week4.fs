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
        ! @"POlymorphic constructors"
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
            ! @"The collection contains only \texttt{Animal}s"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (typedDeclAndInit "animals" "List<Animal>" (Code.New("List<Animal>", [])) >>
                         typedDeclAndInit "id" "int" (constInt -1) >>
                         Code.While(Code.Op(var "id", Operator.NotEquals, constInt 0),
                                    ("id" := StaticMethodCall("Int32", "Parse", [(StaticMethodCall("Console", "ReadLine", []))])) >>
                                    (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 1),
                                                  MethodCall("animals", "Add", [newC "Cat" []]))) >>
                                    (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 2),
                                                  MethodCall("animals", "Add", [newC "Dog" []]))) >>
                                    (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 3),
                                                  MethodCall("animals", "Add", [newC "Bird" []])))))) |> Unrepeated
      ]
    SubSection "Consuming our ``animals'': from different clients"
    ItemsBlock
      [
        ! @"What about all other clients interested with consuming our animals?"
        ! @"Repeating code is error prone and not maintainable"
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
    

    SubSection ("Making our ``Animal'' abstract")
    ItemsBlock
      [
        ! @"We can of course define our \texttt{Animal} abstract"
        ! @"What follows?"
        Pause
        ! @"We can have a static method \texttt{Instantiate} that implements the instantiation mechanism, introduced at the beginning of this example, and returns a concrete animal"
        ! @"\texttt{Instantiate} is static, since we cannot call it directly (\texttt{Animal} is abstract)"
        ! @"We can leave \texttt{MakeSound} as a signature"  
        ! @"In the following we show our abstract \texttt{Animal} and we consume it"        
      ]

    CSharpCodeBlock(TextSize.Tiny, 
                        (abstractClassDef "Animal" 
                                          [typedDef "Instantiate" ["int", "id"] "Animal" 
                                                     
                                                             ((Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 1),
                                                                            newC "Cat" [] |> ret)) >>
                                                              (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 2),
                                                                            newC "Dog" [] |> ret)) >>
                                                              (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 3),
                                                                            newC "Bird" [] |> ret))) |> makeStatic |> makePublic
                                           TypedSigAbstract("MakeSound", [], "void") |> makePublic] >>
                          dots >>
                          (typedDeclAndInit "an_animal" "Animal" (staticMethodCall "Animal" "Instantiate" [StaticMethodCall("Int32", "Parse", [(StaticMethodCall("Console", "ReadLine", []))]) ])) >>
                          (methodCall "an_animal" "MakeSound" [])))    
    SubSection "Consideration"
    ItemsBlock
      [
        ! @"In the last version of our \texttt{Animal} class we managed to define instantiation logic at polymorphic level, instead of carrying such task on all clients"
        ! @"Now there is only one \textit{entry point} where we can create our concrete animals: \texttt{Animal}!"
      ]
    ItemsBlock
      [
        ! @"Whenever a client wishes to instantiate an animal it has to ask \texttt{Animal}"
        ! @"We now can say that \texttt{Animal} is not only the polymorphic type for our concrete animals, but also a \textbf{factory} of animals"
        ! @"This instantiation mechanism belongs to the so called \textit{simple factory method} design pattern"
      ]
    ItemsBlock
      [
        ! @"In the following we will study the formalization of such pattern together with other patterns belonging to this family"
        ! @"Patterns for providing instantiation mechanisms are generally referred to as: \textbf{factory design patterns}"
      ]

    Section "The simple factory design pattern"
    SubSection "Formalization"    
    ItemsBlock
      [
        ! @"The solution provided for our \texttt{Animal} scenario belongs to this pattern"
        ! @"A simple factory is a method that is called directly from the client"
        ! @"Such method returns one of many different polymorphic classes"
        ! @"Such method can be declared in the parent class (as \texttt{static}) or in a separate class"
        ! @"In the following a UML of such pattern and an example are provided"        
      ]

    UML
        [ 
          Class("Client", -6.5, 3.0, Option.None, [], [])
          Package("FactoryOfI",
            [
              Class("ParentClass", 1.0, 0.0, Option.None, [], [UMLItem.Operation("static$\,$Create",["args"],Some "ParentClass")])
              Class("ChildClass1", -5.5, -3.0, Some "ParentClass", [], [])
              Class("ChildClass2", 0.0, -3.0, Some "ParentClass", [], [])
              Class("ChildClassN", 5.5, -3.0, Some "ParentClass", [], [])
              Aggregation("Client","calls",Option.None,"ParentClass")
            ])
        ]
    CSharpCodeBlock(TextSize.Tiny, 
                      (abstractClassDef "Vehicle" 
                                        [typedDef "Create" ["int", "id"] "Vehicle" 
                                                            ((Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 1),
                                                                          newC "Ferrari" [] |> ret)) >>
                                                             (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 2),
                                                                          newC "Lamborghini" [] |> ret))) |> makeStatic |> makePublic
                                         TypedSigAbstract("StartEngine", [], "void") |> makePublic] >>
                        dots >>
                        (typedDeclAndInit "a_vehicle" "Vehicle" (staticMethodCall "Vehicle" "Create" [(StaticMethodCall("Int32", "Parse", [(StaticMethodCall("Console", "ReadLine", []))]))])) >>
                        (methodCall "a_vehicle" "StartEngine" [])))
    CSharpCodeBlock(TextSize.Tiny, 
                      (classDef "Ferrari" 
                                [extends "Vehicle"
                                 typedDefWithBase "Ferrari" [] "" [] endProgram |> makePublic
                                 typedDef "StartEngine" [] "void" (dots)|> makeOverride |> makePublic] >>
                       classDef "Lamborghini" 
                                [extends "Vehicle"
                                 typedDefWithBase "Lamborghini" [] "" [] endProgram |> makePublic
                                 typedDef "StartEngine" [] "void" (dots) |> makeOverride |> makePublic])) |> Unrepeated
    ItemsBlock
      [
        ! @"The \texttt{Create} method can be declared as part of a distinct factory class"
        ! @"In this case an instance of such factory is necessary to call the method, unless the method is \texttt{static}"
        ! @"In the following, UML of this pattern and an example are provided"
      ]
    UML
        [ 
          Class("Client", -7.5, 3.0, Option.None, [], [])
          Package("FactoryOfI",
            [
              Class("SimpleFactory", 0.0, 0.0, Option.None, [], [UMLItem.Operation("Create",["args"],Some "ParentClass")])
              Class("ParentClass", 7.5, -2.0, Option.None, [], [])
              Class("ChildClass1", -5.5, -5.0, Some "ParentClass", [], [])
              Class("ChildClass2", 0.0, -5.0, Some "ParentClass", [], [])
              Class("ChildClassN", 5.5, -5.0, Some "ParentClass", [], [])
              Aggregation("Client","calls",Option.None,"SimpleFactory")
              Aggregation("SimpleFactory","returns",Option.None,"ParentClass")
            ])
        ]
    CSharpCodeBlock(TextSize.Tiny, 
                      (interfaceDef "Vehicle"
                        [
                         typedSig "StartEngine" [] "void"
                        ] >>
                       classDef "VehicleFactory" 
                                        [typedDef "Create" ["int", "id"] "Vehicle" 
                                                            ((Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 1),
                                                                          newC "Ferrari" [] |> ret)) >>
                                                             (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 2),
                                                                          newC "Lamborghini" [] |> ret))) |> makePublic] >>
                        dots >>
                        (typedDeclAndInit "a_vehicle" "Vehicle" (staticMethodCall "VehicleFactory" "Create" [(StaticMethodCall("Int32", "Parse", [(StaticMethodCall("Console", "ReadLine", []))]))])) >>
                        (methodCall "a_vehicle" "StartEngine" [])))
    CSharpCodeBlock(TextSize.Tiny, 
                      (classDef "Ferrari" 
                                [extends "Vehicle"
                                 typedDef "Ferrari" [] "" endProgram |> makePublic
                                 typedDef "StartEngine" [] "void" (dots)|> makeOverride |> makePublic] >>
                       classDef "Lamborghini" 
                                [extends "Vehicle"
                                 typedDef "Lamborghini" [] "" endProgram |> makePublic
                                 typedDef "StartEngine" [] "void" (dots) |> makeOverride |> makePublic]))
    SubSection "Static methods are not enough"    
    ItemsBlock
      [
        ! @"This method is not flexible"
        ! @"We cannot redefine (part of) our factories"
        ! @"We cannot to make use of polymorphism here as well"
      ]
    ItemsBlock
      [
        ! @"A solution would be that our simple factory method becomes virtual"
        ! @"Depending on the domain, a ``concrete factory'' is then selected by the client that implements such virtual methods"
        ! @"This mechanism of \textit{interchangeable} factories is called the \textbf{factory method}"
      ]    
    Section "The factory method"
    SubSection "Formalization"    
    ItemsBlock
      [
        ! @"A factory method is: ``a class which defers instantiation of an object to subclasses''\footnote{GOF}"
        ! @"How do we achieve this? By means of polymorphism"
        ! @"We make our factory polymorphic, so the instantiation becomes polymorphic, as well"
      ]
    ItemsBlock
      [
        ! @"Given a polymorphic type $I$ (to instantiate)"
        ! @"Given a series of concrete implementations of $I$: $C_1,\dots,C_n$"
        ! @"Factory implementation:"
        ItemsBlock
          [
            ! @"Given a polymorphic factory $F_I$ that creates an $I$"
            ! @"Given a series of concrete implementations of $F_I$: $f_1,\dots,f_m$"
          ]
      ]
    //ItemsBlock
     // [
      //  ! @"Our client in order to get concrete \texttt{Parent} it needs to select a concrete factory that implement our \texttt{AbstractFactory}"
       // ! @"Such concrete factory will be a special class that implements, according to some criteria, the virtual \texttt{Create}"
     // ]
    ItemsBlock
      [
        ! @"By deferring instantiation of an object to subclasses a new client that has different criteria for instantiating concrete $I$'s will provide a different concrete factory without changing the already existing relations"
        ! @"Exchanging concrete factories does not affect other classes, structures, or behaviors"        
        ! @"In the following UML of this pattern and an example are provided"        
      ]

    UML
        [ 
            Class("Client", -7.5, 0.0, Option.None, [], [])
            Class("F", -7.5, -2.0, Option.None, [], [UMLItem.Operation("Create",["args"],Some "I")])
            Class("f1", -7.5, -5.0, Some "F", [], [UMLItem.Operation("Create",["args"],Some "I")])
            Class("f2", -2.0, -5.0, Some "F", [], [UMLItem.Operation("Create",["args"],Some "I")])
            Class("fn", 3.5, -5.0, Some "F", [], [UMLItem.Operation("Create",["args"],Some "I")])

            Aggregation("Client","calls",Option.None,"F")

            Class("I", 5.5, -2.0, Option.None, [], [])
            Class("C1", -5.5, 3.0, Some "I", [], [])
            Class("C2", 0.0, 3.0, Some "I", [], [])
            Class("Cn", 5.5, 3.0, Some "I", [], [])
            Aggregation("F","returns",Option.None,"I")
        ]
    CSharpCodeBlock(TextSize.Tiny, 
                    (abstractClassDef "VehicleFactory" [TypedSigAbstract("Create", [("int", "id");("Color", "color")], "Vehicle") |> makePublic] >>
                     classDef "ConcreteVehicleFactory" 
                            [implements "VehicleFactory"
                             typedDef "Create" [("int", "id");("Color", "color")] "Vehicle" 
                                       ((Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 1),
                                                     newC "Ferrari" [var "color"] |> ret)) >>
                                        (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 2),
                                                     newC "Lamborghini" [var "color"] |> ret))) |> makePublic] >>
                     dots >>
                     (typedDeclAndInit "a_vehicle" "Vehicle" (staticMethodCall "ConcreteVehicleFactory" "Create" [StaticMethodCall("Int32", "Parse", [(StaticMethodCall("Console", "ReadLine", []))]); ConstString("Color.Red")])) >>
                     (methodCall "a_vehicle" "StartEngine" [])))

    SubSection "Fixed attribute and factories"
    ItemsBlock  
      [
        ! @"Factories can also specialize in ``cross-type'' concerns"
        ! @"For example, a fixed attribute"
      ]

    CSharpCodeBlock(TextSize.Tiny, 
                    (abstractClassDef "VehicleFactory" [TypedSigAbstract("Create", ["int", "id"], "Vehicle") |> makePublic] >>
                     classDef "RedVehicleFactory" 
                            [implements "VehicleFactory"
                             typedDef "Create" ["int", "id"] "Vehicle" 
                                       ((Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 1),
                                                     newC "Ferrari" [constString "Red"] |> ret)) >>
                                        (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 2),
                                                     newC "Lamborghini" [constString "Red"] |> ret))) |> makePublic] >>
                     classDef "YellowVehicleFactory" 
                            [implements "VehicleFactory"
                             typedDef "Create" ["int", "id"] "Vehicle" 
                                       ((Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 1),
                                                     newC "Ferrari" [constString "Yellow"] |> ret)) >>
                                        (Code.IfThen(Code.Op(var "id", Operator.Equals, constInt 2),
                                                     newC "Lamborghini" [constString "Yellow"] |> ret))) |> makePublic] >>

                     dots >>
                     (typedDeclAndInit "a_vehicle" "Vehicle" (staticMethodCall "YellowVehicleFactory" "Create" [StaticMethodCall("Int32", "Parse", [(StaticMethodCall("Console", "ReadLine", []))])])) >>
                     (methodCall "a_vehicle" "StartEngine" [])))
    CSharpCodeBlock(TextSize.Tiny, 
                      (interfaceDef "Vehicle"
                        [
                         typedSig "StartEngine" [] "void"
                        ] >>
                       classDef "Ferrari" 
                                [extends "Vehicle"
                                 typedDef "Ferrari" ["color","Color"] "" endProgram |> makePublic
                                 typedDef "StartEngine" [] "void" (dots)|> makeOverride |> makePublic] >>
                       classDef "Lamborghini" 
                                [extends "Vehicle"
                                 typedDef "Lamborghini" ["color","Color"] "" endProgram |> makePublic
                                 typedDef "StartEngine" [] "void" (dots) |> makeOverride |> makePublic]))
    
    Section "Conclusions"
    SubSection "Conclusions"
    ItemsBlock
      [
        ! @"Sometime we need interfaces to implement virtual constructors"
        ! @"Why? Because sometimes we know the polymorphic type to instantiate first and later the concrete one"
        ! @"A naive solution would see the client code implement such instantiation mechanism, but this yields repetition and makes code less maintainable"
        ! @"Factories solve this issue elegantly by promoting polymorphic constructors, by means of class polymorphism, or static methods"
      ]
    ItemsBlock
      [
        ! @"Static methods are less flexible when compared to polymorphic classes, since abstract classes allow both virtual and non virtual methods"
        ! @"Moreover, polymorphic classes allow the definition of multiple interchangeable concrete factories, each shaped for a specific domain"
      ]

    Section "Appendix"
    SubSection "The abstract factory method - formalization"
    ItemsBlock
      [
       ! @"The biggest pattern of the factories seen so far"
       ! @"Is acts the same as the factory method, except for the fact that it might contain more than one virtual instantiation method"
       ! @"Each of them returning a different but ``related'' polymorphic object"
       ! @"In the following a UML of such pattern and an example are provided"        
      ]
    UML
        [ 
            Class("Client", -7.5, 0.0, Option.None, [], [])
            Class("F", -7.5, -2.0, Option.None, [], [UMLItem.Operation("Create1",["args"],Some "I")
                                                     UMLItem.Operation("Create2",["args"],Some "J")])
            Class("f1", -7.5, -5.0, Some "F", [], [])
            Class("f2", -2.0, -5.0, Some "F", [], [])
            Class("fm", 3.5, -5.0, Some "F", [], [])

            Aggregation("Client","calls",Option.None,"F")

            Class("I", 5.5, 1.0, Option.None, [], [])
            Class("C11", 0.0, 3.0, Some "I", [], [])
            Class("C1K", 5.5, 3.0, Some "I", [], [])

            Class("J", 5.5, 0.0, Option.None, [], [])
            Class("C21", 0.0, -2.0, Some "I", [], [])
            Class("C2N", 5.5, -2.0, Some "I", [], [])

            Aggregation("F","returns",Option.None,"I")
            Aggregation("F","returns",Option.None,"J")
        ]
    CSharpCodeBlock(TextSize.Tiny, 
                    (abstractClassDef "VehicleComponentsFactory" [TypedSigAbstract("CreateTire", [], "Tire") |> makePublic
                                                                  TypedSigAbstract("CreateSeat", [], "Seat") |> makePublic
                                                                  dots] >>
                     classDef "FerraryComponents" 
                            [implements "VehicleComponentsFactory"
                             dots
                             typedDef "CreateSeat" [] "Seat" dots |> makePublic
                             typedDef "CreateTire" [] "Tire" dots |> makePublic] >>
                     classDef "LamborghiniComponents" 
                            [implements "VehicleComponentsFactory"
                             dots
                             typedDef "CreateSeat" [] "Seat" dots |> makePublic
                             typedDef "CreateTire" [] "Tire" dots |> makePublic] >>

                     dots))
    CSharpCodeBlock(TextSize.Tiny, 
                    (classDef "Garage" 
                            [
                             typedDecl "ferrariComponentsFactory" "VehicleComponentsFactory" |> makePublic
                             typedDecl "lamborghiniComponentsFactory" "VehicleComponentsFactory" |> makePublic
                             typedDef "Garage" [] "" (("this.ferrariComponentsFactory" := newC "FerraryComponents" []) >> ("this.lamborghiniComponentsFactory" := newC "LamborghiniComponents" []) >> endProgram) |> makePublic
                             typedDef "RepearTires" [("vehicle" , "Vehicle")] "void"
                                      (Code.MethodCall("vehicle", "Visit", [Code.GenericLambdaFuncDecl(["ferrari"], ("ferrari.Tire " := MethodCall("ferrariComponentsFactory", "CreateTire", [])) |> ret) 
                                                                            Code.GenericLambdaFuncDecl(["lamborghini"], ("ferrari.lamborghini " := MethodCall("lamborghiniComponentsFactory", "CreateTire", [])) |> ret)])) |> makePublic
                             ] >>

                     dots)) |> Unrepeated
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