module Chapter5.Week5.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
     ! @"Today we are going to study the a behavioral pattern: the observer design pattern"
     ! @"Reasons: sometimes we want a piece of code to be automatically executed after a change in the state"
     ! @"Example of doing it by hand"
     ! @"Example of doing it means of a list containing lambdas"
     ! @"The observer design pattern: is a design pattern in which an object called \textbf{subject}, maintains a list of objects called \textbf{observers}, and notifies them on any state changes, usually calling one of their methods"
     ! @"It defines a one-to-many dependency"
     ! @"Formalization"
     ! @"Conclusions"
  ]