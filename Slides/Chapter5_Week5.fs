module Chapter5.Week5.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides (title : string) = 
  [
    Section(sprintf "%s" title)
    Section("Introduction")
    ItemsBlock
      [
       ! @"Today we are going to study the a behavioral pattern: the decorator design pattern"
       ! @"Sometimes, we need to modify behaviors of an instance dynamically"
       ! @"Sub-classing could be a solution, but excessive sub-classing is a pitfall"
       ! @"The decorator pattern (also known as wrapper) solves this issues"
       ! @"How? By limiting sub-typing through effective aggregation"
      ]
    Section "Case study"
    SubSection "Case study"
    VerticalStack
      [
        ItemsBlockWithTitle "Case study" 
          [
           ! @"Consider again the iterator interface"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                (GenericInterfaceDef (["T"], "Iterator", [typedSig "GetNext" [] "IOption<T>"])) 
                  >> endProgram)  |> Unrepeated
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Case study" 
          [
            ! @"Consider again the natural numbers implementation"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (classDef "NaturalList" 
                          [
                            implements "Iterator<int>"
                            typedDecl "current" "int" |> makePrivate
                            typedDef "NaturalList" [] "" (("current" := ConstInt(1)) >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<int>" (("current" := Code.Op(var "current", Operator.Plus, (ConstInt(1)))) >> 
                                                                  (Code.New("Some<int>",[var "current"]) |> ret))
                          ])) |> Unrepeated 
      ]
    SubSection "First task: selecting only natural even numbers"
    VerticalStack
      [
        ItemsBlockWithTitle "First task: selecting only natural even numbers"
          [
            ! @"We wish now to iterate only the even numbers of our natural number list"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (classDef "NaturalEvenList" 
                          [
                            implements "Iterator<int>"
                            typedDecl "current" "int" |> makePrivate
                            typedDef "NaturalEvenList" [] "" (("current" := ConstInt(-1)) >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<int>" (("current" := Code.Op(var "current", Operator.Plus, (ConstInt(1)))) >> 
                                                                  Code.If(Code.Op((Code.Op(var "current", Operator.Percent, ConstInt(2))), Operator.Equals, ConstInt(0)),
                                                                          Code.New("Some<int>",[var "current"]) |> ret,
                                                                          MethodCall("this", "GetNext", []) |> ret))
                          ])) |> Unrepeated 
      ]
    SubSection "Another task: iteration with offset"
    VerticalStack
      [
        ItemsBlockWithTitle "Another task: iteration with offset"
          [
            ! @"We wish now to iterate our natural number list and while iterating it add an offset to each element"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (classDef "NaturalWithOffsetList" 
                          [
                            implements "Iterator<int>"
                            typedDecl "current" "int" |> makePrivate
                            typedDecl "offset" "int" |> makePrivate
                            typedDef "NaturalWithOffsetList" ["int", "offset"] "" (("current" := ConstInt(-1)) >> ("this.offset" := var "offset") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<int>" (("current" := Code.Op(var "current", Operator.Plus, (ConstInt(1)))) >> 
                                                                  (Code.New("Some<int>",[var "current" .+ var "offset"]) |> ret))
                          ])) |> Unrepeated 
      ]
    SubSection "UML"
    ItemsBlock
      [ ! @"Lets give a look to the UML of our classes made so far.." ]
    UML
        [ 
            Interface("Iterator<T>",5.0,0.0,0.0,[Operation("GetNext", [], Some "IOption<T>")])
            Class("NaturalList<T>", -6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("NaturalEvenList<T>", 0.0, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("NaturalWithOffsetList<T>", 6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
        ]
    SubSection "A new task: iteration over evens with offset"
    VerticalStack
      [
        ItemsBlockWithTitle "A new task: iteration over evens with offset"
          [
            ! @"We wish now to iterate only even numbers of our natural number list and for each number add an offset"
            ! @"Yes, we need another class..."
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (classDef "NaturalEvenWithOffsetList" 
                          [
                            implements "Iterator<int>"
                            typedDecl "current" "int" |> makePrivate
                            typedDecl "offset" "int" |> makePrivate
                            typedDef "NaturalEvenWithOffsetList" ["Offset", "offset"] "" (("current" := ConstInt(-1)) >> ("this.offset" := var "offset") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<int>" (("current" := Code.Op(var "current", Operator.Plus, (ConstInt(1)))) >> 
                                                                  Code.If(Code.Op((Code.Op(var "current", Operator.Percent, ConstInt(2))), Operator.Equals, ConstInt(0)),
                                                                          Code.New("Some<int>",[var "current" .+ var "offset"]) |> ret,
                                                                          MethodCall("this", "GetNext", []) |> ret))
                          ])) |> Unrepeated 
      ]
    SubSection "UML discussion"
    ItemsBlock
      [ ! @"As we can see our sub-classes are growing ``horizontally'', since no reuse is used" 
        ! @"Lets give a the UML again" ]
    UML
        [ 
            Interface("Iterator<T>",5.0,0.0,0.0,[Operation("GetNext", [], Some "IOption<T>")])
            Class("NaturalList<T>", -6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("NaturalEvenList<T>", 0.0, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("NaturalWithOffsetList<T>", 6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("NaturalEvenWithOffsetList<T>", 3.25, -6.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
        ]
    SubSection "Iterating a range between two integers"
    ItemsBlock
      [       
       ! @"Imagine if we now wish to implement a new data structure RangeBetween that takes two integers A and B (where A <= B)"
       ! @"We want \texttt{Range} to as well to support the offset and even behavior"
       ! @"It is a very time consuming task, as we have to literally duplicate everything and to implement all possible combinations"
      ]
    SubSection "Considerations"
    ItemsBlock
      [
       ! @"Sub-typing solves our problem, but adds another one. Too many repetitions"       
       ! @"Every change/add requires lots of work"
       ! @"Behaviors commonalities are not taken into consideration"
      ]
    VerticalStack
      [
      ItemsBlockWithTitle "Considerations"
        [
         ! @"How can we group such behaviors (offset and even) so define them once and use them everywhere?"
         ! @"A possible solution would see our natural number implementing offset and even"
        ]
      CSharpCodeBlock( TextSize.Tiny,
                          (classDef "NaturalEvenWithOffsetList" 
                            [
                              implements "NaturalList"
                              implements "Offset"
                              implements "Evens"
                              dots
                            ])) |> Unrepeated 
      ]
    ItemsBlock
      [
       ! @"This solution is not good, since the responsibilities are now not clear(see SOLID)"
       ! @"Moreover, the resulting structure will become a big, bulky class (a class whose functionality does not adapt to each instance)"
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Considerations"
          [
           ! @"Abstract classes with a series of booleans, which we can use as ``switchers'' to select the appropriate algorithm, could be another solution"       
           ! @"But fields do not force appropriate behavior for each of the roles"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                              (classDef "NaturalList" 
                                [
                                  implements "Iterator<int>"
                                  typedDecl "isEven" "bool" |> makePrivate
                                  typedDecl "isOffset" "bool" |> makePrivate
                                  dots
                                ])) |> Unrepeated 
      ]
    ItemsBlock
      [
        ! @"We need a better mechanism. Ideally we wish:"       
        ItemsBlock
          [
           ! @"To define once our natural list"
           ! @"To define once our the even behavior"
           ! @"To define once our the offset behavior"
           ! @"To apply the above behaviors ``on demand'', and not to all instances of natural lists"
           ! @"To combine the above behaviors without defining new behaviors"
          ]
      ]
    SubSection "Idea"
    VerticalStack
      [
        ItemsBlockWithTitle "Idea"
          [
            ! @"A possible could be that we define an intermediate entity \texttt{Decorator}, which inherits our iterator and also contains an instance of it (\texttt{decorated\_item})"
            ! @"Note \texttt{GetNext} acts as a proxy by simply calling \texttt{decorated\_item.GetNext()} and returning its result"
            ! @"\texttt{Decorator} is abstract, so you cannot create it without a ``concrete'' behavior"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (abstractClassDef "Decorator" 
                          [
                            implements "Iterator<int>"
                            typedDecl "decorated_item" "Iterator<int>" |> makePrivate
                            typedDef "Decorator" ["Iterator<int>", "decorated_item"] "" (("this.decorated_item" := var "decorated_item") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<int>" (MethodCall("decorated_item", "GetNext" ,[]) |> ret) |> makePublic
                          ])) |> Unrepeated 
      ]

    ItemsBlock
      [
        ! @"We can think of the decorator as an iterator containing elements, but it does not know how to iterate such elements (indeed we could make the above \texttt{GetNext} also abstract)"
        ! @"A decorator needs someone who teaches him how to iterate"
        ! @"\texttt{Decorator} is only aware that the instances to iterate are those referenced by \texttt{decorated\_item}"
      ]

    ItemsBlock
      [
       ! @"We now declare two entities \texttt{Even} and \texttt{Offset} that extend our \texttt{Decorator}"
       ! @"\texttt{Even} and \texttt{Offset} are unaware (and they do not need to) of whether they are going to deal with all natural numbers or just a range of them"
      ] 
    VerticalStack
      [
        ItemsBlockWithTitle "Idea: even class"
          [
           ! @"In the following you find the code for \texttt{Even}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                            (classDef "Even" 
                              [
                                implements "Decorator"
                                typedDefWithBase "Even" ["Iterator<int>", "collection"] "" ["collection"] endProgram |> makePublic
                                typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base", "GetNext", []))) >>
                                                                      (Code.MethodCall("current", "Visit", [Code.GenericLambdaFuncDecl([], ret (newC "None<int>" []))
                                                                                                            Code.GenericLambdaFuncDecl(["current"], 
                                                                                                                                         Code.If(Code.Op((Code.Op(var "current", Operator.Percent, ConstInt(2))), Operator.Equals, ConstInt(0)),
                                                                                                                                                          Code.New("Some<int>",[var "current"]) |> ret,
                                                                                                                                                          MethodCall("this", "GetNext", []) |> ret))]))) |> makeOverride |> makePublic
                              ])) |> Unrepeated          
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Idea: offset class"
          [
           ! @"In the following you find the code for \texttt{Even}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                            (classDef "Offset" 
                              [
                                implements "Decorator"
                                typedDecl "offset" "int" |> makePrivate
                                typedDef "Offset" ["Offset", "offset"] "" (("this.offset" := var "offset") >> endProgram) |> makePublic
                                typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base", "GetNext", []))) >>
                                                                      (Code.MethodCall("current", "Visit", [Code.GenericLambdaFuncDecl([], ret (newC "None<int>" []))
                                                                                                            Code.GenericLambdaFuncDecl(["current"], Code.New("Some<int>",[var "current" .+ var "offset"]) |> ret)]))) |> makeOverride |> makePublic
                              ])) |> Unrepeated          
      ]
    ItemsBlock
      [
       ! @"With \texttt{Even} and \{Offset} we managed to capture a reusable behavior that works with any collection made of numbers"
       ! @"Indeed, they are concrete decorators. Their code is always run after a \texttt{GetNext} over a collection of numbers"
       ! @"Does this happen to all collections of numbers? No, only on those who are requesting this behavior"
      ] 
    ItemsBlock
      [
       ! @"The following are all examples of how to use our new data structures:"
       ! @"\texttt{Iterator<int> ns1 = new Even(new NaturalList())}"
       ! @"\texttt{Iterator<int> ns2 = new Offset(new NaturalList())}"
       ! @"\texttt{Iterator<int> ns2 = new Offset(new Even(new NaturalList()))}"
       ! @"\texttt{Iterator<int> ns3 = new Offset(new Even(new Range(5, 10)))}"
       ! @"Note how decomposability helps to keep the interaction surface clean and reusable and the implementation compact"
       ! @"We now show the UML of our code"
      ] 

    ItemsBlock
      [
       ! @"UML"
      ]

    SubSection "Considerations"
    ItemsBlock
      [
       ! @"Of course we still have sub-typing, but it is limited as our decorator help us to take out concerns (our even and offset) that are not part of the natural list description, so concerns can be reused/adapted/combined/etc."
      ]
    
    SubSection "Considerations"
    ItemsBlock
      [
        ! @"The pattern seen so far follows a specific design pattern that is called the \textbf{Decorator design pattern} (a behavioral design pattern)"
        ! @"We now study show its formalization and add some final considerations"
      ]
    Section("The decorator design pattern")
    SubSection "Formalism"
    ItemsBlock [ ! @"Formalism" ]
    ItemsBlock [ ! @"UML" ]

    SubSection "Improving our initial solution"
    ItemsBlock
      [
       ! @"We can further improve the above solution. How? By making offset and even ``general'' as offset simply transforms and even simply filters"
       ! @"Indeed, offset is a concrete \textbf{Map}"
       ! @"CODE"
      ]
    ItemsBlock
      [
       ! @"Indeed, even is a concrete \textbf{Filter}"
       ! @"CODE"
      ]

    Section "Conclusions"     
  ]