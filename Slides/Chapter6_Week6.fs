module Chapter6.Week6.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
     ! @"Today we are going to study the a behavioral pattern: the strategy design pattern"
     ! @"Expressing behavior selection at runtime by means of ``if's'' is complex and error prone"
     ! @"Examples"
     ! @"The strategy design pattern allows the definition of a family of algorithms (behavior) that can be interchanged at runtime"
     ! @"The strategy lets behaviors vary independently from programs that use it"
     ! @"Formalization"
     ! @"Conclusions"
  ]