module Chapter1.Week1.v2

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
    Section("Dev 4")
    SubSection("Lecture topics")
    ItemsBlock
      [
        !"Intro to DEV4"
        !"Design patterns introduction"
        !"The visitor design pattern"
        !"Course agenda"
        !"Conclusions"
      ]
    Section("Intro to DEV4")
    SubSection("Exam")
    ItemsBlock
      [
        !"written exam"
        !"3 open questions"
        !"stack/heap, type system, and design patterns"
        !(@"no grade: go (score$\ge$75) or no go (otherwise)")
      ]

    SubSection("Exercises")
    ItemsBlock
      [
        !"exercises to prepare step-by-step"
        !"builds up to actual practicum"
        !"there is no grade for this"
      ]

    SubSection("Assignments")
    ItemsBlock
      [
        !"a connected series of programming tasks"
        !"build a GUI framework"
        !(@"\textbf{mandatory}, but with no direct grade")
      ]

    SubSection("Oral")
    ItemsBlock
      [
        !"the oral is entirely based on the assignments"
        !"we remove some pieces of code from the working solutions and you fill them back in"
        !"the oral gives you the final grade for the course"
      ]

    SubSection("Expected study effort")
    ItemsBlock
      [
        !(@"between 10 and 20 \textbf{net}\footnote{No, 9gag does not count even if the slides are open on another monitor} hours a week")
        !"read every term on the slides and every sample"
        !"if you do not understand it perfectly, either ask a teacher, google, or brainstorm with other students"
        !(@"every sample of code on the slides you should both \textbf{understand} and \textbf{try out} on your machine")
      ]

    SubSection("What you have done so far")
    ItemsBlock
      [
        ! @"Encapsulation, polymorphism, subtyping, generics, etc.;"
        ! @"Ways to express interactions among entities."
      ]

    SubSection("What is coupling?")
    ItemsBlock
      [
        ! @"Interactions between entities affect maintainability;"
        ! @"The more the interactions, the higher the likelihood of having bugs;"
        ! @"This phenomenon is known as coupling."
      ]
    SubSection("What is coupling?")
    ItemsBlock
      [
        ! @"If changing something in a program requires changing something else, then we have coupling."
      ]
    SubSection("Sort of coupling")
    ItemsBlock
      [
        ! @"High, which is undesirable;"
        ! @"Low, which is our target."
      ]
    SubSection("High-coupling")
    VerticalStack
      [
      ItemsBlockWithTitle "High-coupling"
        [
          ! @"As the number of interaction between two classes \textbf{A} and \textbf{B} increases, the coupling between them increases as well;"
          ! @"This translates into: whenever \textbf{A} changes, the likelihood to erroneously change \textbf{B} is ``high'';" 
          ! @"Threfore, likely more bugs."
        ]     
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "High-coupling"
          [
            ! @"The class \texttt{Driver} contains a field of type \texttt{Car}"
            ! @"The class \texttt{Driver} has visibility of all \texttt{Car} public methods and fields, such as the cilinders status;"
            ! @"Move is really the only relevant bit here"
          ]  
        CSharpCodeBlock( TextSize.Tiny,
                      (classDef "Driver" 
                        [
                        typedDecl "car" "Car" |> makePrivate
                        typedDef "Drive" [] "void" (Code.MethodCall("this.car", "Move", []) |> makePublic)
                        ] >>
                       classDef "Car" 
                        [
                        typedDecl "cilinders" "CilindersStatus" |> makePublic
                        typedDef "Move" [] "void" (Code.Dots) |> makePublic
                        dots
                        ])) |> Unrepeated
      ]    
    VerticalStack
      [
      ItemsBlockWithTitle "Low-coupling"
        [
          ! @"The number of interaction between two classes \textbf{A} and \textbf{B} is limited to a series of methods provided by an interface;"
          ! @"This translates into: whenever \textbf{A} changes, the likelihood to erroneously change \textbf{B} is ``low'', since \textbf{A} knows little about \textbf{B}."
        ]
      ]
    VerticalStack
      [
      ItemsBlockWithTitle "Low-coupling"
        [
          ! @"The class \texttt{Driver} contains a polymorphic type \texttt{Vehicle}"
          ! @"The interaction between \texttt{Driver} and \texttt{Car} is restricted to the interface method \texttt{Move};"
          ! @"No cilinders;"
          ! @"Also electric cars (no cilinders in electric cars)."
        ]
      CSharpCodeBlock( TextSize.Tiny,
                      (classDef "Driver" 
                        [
                        typedDecl "vehicle" "Vehicle" |> makePrivate
                        typedDef "Drive" [] "void" (Code.MethodCall("this.vehicle", "Move", []) |> makePublic)
                        ] >>
                       interfaceDef "Vehicle"
                        [
                         typedSig "Move" [] "void"
                        ] >>
                       classDef "Car" 
                        [
                        implements "Vehicle"
                        typedDef "Move" [] "void" (Code.Dots) |> makePublic
                        ])) |> Unrepeated
      ]    
    SubSection("Low vs High coupling")
    ItemsBlock
      [
        ! @"As the number of entities increases, the number of interactions increases;"
        ! @"More precisely, given N classes, it is:
            \begin{equation*}
            I \simeq \left( \sum_{
            i = 2}^{N} \frac{N!}{2(N-i)!} \right)
            \end{equation*}
            "
        ! @"It is a very big number!"
      ]
    ItemsBlock
      [
        ! @"Consider a very simple program with only 4 classes"
        ! @"This number is given by
            \begin{equation*}
            I \simeq \dfrac{4!}{2(4 - 2)!} + \dfrac{4!}{2(4 - 3)!} + \dfrac{4!}{2(4 - 4)!} = 30
            \end{equation*}"
      ]
    

    SubSection("Achieving low-coupling")
    ItemsBlock
      [
        ! @"Maintaining code is hard and expensive"
        ! @"Low coupling results in easily maintainable code"
        ! @"What seems desirable when dealing with software development is to keep coupling between entities as low as possible"
      ]
    SubSection("Maintainability in code")
    ItemsBlock
      [
        ! @"Is an important aspect in development;"
        ! @"It affects costs of fixing bugs and changing functionalities."
      ]
    SubSection("Polymorphism for reducing coupling in programs")
    ItemsBlock
      [
        ! @"We can control interactions by means of an interface that hides the specifics of some classes"
        ! @"Every entity interacts with another only through small ``windows'' (defined as interfaces), each exposing a specific and controlled behavior."
      ]
    SubSection("A general view of low-coupling ")
    ItemsBlock
      [
        ! @"Given two classes \texttt{A} and \texttt{B};"
        ! @"\texttt{A} interacts with an \texttt{I\_B} interface, whenever \texttt{A} needs to interact with an instance of type \texttt{B};"
        ! @"\texttt{B} interacts with an \texttt{I\_A} interface, whenever \texttt{B} needs to interact with an instance of type \texttt{A}."
      ]
    UML
      [ Interface("IA",3.0,-3.0,0.0,[])
        Interface("IB",3.0,3.0,0.0,[])
        Class("A", -3.0, -3.0, Some "IA", [], [])
        Class("B", 3.0, -3.0, Some "IB", [], [])
        Aggregation("A","ib",1,"IB")
        Aggregation("B","ia",1,"IA")
      ]

    VerticalStack
      [
        CSharpCodeBlock( TextSize.Tiny,
                      (classDef "Driver" 
                        [
                        typedDecl "vehicle" "Vehicle" |> makePrivate
                        typedDef "Drive" [] "void" (Code.MethodCall("this.vehicle", "Move", []) |> makePublic)
                        ] >>
                       interfaceDef "Vehicle"
                        [
                         typedSig "Move" [] "void"
                        ] >>
                       classDef "Car" 
                        [
                        implements "Vehicle"
                        typedDecl "engine" "Engine" |> makePrivate
                        typedDef "Move" [] "void" (Code.Dots) |> makePublic
                        ])) |> Unrepeated
        ItemsBlock
          [
            ! @"The driver (class B) can interact with a vehicle (interface IA);"
            ! @"The \texttt{engine}, which should not be accessible outside the car, is not mentioned in the interface, so the driver cannot interact with it."
          ]
      ]
    SubSection("Recurrent patterns in objects interactions")
    ItemsBlock
      [
        ! @"Disciplined interactions such as the one above tend to exhibit some recurring high level structures;"
        ! @"Such structures are known under the umbrella term of \textbf{design patterns}."
      ]

    SubSection("Design Patterns")
    ItemsBlock
      [
        ! @"Design patterns in short are: ways to capture recurring patterns for expressing controlled interactions between entities;"
        ! @"We will now see a specific example of such a pattern."
      ]

    Section("Our first design pattern")
    SubSection("Choosing in the presence of polymorphism")
    ItemsBlock
      [
        ! @"As you already know polymorphism is a powerful mechanism that allows decomposition and code reuse;"
        ! @"Sometimes though, we need to go ``back'' from general instances to concrete ones\footnote{Cat is Animal. Cat is specific. Animal is general.}."
      ]

    
    VerticalStack
      [
        ItemsBlockWithTitle "Why is choosing concrete types so problematic?"
          [
            ! @"Mainly because a general type has no information about what classes are implementing it."
          ]
        CSharpCodeBlock( TextSize.Tiny,
                      (interfaceDef "Vehicle"
                        [
                         typedSig "Move" [] "void"
                        ] >>
                       classDef "Car" [ 
                        implements "Vehicle"
                        dots ] >>
                       classDef "Bike" [ 
                        implements "Vehicle"
                        dots ])) |> Unrepeated
        ItemsBlock
          [
            ! @"Given an instance \texttt{v} of type \texttt{Vehicle}, what can we say about the concrete type of \texttt{v}?"
            ! @"Is it a \texttt{Car} or a \texttt{Bike}?"
            ! @"What if we want to turn on the airco of \texttt{v} if it is a \texttt{Car}?"
          ]
      ]

    SubSection("Safe choice in the presence of polymorphism")
    ItemsBlock
      [
        ! @"We need a mechanism that allows us to manipulate polymorphic instances as if they were concrete;"
        ! @"Concrete instances are the only ones who know their identity, so we allow them to choose from a series of given ``options''."        
      ]
        
    Section("Visiting Option's")
    SubSection("The IOption<T> data structure")
    ItemsBlock
      [
        ! @"Is used when an actual value of type \texttt{T} might or might not be variable;" 
        ! @"It is also called ``reified null'' or ``null object''."
      ]
    SubSection("Examples of usage")
    VerticalStack
      [
        ItemsBlockWithTitle("Examples of usage")
          [
            ! @"The following code illustrates the use of the option type;"
            ! @"In this case we are capturing the number \texttt{5} within a \texttt{Some<int>} object;"
          ]
        CSharpCodeBlock(TextSize.Tiny,(typedDeclAndInit "a_number" "IOption<int>" (Code.New("Some<int>", [constInt(5)])) >> endProgram )) |> Unrepeated
        ItemsBlockWithTitle("Examples of usage")
          [
            ! @"In this case we capture the ``nothing'' common to all values of type \texttt{int} within a \texttt{None<int>} object;"
          ]
        CSharpCodeBlock(TextSize.Tiny,(typedDeclAndInit "another_number" "IOption<int>" (Code.New("None<int>", [])) >> endProgram )) |> Unrepeated
      ]

    SubSection("Some<T> and None<T>")
    VerticalStack
      [
        ItemsBlockWithTitle "Some<T> and None<T>"
          [
            ! @"Both types implement the \texttt{IOption<T>} data structure;"
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        ((genericClassDef ["T"]
                                           "Some"
                                            [implements "IOption<T>"
                                             typedDecl "value" "T" |> makePublic
                                             typedDef "Some" ["T","value"] "" (("this.value" := var"value") >> endProgram) |> makePublic
                                             dots]
                                             ) >> endProgram )) |> Unrepeated
        CSharpCodeBlock(TextSize.Tiny,
                        ((genericClassDef ["T"]
                                           "None"
                                            [implements "IOption<T>"
                                             typedDef "None" [] "" (endProgram) |> makePublic
                                             dots]
                                             ) >> endProgram )) |> Unrepeated
      ]

    SubSection("IOption<T>")
    VerticalStack
      [
      ItemsBlockWithTitle ("IOption<T>")
        [
          ! @"Is an interface that represents both absence and presence of data of type \texttt{T};"
          ! @"We cannot give direct access to the \texttt{T} value here as \texttt{None} could not implement it!"
        ]
      CSharpCodeBlock(TextSize.Tiny,
                        (GenericInterfaceDef (["T"], "Option", [dots])) 
                         >> endProgram) |> Unrepeated
      ]

    Section("Visiting Options without lambdas")
    SubSection("Visiting an IOption<T>")
    VerticalStack
      [
        ItemsBlockWithTitle("Visiting an IOption<T>")
          [            
            ! @"We add a method \texttt{Visit} to the interface that accepts as input a ``Visitor'' (an \texttt{IOptionVisitor<T, U>}) and returns a generic result;"            
            ! @"The visitor object will able to identify the concrete type of the option (\texttt{Some} or \texttt{None}) and manipulate it accordingly\footnote{\textbf{Note}, in many literature this \texttt{Visit} method is generally called \texttt{Accept}}."
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (GenericInterfaceDef (["T"], "Option", [typedSig "Visit<U>" [("IOptionVisitor<T, U>","visitor")] "U"])) 
                         >> endProgram) |> Unrepeated
      ]
    SubSection("The IOptionVisitor<T, U>")
    VerticalStack
      [
        ItemsBlockWithTitle("What is an IOptionVisitor<T, U>?")
          [
            ! @"An interface that provides a series of methods, one for each concrete class;"
            ! @"In our case we have two signatures one for visiting a concrete \texttt{Some} instance and one for the \texttt{None}."
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (GenericInterfaceDef (["T"; "U"], "OptionVisitor", [typedSig "OnSome<U>" [("T","value")] "U"
                                                                            typedSig "OnNone<U>" [] "U"])) 
                         >> endProgram) |> Unrepeated
      ]
    SubSection("A concrete visitor - PrettyPrinterIOptionVisitor<int, string>")
    VerticalStack
      [
        ItemsBlockWithTitle("A concrete visitor - PrettyPrinterIOptionVisitor<int, string>")
          [
            ! @"Provides a pretty printer for options containing integers."
          ]
        CSharpCodeBlock(TextSize.Tiny,
                  ((classDef 
                        "PrettyPrinterOptionVisitor"
                        [implements "IOptionVisitor<int, string>"
                         typedDef "OnSome<string>" [("int","value")] "string" ((Code.MethodCall("value", "ToString", []) |> ret) >> endProgram) |> makePublic
                         typedDef "OnNone<string>" [] "string" ((Code.ConstString("I'm none..") |> ret) >> endProgram) |> makePublic
                         ]
                          ) >> endProgram )) |> Unrepeated

      ]

    SubSection("Visiting a None<T>")
    VerticalStack
      [
      ItemsBlockWithTitle("Visiting a None<T>")
        [
          ! @"When visited, \texttt{None} informs its visitor of its identity by calling on \texttt{onNone}."
        ]
      CSharpCodeBlock(TextSize.Tiny,
                        ((genericClassDef ["T"]
                                          "None"
                                          [implements "IOption<T>"
                                           typedDef "Visit<U>" [("IOptionVisitor<T, U>","visitor")] "U" ((Code.Call("visitor.onNone", []) |> ret) >> endProgram) |> makePublic
                                           ]) >> endProgram )) |> Unrepeated
      ]

    SubSection("Visiting a Some<T>")
    VerticalStack
      [

        ItemsBlockWithTitle "Visiting a Some<T>"
          [
            ! @"When visited, \texttt{Some} informs its visitor of its identity by calling on \texttt{onSome}."
          ]
        CSharpCodeBlock(TextSize.Tiny,
                         ((genericClassDef ["T"]
                                           "Some"
                                            [implements "IOption<T>"
                                             typedDecl "value" "T" |> makePublic
                                             typedDef "Some" ["T","value"] "" (("this.value" := var"value") >> endProgram) |> makePublic
                                             typedDef "Visit<U>" [("IOptionVisitor<T, U>","visitor")] "U" ((Code.Call("visitor.onSome", [var "this.value"]) |> ret) >> endProgram) |> makePublic
                                             ]
                                             ) >> endProgram )) |> Unrepeated
      ]
    SubSection("Testing out our IOption<T>")
    VerticalStack
      [
      ItemsBlockWithTitle ("Testing out our IOption<T>")
        [
          ! @"The next line shows how to use our option to capture numbers and define operations over it;"
          ! @"More precisely we instantiate a \texttt{PrettyPrinterOptionVisitor}, which is then used to visit a \texttt{Some} containing the number 5."
        ]
      CSharpCodeBlock(TextSize.Tiny,
                      (typedDeclAndInit "opt_visitor" "IOptionVisitor<int, int>" (Code.New("PrettyPrinterIOptionVisitor<int, string>", 
                                                                                           [])) >>
                       typedDeclAndInit "number" "IOption<int>" (Code.New("Some<int>", [constInt(5)])) >>
                       Code.MethodCall( "number", "Visit", [(var "opt_visitor")]) >> endProgram)) |> Unrepeated
      ]

    Section("Visiting Options lambdas")
    SubSection("Visiting an IOption<T>")
    VerticalStack
      [
        ItemsBlockWithTitle("Visiting an IOption<T>")
          [
            ! @"Visiting also can be simplified;"
            ! @"We give directly the methods to choose from;"
            ! @"One less interface and trivial classes."
          ]
        CSharpCodeBlock(TextSize.Tiny,
                        (GenericInterfaceDef (["T"], "Option", [typedSig "Visit<U>" [("Func<U>","onNone"); ("Func<T, U>","onSome")] "U"])) 
                         >> endProgram) |> Unrepeated
      ]


    SubSection("Visiting a None<T>")
    VerticalStack
      [
      ItemsBlockWithTitle("Visiting a None<T>")
        [
          ! @"\texttt{None} simply selects \texttt{onNone}."
        ]
      CSharpCodeBlock(TextSize.Tiny,
                        ((genericClassDef ["T"]
                                          "None"
                                          [implements "IOption<T>"
                                           typedDef "Visit<U>" [("Func<U>","onNone"); ("Func<T, U>","onSome")] "U" ((Code.Call("onNone", []) |> ret) >> endProgram) |> makePublic
                                           ]) >> endProgram )) |> Unrepeated
      ]

    SubSection("Visiting a Some<T>")
    VerticalStack
      [
        ItemsBlockWithTitle "Visiting a Some<T>"
          [
            ! @"\texttt{Some} simply selects \texttt{onSome}."
          ]
        CSharpCodeBlock(TextSize.Tiny,
                         ((genericClassDef ["T"]
                                           "Some"
                                            [implements "IOption<T>"
                                             typedDecl "value" "T" |> makePrivate
                                             typedDef "Some" ["T","value"] "" (("this.value" := var"value") >> endProgram) |> makePublic
                                             typedDef "Visit<U>" [("Func<U>","onNone"); ("Func<T, U>","onSome")] "U" ((Code.Call("onSome", [var "value"]) |> ret) >> endProgram) |> makePublic
                                             ]
                                             ) >> endProgram )) |> Unrepeated
      ]
    SubSection("Testing out our IOption<T>")
    VerticalStack
      [
      ItemsBlockWithTitle ("Testing out our IOption<T>")
        [
          ! @"\texttt{String} conversion is now very streamlined."
        ]
      CSharpCodeBlock(TextSize.Tiny,
                      (typedDeclAndInit "number" "IOption<int>" (Code.New("Some<int>", [constInt(5)])) >>
                       typedDeclAndInit "inc_number" "int" (Code.MethodCall("number", "Visit", [Code.GenericLambdaFuncDecl([], Code.ConstString("I am None...") |> ret )
                                                                                                Code.GenericLambdaFuncDecl(["x"], ret (Code.MethodCall("x", "toString",[])))])) >>
                                                                                                endProgram )) |> Unrepeated
      ]

    SubSection("Adapting lambdas in the first previous - LambdaIOptionVisitor<T, U>")
    VerticalStack
      [
        ItemsBlockWithTitle("A concrete visitor - LambdaIOptionVisitor<T, U>")
          [
            ! @"We can adapt the ``non-lambda'' visitor that we say earlier so that it accepts lambda's as well."
          ]
        CSharpCodeBlock(TextSize.Tiny,
                  ((genericClassDef ["T"; "U"]
                                    "LambdaOptionVisitor"
                                    [implements "IOption<T>"
                                     typedDecl "oneSome" "Func<T, U>" |> makePrivate
                                     typedDecl "onNone" "Func<U>" |> makePrivate
                                     typedDef "LambdaOptionVisitor" ["Func<T, U>","onSome";"Func<U>","onNone"] "" (("this.onNone" := var"onNone") >> ("this.onSome" := var"onSome") >> endProgram) |> makePublic
                                     typedDef "onSome<U>" [("T","value")] "U" ((Code.Call("onSome", [var "value"]) |> ret) >> endProgram) |> makePublic
                                     typedDef "onNone<U>" [] "U" ((Code.Call("onNone", []) |> ret) >> endProgram) |> makePublic
                                     ]
                                     ) >> endProgram )) |> Unrepeated

      ]

    SubSection("More sample")
    ItemsBlock
      [
        ! @"Can be found on GIT under the folder: \texbf{Design Patterns Samples CSharp} and also \texttt{Java}."
      ]




    Section("The visitor design pattern")
    SubSection("The general idea")
    ItemsBlock
      [
        ! @"What we have seen so far is an example implementing the \textit{visitor} design pattern;"                
        ! @"It allows the recovery of ``lost-type'' information from a general instance back to specifics;"
        ! @"The recovery is based on the actually activation of one of the multiple concrete options available."
      ]
    SubSection("How do we define it (lambda version)? (Step 1)")
    ItemsBlock
      [
        ! @"Given: $C_1,...,C_n$ classes implementing a common interface $I$;"
        ! @"Every class $C_i$ has fields $f_1^i,..,f_{m_i}^i$"        
      ]
    SubSection("How do we define it (lambda version)? (Step 2)")
    ItemsBlock
      [
        ! @"We now add to $I$ a method \texttt{Visit} that returns a result of type \texttt{U};"
        ! @"\texttt{Visit}, which is the common to all classes implementing $I$, picks the right option based on its concrete shape;"
        ! @"Since we do not know what the visit will result in, then we return a result of a generic type \texttt{U}"
      ]
    ItemsBlock
      [
        ! @"The \texttt{Visit} method accepts as input one function per concrete implementation;"
        ! @"Each such function depends on the fields of the concrete instance and produces a result of type \texttt{U}."
        ! @"[] \begin{lstlisting} 
interface I
  { 
    U Visit<U>(Func<$FieldsC_1$, U> $onC_1$, 
               ..., 
               Func<$FieldsC_N$, U> $onC_N$);
  }
\end{lstlisting}"
      ]
    SubSection("How do we implement it (lambda version)? (Step 3)")
    ItemsBlock
      [
        ! @"Every class implementing the interface \texttt{I} has the task now to implement the \texttt{Visit} method, by selecting and calling the appropriate argument."
        ! @"[] \begin{lstlisting} 
class C1
     : I
  { 
    F_1 f1;
    ...
    F_m fm;
    U Visit<U>(Func<$FieldsC_1$, U> $onC_1$, 
               ..., 
               Func<$FieldsC_N$, U> $onC_N$){
      $onC_1$(f1,..,fm);
    }
  }
\end{lstlisting}"
    ]
    SubSection("How do we use it (lambda version)? (Step 4)")
    ItemsBlock
      [
        ! @"Every time we want to consume an instance of type \texttt{M} we have to \texttt{Visit} it."
        ! @"[] \begin{lstlisting} 
I i = ...;
...
U result =
  m.Visit(
   i_1 => $b_1$,
   ...,
   i_N => $b_n$);
\end{lstlisting}" 
        ! @"Every argument of the visit becomes a function that is triggered depending on the concrete type of \texttt{i};"
        ! @"\texttt{i\_i} are the fields of the concrete instance $C_i$};"
        ! @"\texttt{b\_i} is the block of code to run when a visit on an instance of a concrete type $C_i$ is needed."]

    SubSection("A visual representation")
    UML
      [ Package("Visitor", 
                [Interface("I",10.0,0.0,0.0,[Operation("Visit<U>", [@"onC1 : FieldsC1 $\rightarrow$ U"; "..." ; @"onCN : FieldsCN $\rightarrow$ U"], Some "U")])
                 Class("C1", -3.0, -3.0, Some "M", [Attribute("value", "FieldsC1")], [Operation("Visit<U>", [@"onC1 : FieldsC1 $\rightarrow$ U"; "..."], Some "U")])
                 Class("CN", 3.0, -3.0, Some "M", [Attribute("value", "FieldsCN")], [Operation("Visit<U>", ["..."; @"onCN : FieldsCN $\rightarrow$ U"], Some "U")])
                 ])

      ]
    SubSection("Final considerations")
    ItemsBlock
      [
        ! @"The visitor patterns provides us with a mechanism to safely manipulate polymorphic instances;"
        ! @"This mechanism is transparent and safe, as there always will be an appropriate function to call;"
        ! @"The instance itself is able to select the proper implementation among the input arguments of the visitor method without any complexity or risks."        
      ]

    
    Section("Course structure")
    ItemsBlock
      [
        ! @"Lectures"
        ItemsBlock
          [ 
            ! @"Intro to design patterns - Visiting polymorphic instances (1 lecture) TODAY"
            ! @"Iterating collections - Iterator (1 lecture)"
            ! @"Entities construction and event management - Factory + Observer  (1 lecture)"
            ! @"Building state machines - Strategy (1 lecture)"
            ! @"Extending behaviors - Decorator over Strategy (1 lecture)"
            ! @"Composing behaviours - Adapter over Strategy and input (1 lecture)"
            ! @"Live coding class (1 optional lecture)"
          ]
        ! @"Assignment"
        ItemsBlock
          [ ! @"Build a GUI application containing interactive buttons."]
      ]
    SubSection("Conclusions")
    ItemsBlock
      [
        ! @"Coupling in code is dangerous;"
        ! @"Unmanaged interactions might introduce bugs;"
        ! @"Interfaces are powerful means to control interactions."
      ]
    ItemsBlock
      [
        ! @"Software engineering techniques (called design patterns) have been developed to achieve low-coupling by effectively using interfaces;"
        ! @"This is going to be the topic of this course;"
        ! @"We will study a series of basic design patterns, used in many applications."
      ]
  ]