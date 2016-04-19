module Chapter1.Week1.v2

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
    Section("Introduction")
    SubSection("Lecture topics")
    ItemsBlock
      [
        !"Intro to DEV4"
        !"Design patterns introduction"
        !"The visitor design pattern"
        !"Course agenda"
        !"Conclusions"
      ]

    SubSection("What you have done so far?")
    ItemsBlock
      [
        ! @"Encapsulation, polymorphism, subtyping, generics, etc.;"
        ! @"Powerful ways to express interactions among objects."
      ]

    SubSection("What we have not told you?")
    ItemsBlock
      [
        ! @"Maybe you have already noticed it;"
        ! @"Interactions affect coupling."
      ]
    SubSection("What is coupling?")
    ItemsBlock
      [
        ! @"If changing one module in a program requires changing another module, then we have coupling."
      ]

    SubSection("High-coupling")
    VerticalStack
      [
      ItemsBlockWithTitle "High-coupling"
        [
          ! @"As the interaction surface between two classes \textbf{A} and \textbf{B} increases, the coupling between them increases as well;"
          ! @"This translates into: whenever \textbf{A} changes the chance to erroneously change \textbf{B} is ``high'';" 
          ! @"Thus, the amount of bugs."

        ]
      CSharpCodeBlock( TextSize.Tiny,
                      (classDef "Driver" 
                        [
                        typedDecl "car" "Car" |> makePrivate
                        typedDef "Drive" [] "void" (Code.MethodCall("this.car", "Move", []) |> makePublic)
                        ] >>
                       classDef "Car" 
                        [
                        typedDef "Move" [] "void" (Code.Dots) |> makePublic
                        ])) |> Unrepeated
      ]    
    VerticalStack
      [
      ItemsBlockWithTitle "Low-coupling"
        [
          ! @"The interaction surface between two classes \textbf{A} and \textbf{B} is limited to a series of methods provided by an interface;"
          ! @"This translates into: whenever \textbf{A} changes the chance to erroneously change \textbf{B} is ``low'', since \textbf{A} know little about \textbf{B}."
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
        ! @"As the amount of entities increase, the of amount interactions increases (especially if the interfaces are not clear or not used at all);"
        ! @"How much?"
        ! @"It is a very big number (we are talking about an exponential function) depending on the amount of interacting objects;"
        ! @"More precisely, given C classes, it is:
\begin{equation*}
O\left(\sum_{\substack{
1<k \leq C
}} \frac{C!}{2(C-k)!}\right)
\end{equation*}
"
      ]
    SubSection("Finding the right amount of coupling")
    ItemsBlock
      [
        ! @"One could argue that: to avoid coupling we can put everything in one big class;"
        ! @"Unfortunately this is completely true, since we can have coupling also within a single class."
      ]

    SubSection("Achieving low-coupling")
    ItemsBlock
      [
        ! @"What seems desirable when dealing with software development is to keep coupling (our interactions) among entities as low as possible;"
        ! @"Why?"
        ! @"To mainly keep code maintainable."        
      ]
    SubSection("Maintainability in code")
    ItemsBlock
      [
        ! @"Is an important aspect in development;"
        ! @"It affects costs, code customization, bug fixing, etc."
      ]
    SubSection("Achieving low-coupling")
    ItemsBlock
      [
        ! @"How how can we reduce the interaction surface among objects??"
        ! @"We can use polymorphism, as seen in the last example, as a tool for specifying interaction surfaces."
      ]
    SubSection("Low-coupling a general view")
    ItemsBlock
      [
        ! @"Given two classes \texttt{A} and \texttt{B};"
        ! @"\texttt{A} interacts with an \texttt{I\_B} interface, whenever \texttt{A} needs to interact with an instance of type \texttt{B};"
        ! @"\texttt{B} interacts with an \texttt{I\_A} interface, whenever \texttt{B} needs to interact with an instance of type \texttt{A}."
        ! @"UML MISSING"
      ]
    SubSection("Polymorphism for taming coupling in programs")
    ItemsBlock
      [
        ! @"We can now control interactions by means of an interface that hides the specifics of some classes;"
        ! @"Now every entity interacts with another only through small ``windows'' (defined as interfaces) each exposing specific and controlled behavior."
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
            ! @"The driver can yes interact with a vehicle, but only with its public \texttt{Move} method;"
            ! @"The \texttt{engine}, which should not be accessible outside the car, is not mentioned in the interface, so the driver cannot interact with it."
          ]
      ]
    SubSection("Recurrent patterns in objects interactions")
    ItemsBlock
      [
        ! @"Disciplined interactions such as the one above tend to exhibit some recurring high level strutures;"
        ! @"Such recurrent structures are known under the umbrella term of \textbf{design patterns}."
      ]

    SubSection("Design Patterns")
    ItemsBlock
      [
        ! @"Design patterns in short are: ways to capture recurrent patterns for expressing controlled interactions between objects;"
        ! @"We will now see a specific example of such a pattern."
      ]

    Section("Our first design pattern")
    SubSection("Choosing in the presence of polymorphism")
    ItemsBlock
      [
        ! @"As you already know polymorphism is a powerful mechanism that allows decomposition and code reuse;"
        ! @"However, polymorphism becomes dangerous when given a general\footnote{Cat is Animal. Cat is specific. Animal is general.} instance we have to choose what its specific shape is."
      ]

    
    VerticalStack
      [
        ItemsBlockWithTitle "Why is choosing concrete types so dangerous?"
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
            ! @"What if we want to turn on the lights of the car of \texttt{v}?"
          ]
      ]

    SubSection("Safe choice in the presence of polymorphism")
    ItemsBlock
      [
        ! @"We need a mechanism that allows us to manipulate polymorphic instances as id they were concrete;"
        ! @"Concrete instances are the only ones who know their identity, so we allow them to choose from a series of given ``options''."        
      ]

    Section("Visiting Option's without lambda's")
    ItemsBlock
      [
        ! @"UML + Examplanation"
      ]
    Section("Visiting Option's with lambda's")
    ItemsBlock
      [
        ! @"UML + Examplanation"
      ]
    Section("The visitor design pattern")
    SubSection("The general idea")
    ItemsBlock
      [
        ! @"What we have seen so far are all examples implementing the \textit{visitor} design pattern;"                
        ! @"It allows the recovery of ``lost-type'' information from a general instance back to specifics;"
        ! @"The recovery is based on the actualy activation of one of the multiple ``options'';"
        ! @"The options can be instances of some concrete visitor interface, or (more elegantly) lambda's;"
        ! @"We will for now on focus on the lambda implementation."
      ]
    SubSection("How do we define it (lambda version)? (Step 1)")
    ItemsBlock
      [
        ! @"Given: $C_1,...,C_n$ classes implementing a common interface $I$;"
        ! @"Every class $C_i$ has fields $f_i^1,..,f_i^{m_i}$"        
      ]
    SubSection("How do we define it (lambda version)? (Step 2)")
    ItemsBlock
      [
        ! @"We now add to $I$ a method \texttt{Visit} that returns an result of type \texttt{U};"
        ! @"\texttt{Visit}, which is  method common to all classes implementing $I$, picks the right option based on its concrete shape;"
        ! @"And since we do not know the visit result it returns a result of type generic s\texttt{U}"
      ]
    ItemsBlock
      [
        ! @"The \texttt{Visit} method accepts as input arguments as many as the possible concrete classes;"
        ! @"Every argument is a function that depends on the fields of the concrete instance and produces a result of type \texttt{U}."
        ! @"[] \begin{lstlisting} 
interface I<Input_1, Input_2, ..., Input_N>
  { 
    U Visit<U>(Func<Input_1, U> onObj1, 
               ..., 
               Func<Input_N, U> onObjN);
  }
\end{lstlisting}"
      ]
    SubSection("How do we implement it (lambda version)? (Step 3)")
    ItemsBlock
      [
        ! @"Every class implementing the interface \texttt{M} has the task now to implement the \texttt{Visit} method, by selecting and calling the appropriate argument."
        ! @"[] \begin{lstlisting} 
class C1<Input_1, Input_2, ..., Input_N> 
     : I<Input_1, Input_2, ..., Input_N>
  { 
    Input_1 value;
    U Visit<U>(Func<Input_1, U> onObj1, 
               ..., 
               Func<Input_N, U> onObjN){
      onObj1(this.value);
    }
  }
\end{lstlisting}"
    ]
    SubSection("How do we use it (lambda version)? (Step 4)")
    ItemsBlock
      [
        ! @"Every time we want to consume an instance of type \texttt{M} we have to \texttt{Visit} it."
        ! @"[] \begin{lstlisting} 
I<Input_1, Input_2, ..., Input_N> i;
...
m.Visit(
 i_1 => $b_1$,
 ...,
 i_N => $b_n$);
\end{lstlisting}" 
        ! @"Every argment of the visit becomes a function that is triggered depending on the concrete type of \texttt{i};"
        ! @"\texttt{i_i} are the fields of a concrete class $C_i$};
        ! @""\texttt{b_i} is the block of to run when a visit on an instance of type $C_i$ is needed."]

    SubSection("A visual representation")
    UML
      [ Package("Visitor", 
                [Interface("M<Obj\_1, Obj\_N>",10.0,0.0,0.0,[Operation("Visit<U>", [@"onObj1 : Obj1 $\rightarrow$ U"; @"onObjN : ObjN $\rightarrow$ U"], Some "U")])
                 Class("Obj1", -3.0, -3.0, Some "M<Obj\_1, Obj\_N>", [Attribute("value", "Obj\_1")], [Operation("Visit<U>", [@"onObj1 : Obj1 $\rightarrow$ U"; "..."], Some "U")])
                 Class("ObjN", 3.0, -3.0, Some "M<Obj\_1, Obj\_N>", [Attribute("value", "Obj\_N")], [Operation("Visit<U>", ["..."; @"onObjN : ObjN $\rightarrow$ U"], Some "U")])])
        Class("Program", 5.5, -7.5, Option.None, [], [Operation("main", [], Option.None)])
        Arrow("Program", "Consumes", "Visitor")
      ]
    SubSection("Final considerations")
    ItemsBlock
      [
        ! @"The visitor patterns provides us with a mechanism to safely manipulate polymorphic instances;"
        ! @"From the interface point of view: this mechanism is transparent and safe, as there always will be an appropriate function to call;"
        ! @"From the concrete class point of view: the instance iself is able to select the proper implementation among the input arguments of the visitor method without any complexity or risks."        
      ]

    
    Section("Course structure")
    ItemsBlock
      [
        ! @"Lectures"
        ItemsBlock
          [ 
            ! @"Intro to design patterns (1 lecture) TODAY"
            ! @"Entities construction - Factory  (1 lecture)"
            ! @"Generalizing behaviors - Adapter (1 lecture)"
            ! @"Extending/Composing behaviors - Decorator (1 lecture)"
            ! @"Composing patterns - MVC, MVVM (1 lecture)"
            ! @"Live coding class (1 lecture)"
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
        ! @"This is going to be the topic for this course;"
        ! @"We will study a series of basic design patterns, used in many applications."
      ]
  ]