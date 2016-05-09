module Chapter2.Week2.v1

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
        ! @"A taxonomy of design patterns"
        ! @"Iterating collections"
        ! @"Concrete examples of the iterator design pattern"
        ! @"Conclusions"
      ]
    Section("A taxonomy of design patterns")
    VerticalStack
      [
      ItemsBlockWithTitle("A taxonomy of design patterns")
        [
          ! @"After having seen the first design pattern, we can add some depth to the discussion"
          ! @"Design patterns have been grouped in several specific categories (we will show at least one design pattern per category):"
        ]
      ItemsBlock
        [
          ! @"Behavioral"
          ! @"Structural"
          ! @"Creational"
        ]
      ]
    SubSection("Behavioral patterns")
    VerticalStack
      [
        ItemsBlockWithTitle("Behavioral patterns")
          [
            ! @"Design patterns for identifying the fundamental communication behavior between entities"
            ! @"Among such patterns we find:"
          ]
        ItemsBlock
          [
            ! @"\textbf{Visitor pattern}"
            ! @"State pattern"
            ! @"Strategy pattern"
            ! @"\textbf{Null Object pattern}"
            ! @"\underline{Iterator pattern}"
            ! @"etc.."
          ]

          
      ]
    SubSection("Structural patterns")
    VerticalStack
      [
        ItemsBlockWithTitle("Structural patterns")
          [
            ! @"Design patterns that ease the design of an application by identifying a simple way to implement relationships between entities"
            ! @"Among such patterns we find:"
          ]
        ItemsBlock
          [
            ! @"\underline{Adapter pattern}"
            ! @"\underline{Decorator pattern}"
            ! @"Proxy pattern"
            ! @"etc.."
          ]
      ]

    SubSection("Creational patterns")
    VerticalStack
      [
        ItemsBlockWithTitle("Creational patterns")
          [
            ! @"Design patterns that deal with entities creation mechanisms, trying to create entities in a manner suitable to the situation"
            ! @"They make it possible to have ``polymorphic'' constructors"
            ! @"Among such patterns we find:"
          ]
        ItemsBlock
          [
            ! @"\underline{Factory method pattern}"
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
        ! @"THey are refinements of the broader principles of encapsulation and loose coupling"
      ]
    SubSection("Software development principles")
    ItemsBlock
      [
        ! @"Such principles are:"
        ! @"[DRY]: Is an acronym for the design principle ``Don't Repeat Yourself''"
        ! @"[KISS]: Is an acronym for the design principle ``Keep it simple, Stupid!''"
        ! @"[SOLID]: Is an acronym for Single responsibility, Open-closed, Liskov substitution, Interface segregation, and Dependency inversion"
      ]
    SubSection("DRY")
    ItemsBlock
      [
        ! @"Every piece of knowledge must have a single, unambiguous, authoritative representation within a system"
        ! @"Violations of DRY are typically referred to as \textit{WET}(write everything twice) solutions"
      ]
    SubSection("KISS")
    ItemsBlock
      [
        ! @"It states that most systems work best if they are kept simple rather than made complicated"
        ! @"Simplicity should be a key goal in design and unnecessary complexity should be avoided"
        ! @"See $\lambda$-calculus / stack & heap: complex system from single rules"
      ]
    SubSection("SOLID")
    ItemsBlock
      [
        ! @"[S]: a class should have only a single responsibility"
        ! @"[O]: entities should be ``open'' for extensions, but ``closed'' for modification"
        ! @"[L]: objects in a program should be replaceable with instances of their subtypes without altering the correctness of the program"
        ! @"[I]: many ``specific'' interfaces are better than one general-purpose interface"
        ! @"[D]: high-level modules should not dependent from the low-levels; both should depend on abstractions. Abstractions should not depend on details and vice-versa"
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
      ]

    SubSection("Different implementations for different collections")
    ItemsBlock
      [
        ! @"Stream of data"
        ! @"Records of a database"
        ! @"List of cars"
        ! @"Array of numbers"
        ! @"Array of Array of pixels (a matrix)"
        ! @"Option (zero or one elements"
        ! @"etc.."
      ]
      
    SubSection("What do we do with collections?")
    ItemsBlock
      [
        ! @"However, all collections, from options to arrays, exhibit similarities"
        ! @"The \textit{general} idea is going through all the elements one by one until there are no more to see"
      ]
    ItemsBlock
      [
        ! @"Unfortunately, every collection has its own different implementation"
        ! @"This is an issue"
        ! @"Why?"
        Pause
        ! @"\textbf{Because we would have to write specific access/iteration code for each collection}"
      ]
    SubSection("Similar collections, but with different implementation")
    ItemsBlock
      [
        ! @"Take for example a linked list and an array:"
        ! @"The former is a dynamic data structure made of linked nodes"
        ! @"The latter is a static compact data structure with a fixed number of elements"
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
                      (genericTypedDeclAndInit ["int"] "list_of_numbers" "LinkedList" (Code.GenericNew("LinkedList", ["int"], [])) >>
                       dots >>
                       Code.While(Code.Op(var "list_of_numbers.Tail", Operator.NotEquals, Code.None),
                                  (dots >>
                                   ("list_of_numbers" := var "list_of_numbers.Tail"))) >> dots)) 
      ]
    SubSection("Iterating array")
    VerticalStack
      [
      ItemsBlockWithTitle ("Iterating array")
        [
          ! @"Iterating an array requires a variable (an index) containing a number representing the position of the current visited element"
          ! @"To move to the next element we need to manually update the index, increasing it by one"
        ]
      CSharpCodeBlock(TextSize.Tiny,
                      (arrayDeclAndInit "array_of_numbers" "int" (Code.NewArray("int", 5))  >>
                       dots >>
                       typedDecl "index" "int"  >>
                       Code.For((AssignInline("index", (constInt(0)))),  
                                (Code.Op(var "index", Operator.LessOrEquals, var "array_of_numbers.Length")), 
                                (AssignInline ("index", Code.Op(var "index", Operator.Plus, constInt(1)))),
                                dots) >> dots)) 
      Pause
      ItemsBlock
        [
          ! @"What about all other collections?"
          ! @"Maps, sets, trees, etc.."
        ]
      ]
    SubSection("The need for different collections")
    ItemsBlock
      [
        ! @"A collection has its own purpose: for example arrays are very performant in retrieving data at specific positions, linked lists allow fast insertions, etc.."
        ! @"But then how can we hide the implementation details so that iterating collections becomes trivial if the specifics are not relevant?"
      ]
    SubSection("Issues")
    ItemsBlock
      [
        ! @"Repeating code is problematic (DRY: do not repeat iteration logic)"
        ! @"Knowing too much about a data structure increases coupling, making code more complex (KISS: keep iteration superficially simple)"
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
    VerticalStack
      [
        ItemsBlockWithTitle("The iterator design pattern")
          [
            ! @"We will define an interface capturing the basics to all container iterations:"
          ]
        ItemsBlock
          [
            ! @"get current item"
            ! @"move to next item"
            ! @"check if next item exists"
          ]
      ]
    SubSection("The iterator design pattern")
    VerticalStack
      [
        ItemsBlockWithTitle("The iterator design pattern")
          [
            ! @"Is an interface \texttt{Iterator<T>} containing the following method signature"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (GenericInterfaceDef (["T"], "Iterator", [typedSig "GetNext" [] "IOption<T>"])) 
                         >> endProgram) 
        ItemsBlock
          [
            ! @"\texttt{GetNext} returns \texttt{Some<T>} if there is an item to fetch"
            ! @"It moves to the next item"
            ! @"It returns \texttt{None<T>} if there are no more elements to fetch"
          ]
      ]
    SubSection("Implementing the Iterator<T>")
    ItemsBlock
      [
        ! @"At this point every collection that wants to provide a disciplined and controlled iteration mechanism has to either implements such interface or provide a way to adapt to it"
        ! @"Iterating a collection with 5, 3, 2 will return: Some(5), Some(3), Some(2), None(), None(), None(), ...., None()"
      ]
    UML
        [ Package("Iterator", 
                  [
                   Class("Client", 0.0, 3.0, Option.None, [], [])
                   Interface("Iterator<T>",5.0,0.0,0.0,[Operation("GetNext", [], Some "IOption<T>")])
                   Class("List<T>", -6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
                   Class("Array<T>", 0.0, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
                   Class("Stack<T>", 6.5, -3.0, Some "Iterator<T>", [], [Operation("GetNext", [], Some "IOption<T>")])
                   Aggregation("Client","",Option.None,"Iterator<T>")

                   ])

        ]
    SubSection("Implementing the Iterator<T>")
    ItemsBlock
        [
          ! @"We now show a series of collections implementing such an interface"
        ]
    SubSection("Natural numbers")
    VerticalStack
      [
        ItemsBlockWithTitle("Natural numbers")
          [
            ! @"The natural numbers are all integers greater than or equal to 0"
            ! @"\texttt{GetNext} increases a counter \texttt{n} (starting from -1) and returns it within a \texttt{Some}"            
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (classDef "NaturalList" 
                          [
                          implements "Iterator<int>"
                          typedDeclAndInit "current" "int" (constInt(-1)) |> makePrivate
                          typedDef "GetNext" [] "IOption<int>" (("current" := Code.Op(var "current", Operator.Plus, (ConstInt(1)))) >> 
                                                                    (Code.New("Some<int>",[var "current"]) |> ret))
                          ])) |> Unrepeated      
      ]
    SubSection("IterableList<T>")
    VerticalStack
      [
        ItemsBlockWithTitle("IterableList<T>")
          [
            ! @"Dealing with a list requires to deal with references"
            ! @"We hide such complexity, which is error-prone, by means of our iterator"
            ! @"We use the unsafe version of the list with \texttt{IsNone}, \texttt{GetValue}, and \texttt{GetTail} for simplicity; a visit would be better "
          ]
      ]
    VerticalStack
      [
        ItemsBlockWithTitle("IterableList<T>")
          [
            ! @"Our iterable list now takes as input an object of type list"
            ! @"\texttt{GetNext} returns \texttt{None} at the end of the list (when the tail is \texttt{None}), otherwise it moves to the next node and returns its value wrapped inside a \texttt{Some}"

          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (genericClassDef ["T"]
                          "IterableList" 
                          [
                          implements "Iterator<T>"
                          typedDecl "list" "List<T>" |> makePrivate
                          typedDef "IterableList" ["List<T>","list"] "" (("this.list" := var"list") >> endProgram) |> makePublic
                          typedDef "GetNext" [] "IOption<T>" (Code.If(Code.MethodCallInline("list","IsNone", []),
                                                                        (Code.New("None<T>",[]) |> ret),
                                                                        ((typedDeclAndInit "tmp" "List<T>" (var "list")) >>
                                                                         ("list" := (MethodCall("list", "GetTail", []))) >>                                                                         
                                                                         (Code.New("Some<T>",[MethodCall("tmp", "GetValue", [])]) |> ret))) >> endProgram)
                          ]))           
      ]
    SubSection("IterableArray<T>")
    VerticalStack
      [
        ItemsBlockWithTitle("IterableArray<T>")
          [
            ! @"Dealing with an array requires to deal with its indexes"
            ! @"We hide such complexity, which is error-prone, by means of our iterator"
          ]
      ]
    VerticalStack
      [
        ItemsBlockWithTitle("IterableArray<T>")
          [
            ! @"Our iterable array takes as input an object of type array"
            ! @"\texttt{GetNext} returns \texttt{None} at the end of the array, otherwise it increases the index and returns the value of the array at position \texttt{index} wrapped inside a \texttt{Some}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (genericClassDef ["T"]
                          "IterableArray" 
                          [
                          implements "Iterator<T>"
                          typedDecl "array" "T[]" |> makePrivate
                          typedDeclAndInit "index" "int" (constInt(-1)) |> makePrivate
                          typedDef "IterableArray" ["T[]","array"] "" (("this.array" := var"array") >> endProgram) |> makePublic
                          typedDef "GetNext" [] "IOption<T>" (Code.If((Code.Op((Code.Op(var "index" ,Operator.Plus, ConstInt(1) ),Operator.GreaterOrEquals, var "array.Length")),
                                                                         (Code.New("None<T>",[]) |> ret),
                                                                         (("index" := Code.Op(var "index" ,Operator.Plus, ConstInt(1))) >> 
                                                                          (Code.New("Some<T>",[var "array[index]"]) |> ret)))) >> endProgram)
                          ])) |> Unrepeated
      ]

    SubSection("Other collections")
    ItemsBlock
      [
        ! @"Each container will then store its own reference to a collection, plus the current iterated element"
        ! @"The plumbing is trivial per container"
        ! @"We obviously cannot show them all"
      ]
    SubSection("The iterator in literature")
    VerticalStack
      [
        ItemsBlockWithTitle("The iterator in literature")
          [
            ! @"In the literature we often find another formulation"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (GenericInterfaceDef (["T"], "TraditionalIterator", [typedSig "MoveNext" [] "void"
                                                                             typedSig "HasNext" [] "bool"
                                                                             typedSig "GetCurrent" [] "T"])) 
                         >> endProgram) 
        ItemsBlock
          [
            ! @"As we can see, this is less safe, since now we have to \textit{carefully} manipulate three methods (instead of one as for \texttt{Iterator<T>})"
          ]
      ]
    SubSection("Improving the TraditionalIterator<T> safeness")
    VerticalStack
      [
        ItemsBlockWithTitle("Improving the TraditionalIterator<T> safeness")
          [
            ! @"Adapting our \texttt{TraditionalIterator} will require us to define an adapter \texttt{MakeSafe} that implements our \texttt{Iterator} by coordinating method calls to an underlying \texttt{Iterator}\footnote{Adapter allows to automatically convert back/forth between iterators}"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (genericClassDef ["T"]
                          "MakeSafe" 
                          [
                            implements "Iterator<T>"
                            typedDecl "iterator" "TraditionalIterator<T>" |> makePrivate
                            typedDef "MakeSafe" ["TraditionalIterator<T>","iterator"] "" (("this.iterator" := var"iterator") >> endProgram) |> makePublic
                            typedDef "GetNext" [] "IOption<T>" (Code.If(MethodCallInline("iterator","HasNext",[]),
                                                                           ((MethodCall("iterator" , "MoveNext", [])) >>
                                                                            (Code.New("Some<T>",[MethodCall("iterator" , "GetCurrent", [])]) |> ret)),
                                                                            (Code.New("None<T>",[]) |> ret)))])) 
      ]
    
    Section("Conclusions")
    SubSection("Conclusions")
    ItemsBlock
      [
        ! @"Iterating collections is a time consuming, error-prone activity, since collections come with different implementations each with its own complexity"
        ! @"Iterators are a mechanism that hides the complexity of a collection and provides a clean interaction surface to iterate them"
        ! @"This mechanism not only reduces the amount of code to write (achieving then the DRY principle), but also reduces the amount of coupling"
      ]
  ]