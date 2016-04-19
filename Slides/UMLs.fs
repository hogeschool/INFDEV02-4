module UMLs

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime


let factory =
  UML
      [ Package("Shapes", 
                [Interface("Shape",3.0,0.0,0.0,[Operation("draw", [], Some "Void")])
                 Class("Circle", -3.0, -2.0, Some "Shape", [Attribute("Position", "Point")], [])
                 Class("Square", 3.0, -2.0, Some "Shape", [Attribute("Position", "Point")], [])])
        Class("ShapeFactory", -3.5, -5.5, Option.None, [], [Operation("getShape", [], Option.None)])
        Class("Program", 3.5, -5.5, Option.None, [], [Operation("main", [], Option.None)])
        Arrow("ShapeFactory", "Creates", "Shapes")
        Arrow("Program", "Asks", "ShapeFactory")
      ]