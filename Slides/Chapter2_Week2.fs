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
            ! @"design patterns for identifying the fundamental communication behavior between entities"
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

    SubSection("About collections")
    ItemsBlock
      [
        ! @"They come in different shapes and implementations:"
        ! @"Option (an option essentially is a ``one-element` collections'')"
        ! @"Stream of data"
        ! @"Records of a database"
        ! @"List of cars"
        ! @"Array of numbers"
        ! @"Array of Array of pixels (a matrix)"
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
        ! @"CODE"
        ! @"The latter is a static compact data structure. In an array the maximum number of elements is fixed"
        ! @"CODE"
      ]

    SubSection("Iterating lists")
    ItemsBlock
      [
        ! @"Iterating a list requires a variable that references the current node in the list"
        ! @"To move to the next node we need to manually update such variable, by assigning to it a reference to the next node"
        ! @"CODE"
        
      ]
    SubSection("Iterating array")
    ItemsBlock
      [
        ! @"Iterating an array requires a variable (an index) containing a number representing the position of the current visited element"
        ! @"To move to the next element we need to manually update the variable, increasing it by one."
        ! @"CODE"
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
    ItemsBlock
      [
        ! @"Is an interface \texttt{IEnumerator<T>} containing the following methods signatures"
        ! @"SIGNATURES + EXAPLANATION"
      ]
    SubSection("Implementing the IEnumerator<T>")
    ItemsBlock
      [
        ! @"At this point every collection that wants to provide a disciplined and controlled iteration mechanism has to implement such interface"
        ! @"We now show a series of collections implementing such our interface"
      ]
    SubSection("Option")
    ItemsBlock
      [
        ! @"..."
      ]
    SubSection("Natural numbers")
    ItemsBlock
      [
        ! @"..."
      ]
    SubSection("Array<T>")
    ItemsBlock
      [
        ! @"..."
      ]
    SubSection("Iterator - formalization")
    ItemsBlock
      [
        ! @"UML"
        ! @"Syntax"
        ! @"Semantics"
      ]
    SubSection("The iterator in literature")
    ItemsBlock
      [
        ! @"Is an interface \texttt{UnsafeIEnumerator<T>} containing the following methods signatures"
        ! @"CODE"
        ! @"It is unsafe"
      ]
    SubSection("Improving the UnsafeIEnumerator<T> safeness")
    ItemsBlock
      [
        ! @"We adapt it to the our IEnumerator<T>"
        ! @"CODE"
      ]
    SubSection("Conclusions")
    ItemsBlock
      [
        ! @"Iterating collections is a time consuming, error-prone, activity, since collections come with different implementations each with its own complexity"
        ! @"Iterators are a mechanism that hides the complexity of a collection and provides a clean interaction surface to iterate them"
        ! @"This mechanism not only reduces the amount of code to write (achieving then the DRY principle), but also reduces the amount of coupling"
      ]


  ]
