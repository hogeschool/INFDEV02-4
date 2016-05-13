module Chapter3.Week3.v1

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
        ! @"Issues arising from connecting domains"
        ! @"The adapter design pattern"
        ! @"Examples and considerations"
        ! @"Conclusions"
      ]
    SubSection("Issues:")
    ItemsBlock
      [
        ! @"Independent domains based each its interface(s)"
        ! @"They share no code, so we cannot make them communicate"
        ! @"Sometimes the logics of one might still be compatible with the other"
      ]
    SubSection("Examples:")
    ItemsBlock
      [
        ! @"Legacy systems"
        ! @"Different frameworks"
        ! @"Closed libraries"
        ! @"Etc.."
      ]
    Section("Adapter")
    SubSection("Introduction")
    ItemsBlock
      [
       ! @"Today we are going to study code adapters"
       ! @"In particular, we are going to study how to make existing classes work within other domains without modifying their code" 
       ! @"How? By means of a design pattern: the adapter (a behavioral design pattern)"
       ! @"A clean and general mechanism that allows an instance of an interface to be used where another interface is expected"
      ]

    SubSection("Adapting existing classes")
    ItemsBlock
      [
        ! @"A further constraint is that we cannot change the original implementation"
        ! @"Why?"
        Pause
        ! @"\textit{We might break other programs depending on such implementation}"

      ]
    SubSection("Examples:")
    ItemsBlock
      [
        ! @"An option as an iterator,"
        ! @"A traditional iterator as a safe iterator,"
        ! @"A class belonging to a closed library with the interface required by our application,"
        ! @"A shape in another drawing library,"
        ! @"Etc." 
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
          ! @"Suppose we wished to build a drawing system"
          ! @"We need to group lines and rectangles together"
          ! @"Cast to \texttt{Object}?"
        ]
      CSharpCodeBlock(TextSize.Tiny,
                      (genericTypedDeclAndInit ["Object"] "shapes" "List" (Code.GenericNew("List", ["Object"] , [])) >>
                       Code.MethodCall("shapes","Add", [newC "LegacyLine" []]) >>
                       Code.MethodCall("shapes","Add", [newC "LegacyRectangle" []]) >>
                       Code.Foreach("Object", "shape", var "shapes",
                                    ((Code.IfThen(InstanceOf(var "shape","LegacyLine"),
                                                  MethodCall("(LegacyLine)shape", "Draw", [dots]))) >>
                                     (Code.IfThen(InstanceOf(var "shape","LegacyRectangle"),
                                                  MethodCall("(LegacyRectangle)shape", "Draw", [dots]))))))) |> Unrepeated          
      ]
    SubSection("Issues with consuming LegacyLine and LegacyRectangle")
    ItemsBlock
      [
            ! @"As we can see from the example consuming instances of such classes is complex and error-prone"
            ! @"We could of course apply a visitor, but in this case it is not possible, since we cannot touch the implementation"
            ! @"We wish now to reduce such complexity and to achieve safety"
      ]
    SubSection("Consuming ``safely'' LegacyLine and LegacyRectangle: idea ")
    VerticalStack
      [
        ItemsBlockWithTitle("Consuming ``safely'' LegacyLine and LegacyRectangle: idea")
          [
            ! @"A solution would be to define an intermediate mediating layer that abstracts instances of both \texttt{LegacyLine} and \texttt{LegacyRectangle}"
            ! @"For this implementation we first define an interface \texttt{Shape} with one method signature \texttt{Draw}"
            ! @"This interface defines the entry of our own domain"
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
            ! @"\texttt{Line} exists both in the legacy and our new domain"
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
            ! @"Our drawing system can now define a list of shapes"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (genericTypedDeclAndInit ["Shape"] "shapes" "List" (Code.GenericNew("List", ["Shape"], [])) >>
                         Code.MethodCall("shapes","Add", [newC "Line" [newC "LegacyLine" []]]) >>
                         Code.MethodCall("shapes","Add", [newC "Rectangle" [newC "LegacyRectangle"[]]]) >>
                         Code.Foreach("Shape", "shape", var "Shapes",
                                    (MethodCall("shape.Value", "Draw", [dots]))))) |> Unrepeated          
      ]
    VerticalStack
      [
        ItemsBlockWithTitle("Consuming ``safely'' LegacyLine and LegacyRectangle")
          [
            ! @"We could even extend our \texttt{Shape} with a visitor"
          ]
        CSharpCodeBlock(TextSize.Tiny, 
                        (interfaceDef "Shape" 
                                      [typedSig "Draw" [("int", "x1"); ("int", "y1"); ("int", "x2"); ("int", "y2");] "void" 
                                       typedSig "Visit<U>" [("Func<U>", "onLegacyLine"); ("Func<U>", "onLegacyRectangle");] "U" ])) |> Unrepeated
      ]
    SubSection("Considerations")
    ItemsBlock
      [
        ! @"As we can see our program now manages instances of both \texttt{LegacyLine} and \texttt{LegacyRectangle} without requiring to manually deal with their details"
        ! @"This makes the code not only more maintainable but also safer, since the original implementation remains the same"
        ! @"In this way our program deals with objects of type \texttt{Rectangle} and \texttt{Line} as if they are concrete \texttt{LegacyLine} and \texttt{LegacyRectangle} objects without changing concrete functionalities"
      ]
    UML
        [ Package("LegacyPackage", 
                  [ 
                    Class("LegacyRectangle", -6.5, 1.5, Option.None, [], [])
                    Class("LegacyCircle", -6.5, -1.5, Option.None, [], []) 
                  ])
          Package("DrawingSystem", 
                  [
                   Class("Client", 6.5, -3.0, Option.None, [], [])
                   Interface("Shape",3.0,6.5,0.5,[Operation("Draw", [], Option.None)])
                   Class("Rectangle", 2.0, 1.5, Some "Shape", [], [])
                   Class("Circle", 2.0, -1.5, Some "Shape", [], []) 
                   Aggregation("Client","",Option.None,"Shape")
                   Aggregation("Rectangle","1",Option.None,"LegacyRectangle")
                   Aggregation("Circle","1",Option.None,"LegacyCircle")
                   ])

        ]
    SubSection("The adapter design pattern")
    ItemsBlock
      [
        ! @"By means of adapters, we ``convert'' the interface of a class into another, without touching the class sources"
        ! @"In what follows we will study such design pattern and provide a general formalization"
      ]
    SubSection("The adapter design pattern structure")
    ItemsBlock
      [
        ! @"Given two different interfaces \texttt{Source} and \texttt{Target}"
        ! @"An \texttt{Adapter} is built to adapt \texttt{Source} to \texttt{Target}"
        ! @"The \texttt{Adapter} implements \texttt{Target} and contains a reference to texttt{Source}"
        ! @"A \texttt{Client} interacts with the \texttt{Adapter} whenever it a \texttt{Target}, but we have a \texttt{Some}"
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
    SubSection("Example:")
    ItemsBlock
      [
        ! @"Consider the \texttt{Option} data type"
        ! @"It is a collection of sorts"
        ! @"It could be iterated, but it does not implement an interator!"
      ]
    SubSection("Iterating an Option<T>")
    VerticalStack
      [
        ItemsBlockWithTitle("Iterating an Option<T>")
          [
            ! @"In this case \texttt{Target} is \texttt{Iterator<T>}, \texttt{Source} is \texttt{Option<T>}, and \texttt{Adapter} is \texttt{IOptionIterator<T>}"
            ! @"Now, \texttt{GetNext} returns \texttt{Some} only the at the first iteration"
            ! @"Note, if we iterate a \texttt{None} entity we return \texttt{None}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (genericClassDef ["T"]
                          "IOptionIterator" 
                          [
                            implements "Iterator<T>"
                            genericTypedDecl ["T"] "option" "Option" |> makePrivate
                            typedDeclAndInit "visited" "bool" (constBool(false)) |> makePrivate
                            typedDef "IOptionIterator" ["Option<T>","option"] "" (("this.option" := var"option") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "Option<T>" (Code.If(var ("visited"),
                                                                           (Code.New("None<T>",[]) |> ret),
                                                                           (("visited" := ConstBool(true)) >>
                                                                             Code.If(MethodCallInline("option", "IsSome", []),
                                                                                     (genericNewC "Some" ["T"] [MethodCall("option", "GetValue", [])] |> ret),
                                                                                     (genericNewC "None" ["T"] [] |> ret)))))])) |> Unrepeated
      ] 
    SubSection("Iterating an Option<T>")
    VerticalStack
      [
        ItemsBlockWithTitle("Iterating an Option<T>")
          [
            ! @"Which with visitor becomes:"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (genericClassDef ["T"]
                          "IOptionIterator" 
                          [
                            implements "Iterator<T>"
                            genericTypedDecl ["T"] "option" "Option" |> makePrivate
                            typedDeclAndInit "visited" "bool" (constBool(false)) |> makePrivate
                            typedDef "IOptionIterator" ["Option<T>","option"] "" (("this.option" := var"option") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "Option<T>" (Code.If(var ("visited"),
                                                                           (Code.New("None<T>",[]) |> ret),
                                                                           (("visited" := ConstBool(true)) >>
                                                                            (MethodCall("option" , "Visit<Option<T>>", 
                                                                                       [(Code.GenericLambdaFuncDecl([], Code.New("None<T>", []) |> ret) )
                                                                                        (Code.GenericLambdaFuncDecl(["t"], Code.New("Some<T>", [var "t"]) |> ret) )]) |> ret))))])) |> Unrepeated
      ] 
    SubSection("Considerations about bijectivity")
    ItemsBlock 
      [
        ! @"Adapters map behaviors across domains"
        ! @"Adapting may not change or add behaviors"
        
      ]
    ItemsBlock
      [
        ! @"Consider the \texttt{TraditionalIterator} and \texttt{Iterator} example"
        ! @"We can adapt in both directions!"
            
      ]
      
    CSharpCodeBlock(TextSize.Tiny,
                    (genericClassDef ["T"]
                      "MakeSafe" 
                      [
                        implements "Iterator<T>"
                        genericTypedDecl ["T"] "iterator" "TraditionalIterator" |> makePrivate
                        typedDef "MakeSafe" ["TraditionalIterator<T>","iterator"] "" (("this.iterator" := var"iterator") >> endProgram) |> makePublic
                        typedDef "GetNext" [] "Option<T>" (Code.If(MethodCallInline ("iterator", "MoveNext", []),
                                                                        ([MethodCall("iterator" , "GetCurrent", [])] |> genericNewC "Some" ["T"]  |> ret),
                                                                        (Code.New("None<T>",[]) |> ret)))])) |> Unrepeated
    CSharpCodeBlock(TextSize.Tiny,
                    (genericClassDef ["T"]
                      "MakeUnsafe" 
                      [
                        implements "TraditionalIterator<T>"
                        typedDecl "T" "_current" |> makePrivate
                        genericTypedDecl ["T"] "iterator" "Iterator" |> makePrivate
                        typedDef "MakeUnsafe" ["Iterator<T>","iterator"] "" (("this.iterator" := var"iterator") >> endProgram) |> makePublic
                        typedDef "GetCurrent" [] "T" (var "_current" |> ret)
                        typedDef "MoveNext" [] "bool" (typedDeclAndInit "opt" "Option<T>" (MethodCallInline("iterator", "GetNext",[])) >>
                                                       Code.If(MethodCallInline ("opt", "IsSome", []),
                                                                        (("_current" := (MethodCall("iterator", "GetValue", []))) >>
                                                                         (constBool(true)  |> ret)),
                                                                        (constBool(false)  |> ret)))
                      ])) |> Unrepeated
    ItemsBlock [
      ! @"What is the behavior of \texttt{new MakeSafe(new MakeUnsafe(it))} for a generic iterator it?"
      Pause
      ! @"No change! The two behave exactly the same!"
      ]
    ItemsBlock [
      ! @"What is the behavior of \texttt{new MakeUnsafe(new MakeSafe(it))} for a generic iterator it?"
      Pause
      ! @"No change! The two behave exactly the same!"
      ]
    ItemsBlock
      [
        ! @"An adapter does not add or remove information, in order to preserve the correctness of the involved interface adapters"        
        ! @"Adapters are simply ``bridges'' to let abstractions vary independently, and contain no domain logic"
      ]

    SubSection("Conclusion")
    ItemsBlock 
      [
        ! @"Code comes in different forms"
        ! @"Sometimes code cannot be changed: a library, a framework, etc.."
        ! @"Sometimes it is hard to make existing code work in a specific target application (for example because it is written with other conventions or is simply legacy)"
        ! @"The adapter pattern allows the adaptation of such code in a way that makes the resulting solution flexible and safe"
        ! @"How? By providing an custom adapter that mediates between the targeted client and the code to adapt"
      ]
  ]