module Chapter3.Week3.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
    Section("Adapting interfaces")
    Section("Introduction")
    SubSection("Lecture topics")
    ItemsBlock
      [
        ! @"Issues arising from importing and using entities"
        ! @"The adapter design pattern"
        ! @"Examples and considerations"
        ! @"Conclusions"
      ]
    SubSection("The adapter design pattern")
    SubSection("Introduction")
    ItemsBlock
      [
       ! @"Today we are going to study code adapters"
       ! @"In particular, we are going to study how to make existing classes work with others without modifying their code" 
       ! @"How? By means of a design pattern: the adapter (a behavioral design pattern)"
       ! @"We will see the adapter provides a clean and general mechanism that allows an interface of an existing class to be used as another interface"
      ]

    SubSection("Adapting existing classes")
    ItemsBlock
      [
        ! @"It is often the case where we need to adapt existing entities to other"
        ! @"For example we wish to treat an option by means of an iterator, traditional iterator as a safe iterator, or a class belonging to a closed library with the interface required by our application" 
        ! @"With the only constraint that we cannot change the original implementation and the original functionalities"
        ! @"Why?"
        Pause
        ! @"Otherwise we might break other programs depending on such implementation" 

      ]

    SubSection("An example of similar but incompatible classes")
    VerticalStack
      [
      ItemsBlockWithTitle("An example of similar but incompatible classes")
        [
          ! @"Consider the following two classes \texttt{LegacyLine} and \texttt{LegacyRectangle}"
          ! @"Both implementing a draw method"          
        ]
      CSharpCodeBlock(TextSize.Tiny,
                      (classDef "LegacyLine" 
                        [
                        typedDef "Draw" [("int", "x1"); ("int", "y1"); ("int", "x2"); ("int", "y2");] "void" 
                                 (Code.StaticMethodCall("Console", "WriteLine", [var "\"line from (\" + x1 + ',' + y1 + \") to (\" + x2 + ',' + y2 + ')'\""])) |> makePublic
                        ] >>
                       classDef "LegacyRectangle" 
                        [
                        typedDef "Draw" [("int", "x"); ("int", "y"); ("int", "w"); ("int", "h");] "void" 
                                 (Code.StaticMethodCall("Console", "WriteLine", [var "\"rectangle at (\" + x + ',' + y + \") with width \" + w + \" and height \" + h"])) |> makePublic
                        ])) |> Unrepeated          
      ]
    SubSection("Consuming our LegacyLine and LegacyRectangle")
    VerticalStack
      [
      ItemsBlockWithTitle("Consuming our LegacyLine and LegacyRectangle")
        [
          ! @"We now wish to consume instances of \texttt{LegacyLine} and \texttt{LegacyRectangle} in our application"
          ! @"We group such instances within the same collection, so to deal with them all at once, thus to avoid duplication"
          ! @"The collection is of type \texttt{Object}. Why? Because \texttt{LegacyLine} and \texttt{LegacyRectangle} do not share a same type"
        ]
      CSharpCodeBlock(TextSize.Tiny,
                      (typedDeclAndInit "shapes" "LinkedList<Object>" (Code.New("LinkedList<Object>", [])) >>
                       Code.MethodCall("shapes","Add", [newC "LegacyLine" []]) >>
                       Code.MethodCall("shapes","Add", [newC "LegacyRectangle" []]) >>
                       Code.While(Code.Op(var "shapes.Tail", Operator.NotEquals, Code.None),
                                  ((Code.IfThen(MethodCallInline("shapes", "Head.getClass().getName().equals", [constString "LegacyLine"]),
                                                MethodCall("(LegacyLine)shapes.Head.Value", "Draw", [dots]))) >>
                                   (Code.IfThen(MethodCallInline("shapes", "Head.getClass().getName().equals", [constString "LegacyRectangle"]),
                                                MethodCall("(LegacyRectangle)shapes.Head.Value", "Draw", [dots]))) >>
                                   ("shapes" := var "shapes.Tail"))))) |> Unrepeated          
      ]
    SubSection("Issues with consuming LegacyLine and LegacyRectangle")
    ItemsBlock
      [
            ! @"As we can see from the example consuming instances of such classes is complex and error-prone"
            ! @"We could of course apply a visitor, but in this case it is not possible, since we cannot touch the their implementation"
            ! @"We wish now to reduce such complexity and to achieve safeness"
      ]
    SubSection("Consuming ``safely'' LegacyLine and LegacyRectangle: idea ")
    VerticalStack
      [
        ItemsBlockWithTitle("Consuming ``safely'' LegacyLine and LegacyRectangle: idea")
          [
            ! @"A solution would be to define an intermediate layer that mediates for us with instances of both \texttt{LegacyLine} and \texttt{LegacyRectangle}"
            ! @"For this implementation we need first to define an interface \texttt{Shape} with one method signature \texttt{Draw}"
          ]
        CSharpCodeBlock(TextSize.Tiny, (interfaceDef "Shape" [typedSig "Draw" [("int", "x1"); ("int", "y1"); ("int", "x2"); ("int", "y2");] "void" ])) |> Unrepeated
      ]
    SubSection("An adapter for our LegacyLine")
    VerticalStack
      [
        ItemsBlockWithTitle("An adapter for our LegacyLine")
          [
            ! @"We declare a class \texttt{Line} that takes as input a \texttt{LegacyLine} object"
            ! @"Whenever the \texttt{Draw} method is called also the \texttt{Draw} of the \texttt{LegacyLine} object is called"
          ]
        CSharpCodeBlock(TextSize.Tiny, 
                        (classDef "Line" 
                          [
                          implements "Shape"
                          typedDecl "underlyingLine" "LegacyLine" |> makePrivate
                          typedDef "Line" ["LegacyLine","line"] "" (("this.underlyingLine" := var"line") >> endProgram) |> makePublic
                          typedDef "Draw" [("int", "x1"); ("int", "y1"); ("int", "x2"); ("int", "y2");] "void" 
                                   (Code.MethodCall("underlyingLine", "Draw", [dots])) |> makePublic
                          ])) |> Unrepeated          
      ]
    SubSection("An adapter for our LegacyRectangle")
    VerticalStack
      [
        ItemsBlockWithTitle("An adapter for our LegacyRectangle")
          [
            ! @"We apply the same mechanism to our \texttt{LegacyRectangle}"
          ]
        CSharpCodeBlock(TextSize.Tiny, 
                        (classDef "Rectangle" 
                          [
                          implements "Shape"
                          typedDecl "underlyingRectangle" "LegacyRectangle" |> makePrivate
                          typedDef "Rectangle" ["LegacyRectangle","rectangle"] "" (("this.underlyingRectangle" := var"rectangle") >> endProgram) |> makePublic
                          typedDef "Draw" [("int", "x1"); ("int", "y1"); ("int", "x2"); ("int", "y2");] "void" 
                                   (Code.MethodCall("underlyingRectangle", "Draw", [dots])) |> makePublic
                          ])) |> Unrepeated          
      ]
    SubSection("Consuming ``safely'' LegacyLine and LegacyRectangle")
    VerticalStack
      [
        ItemsBlockWithTitle("Consuming ``safely'' LegacyLine and LegacyRectangle")
          [
            ! @"The main program will now define a list of shapes containing instances of shapes each referencing an instance of either \texttt{LegacyLine} or \texttt{LegacyRectangle}"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (typedDeclAndInit "shapes" "LinkedList<Shape>" (Code.New("LinkedList<Shape>", [])) >>
                         Code.MethodCall("shapes","Add", [newC "Line" [newC "LegacyLine" []]]) >>
                         Code.MethodCall("shapes","Add", [newC "Rectangle" [newC "LegacyRectangle"[]]]) >>
                         Code.While(Code.Op(var "shapes.Tail", Operator.NotEquals, Code.None),
                                    (MethodCall("shapes.Head.Value", "Draw", [dots])) >>
                                     ("shapes" := var "shapes.Tail")))) |> Unrepeated          
      ]
    SubSection("Considerations")
    ItemsBlock
      [
        ! @"As we can see our program now manages instances of both \texttt{LegacyLine} and \texttt{LegacyRectangle} without requiring to manually deal with their details"
        ! @"This makes the code not only more maintainable but also safer, since the original implementation remains the same"
        ! @"This example "
        ! @"In this way our program deals with objects of type \texttt{Rectangle} and \texttt{Line} as if they are concrete \texttt{LegacyLine} and \texttt{LegacyRectangle} objects without changing its concrete functionalities"
      ]

    SubSection("The adapter design pattern")
    ItemsBlock
      [
        ! @"Is a design pattern that abstracts the just described mechanism"
        ! @"By means of adapters, it allows to convert the interface of a class into another one that a client expects without changing its functionalities"
        ! @"In what follows we will study such design pattern and provide a general formalization"
      ]
    SubSection("The adapter design pattern structure")
    ItemsBlock
      [
        ! @"Given two different interfaces \texttt{Source} and texttt{Target}"
        ! @"An texttt{Adapter} is built to, carefully,  adapt texttt{Source} to texttt{Target}"
        ! @"The texttt{Adapter} implements texttt{Target} and contains a reference to texttt{Source}"
        ! @"A texttt{Client} interacts with the texttt{Adapter} whenever it needs to interact with texttt{Source} as if it is an instance of texttt{Target}"
        ! @"In the following we provide a UML for such structure"
      ]
    UML
        [ Package("Iterator", 
                  [
                   Class("Client", -6.5, -1.5, Option.None, [], [])
                   Interface("Target",3.0,5.5,0.0,[Operation("RequiredMethod", [], Option.None)])
                   Class("Adapter", 0.0, 0.0, Some "Target", [], [Attribute("source", "Source"); Operation("RequiredMethod", [], Option.None)])
                   Class("Source", 0.0, -3.0, Option.None, [], [Operation("OldMethod", [], Option.None)])
                   Arrow("Client","<<instantiate>>","Adapter")
                   Aggregation("Adapter","",Option.None,"Source")

                   ])

        ]
    SubSection("Iterating an IOption<T>")
    ItemsBlock
      [
        ! @"Adapters successfully achieve the task of making an source interface ``behaving'' as another"
        ! @"It is the case of the option introduced in the previously"
        ! @"As an option can be seen as a collection that might contains at most one element"
        ! @"We can treat such option as an iterator"
        ! @"How? By means of an adapter"
      ]
    SubSection("Iterating an IOption<T>")
    VerticalStack
      [
        ItemsBlockWithTitle("Iterating an IOption<T>")
          [
            ! @"In this case \texttt{Target} is \texttt{Iterato<T>}, \texttt{Source} is \texttt{IOption<T>}, and \texttt{Adapter} is \texttt{IOptionIterator<T>}"
            ! @"Now, \texttt{GetNext} returns \texttt{Some} only the at the first iteration and \texttt{None} for the rest"
            ! @"Note, if we iterate a \texttt{None} entity we return \texttt{None}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (genericClassDef ["T"]
                          "IOptionIterator" 
                          [
                            implements "Iterator<T>"
                            typedDecl "option" "IOption<T>" |> makePrivate
                            typedDeclAndInit "visited" "bool" (constBool(false)) |> makePrivate
                            typedDef "IOptionIterator" ["IOption<T>","option"] "" (("this.option" := var"option") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<int>" (Code.If(var ("visited"),
                                                                           (Code.New("None<T>",[]) |> ret),
                                                                           (("visited" := ConstBool(true)) >>
                                                                            (MethodCall("option" , "Visit<IOption<T>>", 
                                                                                       [(Code.GenericLambdaFuncDecl([], Code.New("None<T>", []) |> ret) )
                                                                                        (Code.GenericLambdaFuncDecl(["t"], Code.New("Some<T>", [var "t"]) |> ret) )]) |> ret))))])) |> Unrepeated
      ] 
    SubSection("Considerations about reversibility")
    ItemsBlock 
      [
        ! @"Adapters as we can see successfully achieve the task of adapting imported and custom client interfaces without changing the imported interface"
        ! @"However, it is important that adapting does not change the intended behavior of the imported interface"
        
      ]
    VerticalStack
      [
        ItemsBlockWithTitle("Considerations about reversibility")
          [
            ! @"Consider the \texttt{TraditionalIterator} and \texttt{Iterator} example"
            ! @"Adapting a \texttt{TraditionalIterator} to an \texttt{Iterator} does not change the order of iteration (see previous class)"
            ! @"But, adapting an \texttt{Iterator} to \texttt{TraditionalIterator} changes the order of iteration"
          ]
      ]
    ItemsBlock
      [
        ! @"An adapter does not add or remove information, in order to preserve the correctness of the involved interface adapters"        
        ! @"Adapting interface should not affect their intended logical mechanisms"
        ! @"Adapters are simply ``bridges'' to let abstractions vary independently"
        ! @"Thus, \textbf{Safe(Unsafe(list)) = list} and \textbf{Unsafe(Safe(list)) = list}"
      ]

    SubSection("Conclusion")
    ItemsBlock 
      [
        ! @"Code comes in different forms"
        ! @"For many cases code cannot be changed: like a library, a toolkit, etc.."
        ! @"Sometimes it is hard to make such code work in a specific target application (for example because it is written at a different time)"
        ! @"The adapter pattern allows the adaptation of such code in a way that makes the resulting solution safe and maintainable"
        ! @"How? By providing an custom adapter that mediated between the targeted client and the code to adapt"
      ]
  ]