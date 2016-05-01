module Chapter3.Week3.v1

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime

let slides = 
  [
     ! @"PART 1"
     ! @"The last time we saw to to iterate unsafe and option"
     ! @"Those were all examples of a well known design pattern called the adapter"
     ! @"The decorator design pattern is a behavioral pattern that allows an interface of an existing class to be used as another interface"
     ! @"The reason is because sometimes we want to make existing classes work with others without modifying their code"
     ! @"Formalization"
     ! @"Part 2"
     ! @"In this second part we are going to study a creational pattern: the factory design pattern"
     ! @"Creating objects often require complex instantiation code not appropriate to include within composing objects. Moreover, objects creation may lead to significant code duplication, which may require not accessible information."
     ! @"Examples"
     ! @"A solution to such issue would be to provide a separate method for the creation, which subclasses can then ``override'' to specify the derived type object that will be created"
     ! @"The just described mechanism is commonly referred as factory design pattern"
     ! @"Formalization"
     ! @"Conclusions"
  ]