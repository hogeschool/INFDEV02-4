module Chapter2.Week2.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
    Section("Dev 4 - Lecture topics")
    SubSection("Lecture topics")
    ItemsBlock
      [
        ! @"Breaking down design patterns, an introduction"
        ! @"Iterating collections"
        ! @"Concrete examples of the iterator design pattern"
        ! @"Conclusions"
      ]
    Section("Breaking down design patterns")
    SubSection("Introduction")
    ItemsBlock
      [
        ! @"After having seen the 1st design pattern, we can add some depth to the discussion"
        ! @"Design patterns have been grouped in several specific categories:"
        ! @"Behavioral"
        ! @"Structural"
        ! @"Creational"
        ! @"In this course we will always try, when introducing a design pattern, to picture it with respect to such categories"
      ]
    SubSection("Behavioral patterns")
    VerticalStack
      [
        ItemsBlockWithTitle("Behavioral patterns")
          [
            ! @"Are design patterns for identifying the fundamental communication behavior between entities"
            ! @"Among such pattern we find:"
          ]
        ItemsBlock
          [
            ! @"\textbf{Visitor pattern}"
            ! @"State pattern"
            ! @"Strategy pattern"
            ! @"\textbf{Null Object pattern}"
            ! @"Iterator pattern"
            ! @"etc.."
          ]

          
      ]
    SubSection("Structural patterns")
    VerticalStack
      [
        ItemsBlockWithTitle("Structural patterns")
          [
            ! @"Are design patterns that ease the design of an application by identifying a simple way to implement relationships between entities"
            ! @"Among such pattern we find:"
          ]
        ItemsBlock
          [
            ! @"Adapter pattern"
            ! @"Decorator pattern"
            ! @"Proxy pattern"
            ! @"etc.."
          ]
      ]

    SubSection("Creational patterns")
    VerticalStack
      [
        ItemsBlockWithTitle("Creational patterns")
          [
            ! @"Are design patterns that deal with entities creation mechanisms, trying to create entities in a manner suitable to the situation"
            ! @"They make it possible to have ``virtual'' constructors"
            ! @"Among such pattern we find:"
          ]
        ItemsBlock
          [
            ! @"Factory method pattern"
            ! @"Lazy initialization pattern"
            ! @"Singleton pattern"
            ! @"etc.."
          ]
      ]
    SubSection("Software development principles")
    ItemsBlock
      [
        ! @"Even more abstractly, design patterns are all rooted in the same principles"
        ! @"These principles make it possible to derive old and new patterns"
      ]
    SubSection("Software development principles")
    ItemsBlock
      [
        ! @"Such principles are:"
        ! @"[DRY]: Is an acronym for the design principle ``Don't Repeat Yourself''"
        ! @"[KISS]: Is an acronym for the design principle ``Keep it simple, Stupid!''"
        ! @"[SOLID]: Is an acronym for Single responsibility, Open-closed, Liskov substitution, Interface segregation, and Dependency inversion"
      ]
    SubSection("Software development principles")
    ItemsBlock
      [
        ! @"In this course we will always try, when introducing a design pattern, to present it along with its principles"
      ]
    Section("Iterating collections")
    SubSection("Introduction")
    ItemsBlock
      [
        ! @"Today we are going to study collections"
        ! @"In particular, we are going to study how to access the elements of a collection without exposing its underlying representation (methods and fields)"
        ! @"How? By means of a design pattern: the iterator (a behavioral design pattern)"
        ! @"We will see how the iterator provides a clean, almost trivial, general way representation for iterating collections"
      ]

    SubSection("Different implementations for different collections")
    ItemsBlock
      [
        ! @"Stream of data"
        ! @"Records of a database"
        ! @"List of cars"
        ! @"Array of numbers"
        ! @"Array of Array of pixels (a matrix)"
        ! @"Option (an option essentially is a ``one-element` collections'')"
        ! @"etc.."
      ]
      
    SubSection("What do we do with collections?")
    ItemsBlock
      [
        ! @"However, all collections, from options to arrays, exhibit similarities"
        ! @"The, \textit{general} idea is going through all its elements one by one until there are no more to see"
      ]
    ItemsBlock
      [
        ! @"Unfortunately, every collection has its own different implementation"
        ! @"This is an issue"
        ! @"Why?"
        Pause
        ! @"Because we would have to write specific code for each collection "
      ]
    SubSection("Similar collections, but with different implementation")
    ItemsBlock
      [
        ! @"Take for example a linked list and an array:"
        ! @"The former is a dynamic data structure made of linked nodes. A linked list potentially might contains infinite nodes"
        ! @"The latter is a static compact data structure. In an array the maximum number of elements is fixed"
      ]

    SubSection("Iterating lists")
    VerticalStack
      [
      ItemsBlockWithTitle "Iterating lists"
        [
          ! @"Iterating a list requires a variable that references the current node in the list"
          ! @"To move to the next node we need to manually update such variable, by assigning to it a reference to the next node"
        ]
      CSharpCodeBlock(TextSize.Tiny,
                      (dots >>
                       typedDeclAndInit "list_of_numbers" "LinkedList<int>" (Code.New("LinkedList<int>", [])) >>
                       Code.While(Code.Op(var "list_of_numbers.Tail", Operator.NotEquals, Code.None),
                                  (dots >>
                                   ("list_of_numbers" := var "list_of_numbers.Tail"))) >> dots)) |> Unrepeated
      ]
    SubSection("Iterating array")
    VerticalStack
      [
      ItemsBlockWithTitle ("Iterating array")
        [
          ! @"Iterating an array requires a variable (an index) containing a number representing the position of the current visited element"
          ! @"To move to the next element we need to manually update the variable, increasing it by one."
        ]
      CSharpCodeBlock(TextSize.Tiny,
                      (dots >>
                       arrayDeclAndInit "array_of_numbers" "int" (Code.NewArray("int", 5))  >>
                       typedDecl "index" "int"  >>
                       Code.For((AssignInline("index", (constInt(0)))),  
                                (Code.Op(var "index", Operator.LessOrEquals, var "array_of_numbers.Length")), 
                                (AssignInline ("index", Code.Op(var "index", Operator.Plus, constInt(1)))),
                                dots) >> dots)) |> Unrepeated
      ]

    SubSection("The need for different collections")
    ItemsBlock
      [
        ! @"A collection has its own use: for example arrays are very performant in retrieving data at specific positions, linked lists allow fast insertions, etc.."
        ! @"But then how can we hide the implementation details so that iterating collections becomes trivial if the specifics are not relevant?"
      ]
    SubSection("Issues")
    ItemsBlock
      [
        ! @"Repeating code is problematic (DRY: do not repeat iteration logic)"
        ! @"Knowing too much about a data structure increases coupling make code more complex (KISS: keep iteration simple)"
      ]
    SubSection("Our goal")
    ItemsBlock
      [
        ! @"We try to achieve a mechanism that abstracts our concrete collections from their iteration algorithms"
        ! @"Iteration is a behavior common to all collections: only its implementation changes"
      ]
    SubSection("How do we achieve it?")
    ItemsBlock
      [
        ! @"We wish to delegate the implementation of such algorithms to each concrete collection"
        ! @"We control such algorithms by means of a common/shared interface"
      ]
    SubSection("What follows?")
    ItemsBlock
      [
        ! @"When developers need to iterate a collection they simply use the interface provided by the chosen collection"
        ! @"Such interface hides the internals of a collection and provides a clean interaction surface for iterating it"
      ]
    SubSection("The iterator design pattern")
    ItemsBlock
      [
        ! @"Is a design pattern that captures the iteration mechanism"
        ! @"We will now study it in detail and provide a series of examples"

      ]
    Section("The iterator design pattern")
    SubSection("The iterator design pattern")
    VerticalStack
      [
        ItemsBlockWithTitle("The iterator design pattern")
          [
            ! @"Is an interface \texttt{Iterator<T>} containing the following method signature"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (GenericInterfaceDef (["T"], "Iterator", [typedSig "GetNext" [] "IOption<T>"])) 
                         >> endProgram) |> Unrepeated
      ]
    SubSection("Implementing the Iterator<T>")
    ItemsBlock
      [
        ! @"At this point every collection that wants to provide a disciplined and controlled iteration mechanism has to implement such interface"
      ]
    UML
        [ Package("Iterator", 
                  [
                   Class("Client", 0.0, 3.0, Option.None, [], [])
                   Interface("Iterator<T>",5.0,0.0,0.0,[Operation("GetNext", [], Some "IOption<T>")])
                   Class("Coll1<T>", -6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
                   Class("Coll2<T>", 0.0, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
                   Class("CollN<T>", 6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
                   Aggregation("Client","",Option.None,"Iterator<T>")

                   ])

        ]
    SubSection("Implementing the Iterator<T>")
    ItemsBlock
        [
          ! @"We now show a series of collections implementing such interface"
        ]
    SubSection("Natural numbers")
    VerticalStack
      [
        ItemsBlockWithTitle("Natural numbers")
          [
            ! @"The natural numbers are all integers greater than or equal to 0"
            ! @"We now wish to define a collection containing all natural numbers"
            ! @"To do so we define a data structure that implements our iterator"
            ! @"And starting from -1 (the successor of it is 0, the first natural number), which is stored in a field called \texttt{current}, whenever we call the \texttt{GetNext} method we increase such \texttt{current} and returns its value"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (classDef "NaturalList" 
                          [
                          implements "Iterator<int>"
                          typedDeclAndInit "current" "int" (constInt(-1)) |> makePrivate
                          typedDef "GetNext" [] "IOption<int>" ("current" := (ConstInt(1)) >> 
                                                                (Code.New("Some<int>",[var "current"]) |> ret) >> endProgram)
                          ])) |> Unrepeated          
      ]
    SubSection("Array<T>")
    VerticalStack
      [
        ItemsBlockWithTitle("Array<T>")
          [
            ! @"Dealing with array requires to deal with its indexes"
            ! @"We hide such complexity, which often is error-prone, by means of our iterator"
          ]
      ]
    VerticalStack
      [
        ItemsBlockWithTitle("Array<T>")
          [
            ! @"Our ``new'' array takes as input an object of type array"
            ! @"A field \texttt{index} keeps track of the current index"
            ! @"Whenever the \texttt{GetNext} method is called we check whether we reached the end of the array: if so we return \texttt{None}, otherwise we increase the index and return the value of the array at position \texttt{index} wrapped inside a \texttt{Some} object"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (genericClassDef ["T"]
                          "Array" 
                          [
                          implements "Iterator<T>"
                          typedDecl "array" "T[]" |> makePrivate
                          typedDeclAndInit "index" "int" (constInt(-1)) |> makePrivate
                          typedDef "Array" ["T[]","array"] "" (("this.array" := var"array") >> endProgram) |> makePublic
                          typedDef "GetNext" [] "IOption<int>" (Code.If((Code.Op((Code.Op(var "index" ,Operator.Plus, ConstInt(1) ),Operator.LessThan, var "array.Length")),
                                                                         (Code.New("None<T>",[]) |> ret),
                                                                         (("index" := Code.Op(var "index" ,Operator.Plus, ConstInt(1))) >> 
                                                                          (Code.New("Some<int>",[var "array[index]"]) |> ret)))) >> endProgram)
                          ])) |> Unrepeated          
      ]

    SubSection("The iterator in literature")
    VerticalStack
      [
        ItemsBlockWithTitle("The iterator in literature")
          [
            ! @"In literature it is often the case to see our Iterator as an interface containing the following signatures"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (GenericInterfaceDef (["T"], "UnsafeIterator", [typedSig "MoveNext" [] "void"
                                                                        typedSig "HasNext" [] "bool"
                                                                        typedSig "GetCurrent" [] "T"])) 
                         >> endProgram) |> Unrepeated
        ItemsBlock
          [
            ! @"The main big difference now is that whenever we need to move throughout our collection we have to coordinate \texttt{GetCurrent}, \texttt{HasNext}, and \texttt{MoveNext}"
            ! @"As we can see, this adds a layer of complexity to the iteration and is error prone, since now we have to \textit{carefully} manipulate three methods (instead of one as for \texttt{Iterator<T>})"
            ! @"In what follows we show how to make \texttt{UnsafeIterator} safe!"
          ]
      ]
    SubSection("Improving the UnsafeIterator<T> safeness")
    VerticalStack
      [
        ItemsBlockWithTitle("Improving the UnsafeIterator<T> safeness")
          [
            ! @"Adapting our \texttt{UnsafeIterator} will require us to define an adapter \texttt{AdapterIterator} that implements our \texttt{Iterator}"
            ! @"The \texttt{AdapterIterator} takes as input an \texttt{UnsafeIterator} and whenever the \texttt{GetNext} method is called it calls \texttt{GetCurrent} and \texttt{MoveNext} accordingly"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (genericClassDef ["T"]
                          "AdapterIterator" 
                          [
                            implements "Iterator<T>"
                            typedDecl "iterator" "UnsafeIterator<T>" |> makePrivate
                            typedDef "AdapterIterator" ["UnsafeIterator<T>","iterator"] "" (("this.iterator" := var"iterator") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<int>" (Code.If(MethodCall("iterator","HasNext",[]),
                                                                           ((MethodCall("iterator" , "MoveNext", [])) >>
                                                                            (Code.New("Some<int>",[MethodCall("iterator" , "GetCurrent", [])]) |> ret)),
                                                                           (Code.New("None<T>",[]) |> ret)))])) |> Unrepeated
      ] 
    SubSection("Iterating an IOption<T>")
    VerticalStack
      [
        ItemsBlockWithTitle("Iterating an IOption<T>")
          [
            ! @"We could also make our \texttt{IOption<T>} iterable"
            ! @"To do so, we have to return \texttt{Some} only the first time we it and \texttt{None} for all successive iterations"
            ! @"Note, if we iterate a \texttt{None} entity we return \texttt{None}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (genericClassDef ["T"]
                          "IOptionAdapter" 
                          [
                            implements "Iterator<T>"
                            typedDecl "option" "IOption<T>" |> makePrivate
                            typedDeclAndInit "visited" "bool" (constBool(false)) |> makePrivate
                            typedDef "IOptionAdapter" ["IOption<T>","option"] "" (("this.option" := var"option") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<int>" (Code.If(var ("visited"),
                                                                           (Code.New("None<T>",[]) |> ret),
                                                                           (("visited" := ConstBool(true)) >>
                                                                            (MethodCall("option" , "Visit<IOption<T>>", 
                                                                                       [(Code.GenericLambdaFuncDecl([], Code.New("None<T>", []) |> ret) )
                                                                                        (Code.GenericLambdaFuncDecl(["t"], Code.New("Some<T>", [var "t"]) |> ret) )]) |> ret))))])) |> Unrepeated
      ] 

    SubSection("Conclusions")
    ItemsBlock
      [
        ! @"Iterating collections is a time consuming, error-prone, activity, since collections come with different implementations each with its own complexity"
        ! @"Iterators are a mechanism that hides the complexity of a collection and provides a clean interaction surface to iterate them"
        ! @"This mechanism not only reduces the amount of code to write (achieving then the DRY principle), but also reduces the amount of coupling"
      ]


  ]
