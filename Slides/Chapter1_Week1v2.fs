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
        ! @"Interactions between program modules affect maintainability"
        ! @"The higher the interactions, the higher is the chance of having bugs"
        ! @"This phenomenon is knowkn as coupling"
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
          ! @"As the amount of interaction between two classes \textbf{A} and \textbf{B} increases, the coupling between them increases as well;"
          ! @"This translates into: whenever \textbf{A} changes, the chance to erroneously change \textbf{B} is ``high'';" 
          ! @"More bugs"
        ]     
      ]
    VerticalStack
      [
        ItemsBlockWithTitle "High-coupling"
          [
            ! @"The class \texttt{Driver} contains a field of type \texttt{Car}"
            ! @"The class \texttt{Driver} has visibility of all \texttt{Car} public methods and fields"
            ! @"The interaction between \texttt{Driver} and \texttt{Car} should be limited to the \texttt{Move} method"
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
          ! @"The amount of interaction between two classes \textbf{A} and \textbf{B} is limited to a series of methods provided by an interface;"
          ! @"This translates into: whenever \textbf{A} changes, the chance to erroneously change \textbf{B} is ``low'', since \textbf{A} know little about \textbf{B}."
        ]
      ]
    VerticalStack
      [
      ItemsBlockWithTitle "Low-coupling"
        [
          ! @"The class \texttt{Driver} contains a polymorphic type \texttt{Vehicle}"
          ! @"The interaction between \texttt{Driver} and \texttt{Car} is restricted to the interface method \texttt{Move}"
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
        ! @"As the amount of entities increases, the of amount of interactions increases (especially if the interfaces are not clear or not used at all);"
        ! @"It is a very big number (we are talking about a factorial function) depending on the amount of interacting objects"
        ! @"More precisely, given C classes, it is:
            \begin{equation*}
            I \sim \left( \sum_{
            k = 2}^{C} \frac{C!}{2(C-k)!} \right)
            \end{equation*}
            "
      ]
    ItemsBlock
      [
        ! @"Consider a very simple program with only 4 classes"
        ! @"This amount is given by
            \begin{equation*}
            I \sim \dfrac{4!}{2(4 - 2)!} + \dfrac{4!}{2(4 - 3)!} + \dfrac{4!}{2(4 - 4)!} = 30
            \end{equation*}"
      ]
    SubSection("Finding the right amount of coupling")
    VerticalStack
      [
        ItemsBlock
          [
            ! @"One could argue that: to avoid coupling we can put everything in one big class;"
            ! @"Unfortunately this does not solve the problem, since we can have coupling also within a single class."
            ! @"Parts of the class \texttt{Driver} still have complete visibility on the rest of the class"
          ]
        CSharpCodeBlock( TextSize.Tiny,
                        (classDef "Driver" 
                          [
                            typedDecl "vehicle" "Vehicle" |> makePrivate
                            typedDef "Drive" [] "void" (Code.MethodCall("this.vehicle", "Move", []) |> makePublic)
                            typedDef "Move" [] "void" (Code.Dots) |> makePublic
                          ])) |> Unrepeated
      ]

    SubSection("Achieving low-coupling")
    ItemsBlock
      [
        ! @"Maintaining code is hard and expensive"
        ! @"Low coupling = easily maintainable code"
        ! @"What seems desirable when dealing with software development is to keep coupling (our interactions) among entities as low as possible"
      ]
    SubSection("Maintainability in code")
    ItemsBlock
      [
        ! @"Is an important aspect in development;"
        ! @"It affects costs, code customization, bug fixing, etc."
        ! @"Maintainable code = low chance of bugs and smaller effort in making changes"  
      ]
    SubSection("Polymorphism for taming coupling in programs")
    ItemsBlock
      [
        ! @"We can control interactions by means of an interface that hides the specifics of some classes"
        ! @"Every entity interacts with another only through small ``windows'' (defined as interfaces), each exposing specific and controlled behavior."
      ]
    SubSection("Low-coupling a general view")
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
        ! @"We need a mechanism that allows us to manipulate polymorphic instances as if they were concrete;"
        ! @"Concrete instances are the only ones who know their identity, so we allow them to choose from a series of given ``options''."        
      ]
        
    Section("Visiting Option's")
    SubSection("The Option data structure")
    ItemsBlock
      [
        ! @"Is used when an actual value might not exist for a named value or variable;" 
        ! @"An option has an underlying type and can hold a value of that type, or it might not have a value."
      ]
    SubSection("Example of usage")
    ItemsBlock
      [
        ! @"The following code illustrates the use of the option type;"
        ! @"In this case we are capturing the number \texttt{5} within a \texttt{Some<int>} object;"
        ! @"[] \begin{lstlisting}
Option<int> a_number = new Some<int>(5);
\end{lstlisting}"   
        ! @"In this case we captring the ``nothing'' common to all values of type \texttt{int} withing a \texttt{None<int>} object;"
        ! @"[] \begin{lstlisting}
Option<int> another_number = new None<int>();
\end{lstlisting}" 
      ]

    SubSection("Some<T> and None<T>")
    ItemsBlock
      [
        ! @"Both types implement the \texttt{Option<T>} data structure;"
        ! @"[] \begin{lstlisting}
class Some<T> : Option<T> { ... }
\end{lstlisting}" 
        ! @"[] \begin{lstlisting}
class None<T> : Option<T> { ... }
\end{lstlisting}" 
        ! @"\texttt{Some<T>} is a container of data, of type \texttt{T}, which is ready to get consumed; and"
        ! @"\texttt{None<T>} is a container of data, of type \texttt{T}, which is not ready to get consumed yet."
      ]

    SubSection("Option<T>")
    ItemsBlock
      [
        ! @"Is an interface that represents both the absence and presence of data of type \texttt{T}"
        ! @"[] \begin{lstlisting}
interface Option<T> { ... }
\end{lstlisting}" 
      ]

    SubSection("Visiting an Option<T>")
    ItemsBlock
      [
        ! @"As option represents a generic container for any type of objects, we need a mechanism that allows us to manipulate its content regardless its concrete data type;"
        ! @"We add a method to our interface called \texttt{Visit} that accepts as inputs a series of options (in the shape of lambdas) and a generic result;"
        ! @"Each option will be selected by exactly one of the possible concrete types;"
        ! @"We decided a propri that the first argument is meant for the class \texttt{None<T>} while the second one for the \texttt{Some<T>}"
        ! @"[] \begin{lstlisting}
interface Option<T>
{
  U Visit<U>(Func<U> onNone, Func<T, U> onSome);
}
\end{lstlisting}" 
      ]

    SubSection("Visiting a None<T>")
    ItemsBlock
      [
        ! @"When visiting an object of type \texttt{None<T>} we first select the input reserved for it then we return the result of its call;"
        ! @"[] \begin{lstlisting}
public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
{
  return onNone();
}
\end{lstlisting}" 
      ]

    SubSection("Visiting a Some<T>")
    ItemsBlock
      [
        ! @"When instantiating a \texttt{Some<T>} a data of type \texttt{T} is passed and stored inside a field \texttt{value};"
        ! @"When visiting an object of type \texttt{Some<T>} we first select the input reserved for it then we return the result of its call with \texttt{value} given as input;"
        ! @"We pass \texttt{value} to the lambda, since it might be transformed/consumed by it;"        
        ! @"[] \begin{lstlisting}
class Some<T> : Option<T>
{
  T value;
  public Some(T value) { this.value = value; }
  public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
  {
    return onSome(value);
  }
}
\end{lstlisting}" 
      ]
    SubSection("Testing out our Option<T>")
    ItemsBlock
      [
        ! @"The next line shows how to use our option to capture numbers and define operations over it;"
        ! @"More precisely we define a \texttt{Some} containing the number 5 with the following operations: \begin{itemize} \item The first lambda runs an exception, since we are trying to read a data that is not ready (None represents a \texttt{null} object); \item The second lambda gets as input the \texttt{value} stored into \texttt{Some} and increments it by 1. \end{itemize} "
        ! @"[] \begin{lstlisting}
Option<int> number = new Some<int>(5);
int inc_number = number.Visit(() => { throw new Exception(\""Expexting a value..\""); }, i => i + 1);
\end{lstlisting}" 
      ]
    SubSection("Testing out our Option<T>")
    ItemsBlock
      [
        ! @"The next line shows an example with a \texttt{None} object;"
        ! @"Visiting such object will indeed cause an exception;"
        ! @"[] \begin{lstlisting}
number = new None<int>();
int inc_number = number.Visit(() => { throw new Exception(\""Expexting a value..\""); }, i => i + 1);
\end{lstlisting}" 
        ! @"As we see we managed to define operations on the fly over polimorphic data types in a controlled way;"
        ! @"This design will work properly (regadless the data type captured by \texttt{T}) as long as there are always options to choose."
      ]
    SubSection("More sample")
    ItemsBlock
      [
        ! @"Can be found on GIT under the folder: \texbf{Design Patterns Samples C#}."
      ]

    Section("The visitor design pattern")
    SubSection("The general idea")
    ItemsBlock
      [
        ! @"What we have seen so far is an example implementing the \textit{visitor} design pattern;"                
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
interface I<$FieldsC_1$, $FieldsC_2$, ..., $FieldsC_N$>
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
class C1<$FieldsC_1$, $FieldsC_2$, ..., $FieldsC_N$> 
     : I<$FieldsC_1$, $FieldsC_2$, ..., $FieldsC_N$>
  { 
    Input_1 value;
    U Visit<U>(Func<$FieldsC_1$, U> $onC_1$, 
               ..., 
               Func<$FieldsC_N$, U> $onC_N$){
      $onC_1$(this.value);
    }
  }
\end{lstlisting}"
    ]
    SubSection("How do we use it (lambda version)? (Step 4)")
    ItemsBlock
      [
        ! @"Every time we want to consume an instance of type \texttt{M} we have to \texttt{Visit} it."
        ! @"[] \begin{lstlisting} 
I<$FieldsC_1$, $FieldsC_2$, ..., $FieldsC_N$> i;
...
m.Visit(
 i_1 => $b_1$,
 ...,
 i_N => $b_n$);
\end{lstlisting}" 
        ! @"Every argment of the visit becomes a function that is triggered depending on the concrete type of \texttt{i};"
        ! @"\texttt{i\_i} are the fields of a concrete class $C_i$};"
        ! @"\texttt{b\_i} is the block of to run when a visit on an instance of a concrete type $C_i$ is needed."]

    SubSection("A visual representation")
    UML
      [ Package("Visitor", 
                [Interface("M<FieldsC1, FieldsCN>",10.0,0.0,0.0,[Operation("Visit<U>", [@"onC1 : FieldsC1 $\rightarrow$ U"; "..." ; @"onCN : FieldsCN $\rightarrow$ U"], Some "U")])
                 Class("C1", -3.0, -3.0, Some "M<FieldsC1, FieldsCN>", [Attribute("value", "FieldsC1")], [Operation("Visit<U>", [@"onC1 : FieldsC1 $\rightarrow$ U"; "..."], Some "U")])
                 Class("CN", 3.0, -3.0, Some "M<FieldsC1, FieldsCN>", [Attribute("value", "FieldsCN")], [Operation("Visit<U>", ["..."; @"onCN : FieldsCN $\rightarrow$ U"], Some "U")])
                 ])

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