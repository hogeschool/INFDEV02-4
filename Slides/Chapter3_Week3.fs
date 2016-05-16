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
        ! @"Independent domains each based on its interface(s)"
        ! @"No shared code, so they cannot communicate directly"
        ! @"Semantically compatible: we want to connect them"
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
       ! @"Today we are going to study adapters"
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
        ! @"An option as an iterator"
        ! @"A traditional iterator as a safe iterator"
        ! @"A class belonging to a closed library with the interface required by our application"
        ! @"A shape in another drawing library"
        ! @"..."
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
          ! @"We need to group lines and rectangles together, plus our own classes"
          ! @"Cast to \texttt{Object}?"
        ]
      CSharpCodeBlock(TextSize.Tiny,
                      (genericTypedDeclAndInit ["Object"] "shapes" "List" (Code.GenericNew("List", ["Object"] , [])) >>
                       Code.MethodCall("shapes","Add", [newC "LegacyLine" []]) >>
                       Code.MethodCall("shapes","Add", [newC "LegacyRectangle" []]) >>
                       Code.MethodCall("shapes","Add", [newC "NonLegacyCircle" []]) >>
                       Code.Foreach("Object", "shape", var "shapes",
                                    ((Code.IfThen(InstanceOf(var "shape","NonLegacyCircle"),
                                                  MethodCall("(NonLegacyCircle)shape", "Draw", [dots]))) >>
                                     (Code.IfThen(InstanceOf(var "shape","LegacyLine"),
                                                  MethodCall("(LegacyLine)shape", "Draw", [dots]))) >>
                                     (Code.IfThen(InstanceOf(var "shape","LegacyRectangle"),
                                                  MethodCall("(LegacyRectangle)shape", "Draw", [dots]))))))) |> Unrepeated          
      ]
    SubSection("Issues with consuming LegacyLine and LegacyRectangle")
    ItemsBlock
      [
            ! @"This technique is complex and error-prone"
            ! @"We cannot even apply a visitor, since we cannot touch the implementation of \texttt{Legacy*}"
            ! @"We wish now to reduce such complexity and to achieve safety"
      ]
    SubSection("Safely consuming LegacyLine and LegacyRectangle: idea")
    VerticalStack
      [
        ItemsBlockWithTitle("Safely consuming LegacyLine and LegacyRectangle: idea ")
          [
            ! @"We define a mediating layer that abstracts instances of both \texttt{LegacyLine} and \texttt{LegacyRectangle}"
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
    SubSection("Safely consuming LegacyLine and LegacyRectangle")
    VerticalStack
      [
        ItemsBlock
          [
            ! @"Our drawing system can now define a list of shapes"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (genericTypedDeclAndInit ["Shape"] "shapes" "List" (Code.GenericNew("List", ["Shape"], [])) >>
                         Code.MethodCall("shapes","Add", [newC "Line" [newC "LegacyLine" []]]) >>
                         Code.MethodCall("shapes","Add", [newC "Rectangle" [newC "LegacyRectangle"[]]]) >>
                         Code.MethodCall("shapes","Add", [newC "NonLegacyCircle" []]) >>
                         Code.Foreach("Shape", "shape", var "Shapes",
                                    (MethodCall("shape", "Draw", [dots]))))) |> Unrepeated          
      ]
    VerticalStack
      [
        ItemsBlock
          [
            ! @"We could even extend our \texttt{Shape} with a visitor"
          ]
        CSharpCodeBlock(TextSize.Tiny, 
                        (interfaceDef "Shape" 
                                      [typedSig "Draw" [("int", "x1"); ("int", "y1"); ("int", "x2"); ("int", "y2");] "void" 
                                       typedSig "Visit<U>" [("Func<U>", "onLegacyLine"); ("Func<U>", "onLegacyRectangle"); ("Func<U>", "onNonLegacyCircle");] "U" ])) |> Unrepeated
      ]
    SubSection("Considerations")
    ItemsBlock
      [
        ! @"Instances of both \texttt{LegacyLine} and \texttt{LegacyRectangle} are now harmoniously integrated with our own framework"
        ! @"Code is more maintainable, and we have not changed (and potentially broken) the legacy implementations"
        ! @"Only requirement is that we never manipulate legacy instances directly, but go through \texttt{Rectangle} and \texttt{Line}"
        ! @"\texttt{Rectangle} and \texttt{Line} are \textbf{adapters}"
      ]
    UML
        [ Package("LegacyPackage", 
                  [ 
                    Class("LegacyRectangle", -6.5, 1.5, Option.None, [], [])
                    Class("LegacyLine", -6.5, -1.5, Option.None, [], []) 
                  ])
          Package("DrawingSystem", 
                  [
                   Class("Client", 6.5, -3.0, Option.None, [], [])
                   Interface("Shape",3.0,6.5,0.5,[Operation("Draw", [], Option.None)])
                   Class("Rectangle", 2.0, 1.5, Some "Shape", [], [])
                   Class("Line", 2.0, -1.5, Some "Shape", [], [])
                   Class("Circle", 2.0, -3.5, Some "Shape", [], [])
                   Aggregation("Client","",Option.None,"Shape")
                   Aggregation("Rectangle","1",Option.None,"LegacyRectangle")
                   Aggregation("Line","1",Option.None,"LegacyLine")
                   ])

        ]
    Section("The adapter design pattern")
    SubSection("General idea")
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
        ! @"The \texttt{Adapter} implements \texttt{Target} by means of a reference to texttt{Source}"
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
    SubSection("Making an option ``iterable'': a naive approach")
    VerticalStack
      [
        ItemsBlockWithTitle("Making Option ``iterable'', a naive approach:")
          [
            ! @"Without adapter, we need \texttt{Option<T>} to implement \texttt{Iterator<T>}"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                          (genericInterfaceDef ["T"] "Iterator" 
                            [
                            typedSig "GetNext" [] "Option<T>" |> makePublic
                            ])) |> Unrepeated          
        CSharpCodeBlock(TextSize.Tiny,
                          (genericInterfaceDef ["T"] "Option" 
                            [
                            implements "Iterator<T>"
                            dots
                            ])) |> Unrepeated          
      ]
    SubSection("Making Some ``iterable'': a naive approach")
    VerticalStack
      [
        ItemsBlockWithTitle("Making Some ``iterable'', a naive approach")
          [
            ! @"Calling \texttt{GetNext} on \texttt{Some} returns only once its \texttt{Value} within a \texttt{Some}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                  (genericClassDef ["T"]
                    "Some" 
                    [
                      implements "Iterator<T>"
                      typedDecl "value" "T" |> makePrivate
                      typedDeclAndInit "visited" "bool" (constBool(false)) |> makePrivate
                      typedDef "Some" ["T","value"] "" (("this.value" := var"value") >> endProgram) |> makePublic
                      typedDef "GetNext" [] "Option<T>" (Code.If(var ("visited"),
                                                                      (Code.New("None<T>",[]) |> ret),
                                                                      (("visited" := ConstBool(true)) >>
                                                                        ((genericNewC "Some" ["T"] [var "value"]) |> ret))))
                      dots
                    ])) |> Unrepeated
      ] 
    SubSection("Making None ``iterable'': a naive approach")
    VerticalStack
      [
        ItemsBlockWithTitle("Making None ``iterable'', a naive approach")
          [
            ! @"Calling \texttt{GetNext} returns always \texttt{None}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                  (genericClassDef ["T"]
                    "None" 
                    [
                      implements "Iterator<T>"
                      typedDef "GetNext" [] "Option<T>" (Code.New("None<T>",[]) |> ret)
                      dots
                    ])) |> Unrepeated
      ] 

    SubSection("Making an option ``iterable'': considerations")
    ItemsBlock
      [
        ! @"Is it always needed for the option to be iterable?"
        Pause
        ! @"No!"
        ! @"According to the single responsibility principle of SOLID, \texttt{Option} should not include considerations regarding iteration\footnote{That is why we presented all the iterators through adapter in the previous lecture.}"
        ! @"Adapter solution is better, as it allows to extend option to any additional services required without changing the option data structure"
      ]
    SubSection("Iterating an Option<T> with adapters")
    VerticalStack
      [
        ItemsBlockWithTitle("Iterating an Option<T> with adapters")
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
    SubSection("Considerations about bijective adapters")
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

    // Repeat the interfaces TraditionalIterator and Iterator
    // What was the point of one and the other?
    // We need both, as they both make sense within their respective contexts!
    // How do we bridge the two worlds? With an adapter per direction!
          
    CSharpCodeBlock(TextSize.Tiny,
                    (genericClassDef ["T"]
                      "MakeSafe" 
                      [
                        implements "Iterator<T>"
                        genericTypedDecl ["T"] "iterator" "TraditionalIterator" |> makePrivate
                        typedDef "MakeSafe" ["TraditionalIterator<T>","iterator"] "" (("this.iterator" := var"iterator") >> endProgram) |> makePublic
                        typedDef "GetNext" [] "Option<T>" (Code.If(MethodCallInline ("iterator", "MoveNext", []),
                                                                        ([MethodCall("iterator" , "GetCurrent", [])] |> genericNewC "Some" ["T"]  |> ret),
                                                                        (Code.New("None<T>",[]) |> ret)))]))
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
        ! @"This semantic neutrality is common to all adapters: no information is added or removed"
        ! @"Adapters preserve the full behavior of the adapted interface"
        ! @"Adapters are simply ``bridges'' between domains, and contain no domain logic themselves"
      ]

    SubSection("Conclusion")
    ItemsBlock 
      [
        ! @"Code usually is partitioned in (closed) domains"
        ! @"Sometimes it cannot be changed: a library, a framework, etc.."
        ! @"Sometimes it is hard to make existing code work in a specific target application (for example because it is written with other conventions or is simply legacy)"
        ! @"The adapter pattern allows the adaptation of such code in a way that makes the resulting solution flexible and safe"
        ! @"How? By providing a neutral adapter that mediates between the target and source domains"
      ]
  ]