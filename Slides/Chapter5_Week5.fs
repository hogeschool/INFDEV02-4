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
    SubSection("Lecture topics")
    ItemsBlock
      [
        ! @"Adding responsibilities dynamically"
        ! @"Possible solutions and pitfalls"
        ! @"The decorator design pattern"
        ! @"Conclusions"
      ]
    SubSection "Introduction"
    ItemsBlock
      [
       ! @"Today we are going to study a behavioral pattern: the decorator design pattern"
       ! @"Sometimes, we need to modify behaviors (or to add responsibilities) of an object dynamically"

      ]
    ItemsBlock
      [
        ! @"Hand made combinations could be a solution, but excessive inheritance is a pitfall, since the amount of combinations could be huge"
        ! @"Examples:"
        ! @"Add a turbo to a \texttt{Car}"
        ! @"Add a truck to a \texttt{Car}"
        ! @"Add an extra seat to a \texttt{Car}"
        ! @"Add a turbo and an extra seat to a \texttt{Car}"
        ! @"etc."
      ]

    ItemsBlock
      [
       ! @"The decorator pattern (also known as wrapper) solves this issue"
       ! @"How? By emulating polymorphism through composition"
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
                        (classDef "Naturals" 
                          [
                            implements "Iterator<int>"
                            typedDecl "current" "int" |> makePrivate
                            typedDef "Naturals" [] "" (("current" := ConstInt(1)) >> endProgram) |> makePublic
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
                        (classDef "Evens" 
                          [
                            implements "Iterator<int>"
                            typedDecl "current" "int" |> makePrivate
                            typedDef "Evens" [] "" (("current" := ConstInt(-1)) >> endProgram) |> makePublic
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
                        (classDef "Offset" 
                          [
                            implements "Iterator<int>"
                            typedDecl "current" "int" |> makePrivate
                            typedDecl "offset" "int" |> makePrivate
                            typedDef "Offset" ["int", "offset"] "" (("current" := ConstInt(-1)) >> ("this.offset" := var "offset") >> endProgram) |> makePublic
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
            Class("Naturals", -6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("Evens", 0.0, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("Offset", 6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
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
                        (classDef "EvensFrom" 
                          [
                            implements "Iterator<int>"
                            typedDecl "current" "int" |> makePrivate
                            typedDecl "offset" "int" |> makePrivate
                            typedDef "EvensFrom" ["Offset", "offset"] "" (("current" := ConstInt(-1)) >> ("this.offset" := var "offset") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<int>" (("current" := Code.Op(var "current", Operator.Plus, (ConstInt(1)))) >> 
                                                                  Code.If(Code.Op((Code.Op(var "current", Operator.Percent, ConstInt(2))), Operator.Equals, ConstInt(0)),
                                                                          Code.New("Some<int>",[var "current" .+ var "offset"]) |> ret,
                                                                          MethodCall("this", "GetNext", []) |> ret))
                          ])) |> Unrepeated 
      ]
    SubSection "UML discussion"
    ItemsBlock
      [ ! @"As we can see our class hierarchy is growing ``horizontally'', because of lacks of reuse" 
        ! @"Let us see the UML again" ]
    UML
        [ 
            Interface("Iterator<T>",5.0,0.0,0.0,[Operation("GetNext", [], Some "IOption<T>")])
            Class("Naturals", -6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("Evens", 0.0, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("Offset", 6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
            Class("EvensFrom", 3.25, -6.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
        ]
    SubSection "Iterating a range between two integers"
    ItemsBlock
      [       
       ! @"Imagine if we now wish to implement a new data structure \texttt{Range} that takes two integers A and B (where A <= B)"
       ! @"We want \texttt{Range} to support the \texttt{Offset} and \texttt{Even} behaviors"
       ! @"We have to literally duplicate everything and to implement all possible combinations"
      ]
    SubSection "Considerations"
    ItemsBlock
      [
       ! @"Polymorphism solves our problem, but adds another one. Too many combinations"       
       ! @"Every change/add requires lots of work"
       ! @"Behavioral commonalities are not taken into consideration"
      ]
    VerticalStack
      [
      ItemsBlockWithTitle "Considerations"
        [
         ! @"How can we group such behaviors (offset and even) to define them once and use them everywhere?"
         ! @"A possible solution would see our natural number implementing offset and even"
        ]
      CSharpCodeBlock( TextSize.Tiny,
                          (classDef "EvensFrom" 
                            [
                              implements "Naturals"
                              implements "Offset"
                              implements "Evens"
                              dots
                            ])) |> Unrepeated 
      ItemsBlock [ ! @"This solution is not good, since the responsibilities are now not clear, see SOLID" ]
      ]
    
    VerticalStack
      [
        ItemsBlockWithTitle "Considerations"
          [
           ! @"Abstract classes with a series of booleans, which we can use as ``switchers'' to select the appropriate algorithm, could be another solution"       
           ! @"But fields do not force appropriate behavior for each of the roles"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                              (classDef "Naturals" 
                                [
                                  implements "Iterator<int>"
                                  typedDecl "isEven" "bool" |> makePrivate
                                  typedDecl "isOffset" "bool" |> makePrivate
                                  dots
                                ])) |> Unrepeated 
        ItemsBlock [ ! @"This solution is not good, since the responsibilities are now not clear, see SOLID" ]
      ]
    ItemsBlock
      [
        ! @"We need a better mechanism. Ideally we wish:"       
        ItemsBlock
          [
           ! @"To define once our naturals"
           ! @"To define once our even behavior"
           ! @"To define once our offset behavior"
           ! @"To apply the above behaviors ``on demand'', and not to all instances of natural lists"
           ! @"To combine the above behaviors without defining new behaviors"
          ]
      ]
    SubSection "Idea"
    ItemsBlock
      [
        ! @"A interesting solution could be to built a \textit{proxy}, like adapter, but with the possibility to add semantics!"
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Idea"
          [
            ! @"We define an intermediate entity \texttt{Decorator}, which inherits our iterator and also contains an instance of it (\texttt{decorated\_item})"
            ! @"Note \texttt{GetNext} acts as a proxy by simply calling \texttt{decorated\_item.GetNext()} and returning its result"
            ! @"\texttt{Decorator} is abstract, so you cannot create it without a ``concrete'' behavior"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (abstractClassDef "Decorator" 
                          [
                            implements "Iterator<int>"
                            typedDecl "decorated_item" "Iterator<int>" |> makeProtected
                            typedDef "Decorator" ["Iterator<int>", "decorated_item"] "" (("this.decorated_item" := var "decorated_item") >> endProgram) |> makePublic
                            TypedSigAbstract ("GetNext", [], "IOption<int>") |> makeProtected
                          ])) |> Unrepeated 
      ]

    ItemsBlock
      [
        ! @"We can think of the decorator as an iterator containing elements, but which does not know how to iterate them"
        ! @"A concrete decorator needs a specification of how to iterate"
      ]

    ItemsBlock
      [
       ! @"We now declare concrete two decorators \texttt{Even} and \texttt{Offset} that extend our \texttt{Decorator}"
       ! @"\texttt{Even} and \texttt{Offset} are unaware (and they do not need to be) of whether they are going to deal with all natural numbers, just a range of them, or something else"
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
                                typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base.decorated_item", "GetNext", []))) >>
                                                                      Code.If(MethodCallInline("current", "IsNone", []), ret (newC "None<int>" []), 
                                                                                              Code.If(Code.Op((Code.Op(var "current.GetValue()", Operator.Percent, ConstInt(2))), Operator.Equals, ConstInt(0)),
                                                                                                              Code.New("Some<int>",[var "current.GetValue()"]) |> ret,
                                                                                                              MethodCall("this", "GetNext", []) |> ret))) |> makeOverride |> makePublic
                              ])) |> Unrepeated          
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Idea: even class, with lambdas"
          [
           ! @"In the following you find the code for \texttt{Even}, with lambdas"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                            (classDef "Even" 
                              [
                                implements "Decorator"
                                typedDefWithBase "Even" ["Iterator<int>", "collection"] "" ["collection"] endProgram |> makePublic
                                typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base.decorated_item", "GetNext", []))) >>
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
           ! @"In the following you find the code for \texttt{Offset}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                            (classDef "Offset" 
                              [
                                implements "Decorator"
                                typedDecl "offset" "int" |> makePrivate
                                typedDefWithBase "Offset" ["int", "offset"; "Iterator<int>", "collection"] "" ["collection"] (("this.offset" := var "offset") >> endProgram) |> makePublic
                                typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base.decorated_item", "GetNext", []))) >>
                                                                      Code.If(MethodCallInline("current", "IsNone", []), ret (newC "None<int>" []), 
                                                                              Code.New("Some<int>",[var "current.GetValue()" .+ var "offset"]) |> ret)) |> makeOverride |> makePublic
                              ])) |> Unrepeated          
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Idea: offset class, with lambdas"
          [
           ! @"In the following you find the code for \texttt{Offset}, with lambdas"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                            (classDef "Offset" 
                              [
                                implements "Decorator"
                                typedDecl "offset" "int" |> makePrivate
                                typedDefWithBase "Offset" ["int", "offset"; "Iterator<int>", "collection"] "" ["collection"] (("this.offset" := var "offset") >> endProgram) |> makePublic
                                typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base.decorated_item", "GetNext", []))) >>
                                                                      (Code.MethodCall("current", "Visit", [Code.GenericLambdaFuncDecl([], ret (newC "None<int>" []))
                                                                                                            Code.GenericLambdaFuncDecl(["current"], Code.New("Some<int>",[var "current" .+ var "offset"]) |> ret)]))) |> makeOverride |> makePublic
                              ])) |> Unrepeated          
      ]
    ItemsBlock
      [
       ! @"With \texttt{Even} and \texttt{Offset} we managed to capture a reusable behavior that works with any collection made of numbers"
       ! @"They work on \texttt{Range} and \texttt{Numbers}"
       ! @"They work on any other collection of ints, even with those built with decorators"
       ItemsBlock
        [
          ! @"\texttt{Naturals} $\rightarrow$ \texttt{Evens} $\rightarrow$ \texttt{Offset}"
          ! @"etc."
        ]
      ] 
    ItemsBlock
      [
       ! @"The following are all examples of how to use our new data structures:"
       ! @"\texttt{Iterator<int> ns1 = new Even(new Naturals())}"
       ! @"\texttt{Iterator<int> ns2 = new Offset(new Naturals())}"
       ! @"\texttt{Iterator<int> ns2 = new Offset(new Even(new Naturals()))}"
       ! @"\texttt{Iterator<int> ns3 = new Offset(new Even(new Range(5, 10)))}"
      ]
    ItemsBlock
      [
       ! @"Note how decomposability helps to keep the interaction surface clean and reusable and the implementation compact"
       ! @"We now show the UML of our code"
      ] 
    ItemsBlock
      [
        Image("Class Diagram0.jpg", 0.3)
      ]
    SubSection "Considerations"
    ItemsBlock
      [
        ! @"The pattern seen so far follows a specific design pattern that is called the \textbf{Decorator design pattern} (a behavioral design pattern)"
        ! @"We now study its formalization and add some final considerations"
      ]
    Section("The decorator design pattern")
    SubSection "Formalism"
    ItemsBlock [ 
      ! @"Given a polymorphic type $I$ (to instantiate)"
      ! @"Given a series of concrete implementations of $I$: $C_1,..,C_m$"
      ! @"A decorator $D$ is an entity that implements $I$ and references an instance of $I$"
      ! @"Given a series of concrete decorators $CD1,...,CD_n$ extends $D$"
      ! @"As concrete $CD$'s come with difference semantics, every $CD$ is tasked to apply its semantics by overriding methods of the inherited $D$"
     ]
    ItemsBlock
      [
        Image("Class Diagram1.jpg", 0.3)
      ]
    SubSection "Generic decorators"
    ItemsBlock
      [
       ! @"We can build generic decorators"
       ! @"Genericity comes from lambdas, which act as ``holes'' in their behaviors"
       ! @"Concrete decorators can be defined by specifying the underlying iterator + the lambdas"
      ]
    SubSection "Filter"
    VerticalStack
      [
        ItemsBlockWithTitle "Filter"
          [
           ! @"A \texttt{Filter} is a generic decorator that skips some elements"
           ! @"We can use it to express our \texttt{Evens}, see code below"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                    (classDef "Filter" 
                      [
                        implements "Decorator"
                        typedDecl "p" "Func<int, bool>"
                        typedDefWithBase "Filter" ["Iterator<int>", "collection";"Func<int, bool>","p"] "" ["collection"] ("p" := var "this.p") |> makePublic
                        typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base.decorated_item", "GetNext", []))) >>
                                                              Code.If(MethodCallInline("current", "IsNone", []), ret (newC "None<int>" []),
                                                              
                                                                      Code.If(MethodCallInline("p", "Invoke", [var "current.GetValue()"]),
                                                                              Code.New("Some<int>",[var "current.GetValue()"]) |> ret,
                                                                              MethodCall("this", "GetNext", []) |> ret))) |> makeOverride |> makePublic
                      ] >>
                      (typedDeclAndInit "numbers" "Iterator<int>" (newC "Filter" 
                                                                        [newC "Range" [constInt 0; constInt 5]; 
                                                                         (Code.GenericLambdaFuncDecl(["n"], ret (Code.Op((Code.Op(var "n", Operator.Percent, ConstInt(2))), Operator.Equals, ConstInt(0)))))
                                                                        ])))) |> Unrepeated          
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Filter, with lambdas"
          [
           ! @"A \texttt{Filter} is a generic decorator that skips some elements"
           ! @"We can use it to express our \texttt{Evens}, see code below (with lambdas)"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                    (classDef "Filter" 
                      [
                        implements "Decorator"
                        typedDecl "p" "Func<int, bool>"
                        typedDefWithBase "Filter" ["Iterator<int>", "collection";"Func<int, bool>","p"] "" ["collection"] ("p" := var "this.p") |> makePublic
                        typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base.decorated_item", "GetNext", []))) >>
                                                              (Code.MethodCall("current", "Visit", [Code.GenericLambdaFuncDecl([], ret (newC "None<int>" []))
                                                                                                    Code.GenericLambdaFuncDecl(["current"], 
                                                                                                                                  Code.If(MethodCallInline("p", "Invoke", [var "current"]),
                                                                                                                                          Code.New("Some<int>",[var "current"]) |> ret,
                                                                                                                                          MethodCall("this", "GetNext", []) |> ret))]))) |> makeOverride |> makePublic
                      ] >>
                      (typedDeclAndInit "numbers" "Iterator<int>" (newC "Filter" 
                                                                        [newC "Range" [constInt 0; constInt 5]; 
                                                                         (Code.GenericLambdaFuncDecl(["n"], ret (Code.Op((Code.Op(var "n", Operator.Percent, ConstInt(2))), Operator.Equals, ConstInt(0)))))
                                                                        ])))) |> Unrepeated          
      ]
    SubSection "Map"
    VerticalStack
      [
        ItemsBlockWithTitle "Map"
          [
           ! @"A \texttt{Map} transforms all elements one after the other"
           ! @"We can use it to express our \texttt{Offset}, see code below"
          ]

        CSharpCodeBlock( TextSize.Tiny,
                            (classDef "Map" 
                              [
                                implements "Decorator"
                                typedDecl "t" "Func<int, int>"
                                typedDefWithBase "Map" ["Func<int, int>", "t"; "Iterator<int>", "collection"] "" ["collection"] (("this.t" := var "t") >> endProgram) |> makePublic
                                typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base.decorated_item", "GetNext", []))) >>
                                                                       Code.If(MethodCallInline("current", "IsNone", []), ret (newC "None<int>" []),
                                                                               Code.New("Some<int>",[Code.MethodCallInline("t", "Invoke", [var "current.GetValue()"])]) |> ret)) |> makeOverride |> makePublic
                              ] >>
                             (typedDeclAndInit "numbers" "Iterator<int>" (newC "Map" 
                                                                               [newC "Range" [constInt 0; constInt 5]; 
                                                                                     (Code.GenericLambdaFuncDecl(["n"], ret (var "current" .+ constInt 1)))
                                                                               ])))) |> Unrepeated 
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "Map, with lambdas"
          [
           ! @"A \texttt{Map} transforms all elements one after the other"
           ! @"We can use it to express our \texttt{Offset}, see code below (with lambdas)"
          ]

        CSharpCodeBlock( TextSize.Tiny,
                            (classDef "Map" 
                              [
                                implements "Decorator"
                                typedDecl "t" "Func<int, int>"
                                typedDefWithBase "Map" ["Offset", "offset"; "Iterator<int>", "collection"] "" ["collection"] (("this.offset" := var "offset") >> endProgram) |> makePublic
                                typedDef "GetNext" [] "IOption<int>" ((typedDeclAndInit "current" "Option<int>" (Code.MethodCall("base.decorated_item", "GetNext", []))) >>
                                                                      (Code.MethodCall("current", "Visit", [Code.GenericLambdaFuncDecl([], ret (newC "None<int>" []))
                                                                                                            Code.GenericLambdaFuncDecl(["current"], Code.New("Some<int>",[Code.MethodCallInline("t", "Invoke", [var "current"])]) |> ret)]))) |> makeOverride |> makePublic
                              ] >>
                             (typedDeclAndInit "numbers" "Iterator<int>" (newC "Map" 
                                                                               [newC "Range" [constInt 0; constInt 5]; 
                                                                                     (Code.GenericLambdaFuncDecl(["n"], ret (var "current" .+ constInt 1)))
                                                                               ])))) |> Unrepeated 
      ]
    SubSection "Map and Filter"
    ItemsBlock
      [
        ! @"Of course we can combine them, so to express our \texttt{EvensFrom}"
        ! @"\texttt{Iterator<int>} numbers = \texttt{new Filter(new Map(new Range(0,5), n => n + 1), n => n \% 2 == 0);}"
      ]
    Section "Conclusions"     
    SubSection "Conclusions"
    ItemsBlock
      [
        ! @"Sometimes, we need to apply behaviors instances dynamically"
        ! @"Hand made combinations could be a solution, but the number of combinations is huge"
        ! @"The decorator pattern solves this issue by emulating a customizable, dynamic form of polymorphism through composition"
        ! @"In short, a decorator allows behavior to be added to an individual object, without affecting other objects"
      ]
  ]