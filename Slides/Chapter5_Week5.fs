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
    ItemsBlock
      [
       ! @"Today we are going to study the a behavioral pattern: the decorator design pattern"
       ! @"Sometimes, we need to modify behaviors of an instance dynamically"
       ! @"Sub-classing could be a solution, but excessive sub-classing is a pitfall"
       ! @"The decorator pattern (also known as wrapper) solves this issues"
       ! @"How? By limiting sub-typing through effective aggregation"
      ]
    Section "Example"
    SubSection "Iterator"
    ItemsBlock
      [
       ! @"Consider the iterator interface"
      ]
    SubSection "Natural numbers"
    ItemsBlock
      [
       ! @"Consider the natural number collection"
      ]
    SubSection "Iterating only the even numbers"
    ItemsBlock
      [
       ! @"We wish now to iterate only the even numbers of our natural number list"
      ]
    SubSection "Adding an offset"
    ItemsBlock
      [
       ! @"We wish now to iterate and while iterating adding an offset to our natural number list"
      ]
    SubSection "Adding an offset only on even numbers"
    ItemsBlock
      [
       ! @"We wish now to iterate only even numbers of our natural number list and for each number add an offset"
       ! @"It is getting hard :/"       
      ]
    SubSection "UML discussion"
    ItemsBlock
      []
    SubSection "Iterating a range between two integers"
    ItemsBlock
      [
       ! @"We now wish to implement a new data structure RangeBetween that takes two integers A and B (where A <= B)"
      ]
    SubSection "Ranging over even numbers and/or adding an offset"
    ItemsBlock
      [
       ! @"We now wish our range to support the same behavior as for our natural numbers"
       ! @"Trivial and time consuming"       
      ]
    SubSection "Considerations"
    ItemsBlock
      [
       ! @"Sub-typing solves our problem, but adds another one. Too many repetitions"       
       ! @"Every change/add requires lots of work"
      ]
    SubSection "Considerations"
    ItemsBlock
      [
       ! @"A possible solution would see our numbers implementing offset and even"       
       ! @"This is not good..what about SOLID"
       ! @"The resulting structure is a big, bulky class (a class whose functionality does not adapt to each instance instance)"
      ]
    SubSection "Considerations"
    ItemsBlock
      [
       ! @"Abstract classes with a series of fields (which we can check to select an appropriate algorithm)"       
       ! @"But fields do not force appropriate behavior for each the roles"
      ]
    SubSection "Solution"
    ItemsBlock
      [
       ! @"A possible could be that we define an intermediate class \texttt{Decorator}, which inherits our iterator and contains an instance of it"
       ! @"It may seems strange, but this is a crucial moment!"
      ]
    ItemsBlock
      [
       ! @"As our behaviors described above: offset and even are general to all numbers we can define two distinct classes to represent them"
       ! @"Such classes extends our decorator"
      ]
    ItemsBlock
      [
       ! @"We can think of the decorator as an iterator containing elements, but it does not know how to iterate such elements"
       ! @"Who will teach the decorator how to iterate?"
       ! @"Our concrete offset and even"
      ]
    ItemsBlock
      [
       ! @"See code"
      ]
    ItemsBlock
      [
       ! @"See UML"
      ]
    ItemsBlock
      [
       ! @"Compare with the initial solution"
      ]
    SubSection "Solution - improvements"
    ItemsBlock
      [
       ! @"Offset simply transforms"
       ! @"It is a map"
       ! @"CODE"
      ]
    ItemsBlock
      [
       ! @"Even simply filters"
       ! @"It is a filter"
       ! @"CODE"
      ]
    Section("The decorator design pattern")
    SubSection "Formalism"
    ItemsBlock [ ! @"Formalism" ]
    ItemsBlock [ ! @"UML" ]
    Section "Conclusions"     
  ]