module TestExamWithSolution

open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime
open TypeChecker

let exam title =
  [
    Section("Question 1")
    TextBlock @"Given the following class definitions, and a piece of code that uses them, fill in the stack, heap, and PC with all steps taken by the program at runtime."
    ItemsBlock
      [
        ! @"Points: \textit{3 (30\% of total).}"
        ! @"Grading: one point per correctly filled-in execution step."
        ! @"Associated learning objective: \textit{abstraction}."
      ]
    CSharpStateTrace(TextSize.Small,
                     (interfaceDef "NumberVisitor"[typedSig "OnInt" [] "void" 
                                                   typedSig "OnFloat" [] "void"] >>
                      (interfaceDef "Number"[typedSig "Visit" ["NumberVisitor", "visitor"] "void" ] >>
                        (classDef "TypePrettyPrinter" 
                              [
                              implements "NumberVisitor"
                              typedDef "TypePrettyPrinter" [] "" endProgram
                              typedDef "OnInt" [] "void" (staticMethodCall "Console" "WriteLine" [ConstString("I am Int")])
                              typedDef "OnFloat" [] "void" (staticMethodCall "Console" "WriteLine" [ConstString("I am Float")])
                              ] >>
                         (classDef "MyFloat" 
                              [
                              implements "Number"
                              typedDef "MyFloat" [] "" endProgram
                              typedDef "Visit" ["NumberVisitor", "visitor"] "void" (methodCall "visitor" "OnFloat" [])
                              ] >> 
                          (classDef "MyInt" 
                              [
                              implements "Number"
                              typedDef "MyInt" [] "" endProgram
                              typedDef "Visit" ["NumberVisitor", "visitor"] "void" (methodCall "visitor" "OnInt" [])
                              ] >> 
                              ((typedDeclAndInit "visitor" "TypePrettyPrinter" (newC "TypePrettyPrinter" [])) >>
                               ((typedDeclAndInit "aNumber" "Number" (newC "MyFloat" [])) >>
                                ((methodCall "aNumber" "Visit" [var "visitor"]) >> dots)))))))),
                      Runtime.RuntimeState<_>.Zero (constInt 1))
    
    Section("Question 2")
    TextBlock @"Given the following class definitions, and a piece of code that uses them, fill in the declarations, class definitions, and PC with all steps taken by the compiler while type checking."
    CSharpTypeTrace(TextSize.Small,
                    (interfaceDef "NumberVisitor"[typedSig "OnInt" [] "void" 
                                                  typedSig "OnFloat" [] "void"] >>
                      (interfaceDef "Number"[typedSig "Visit" ["NumberVisitor", "visitor"] "void" ] >>
                        (classDef "TypePrettyPrinter" 
                              [
                              implements "NumberVisitor"
                              typedDef "TypePrettyPrinter" [] "" endProgram
                              typedDef "OnInt" [] "void" (staticMethodCall "Console" "WriteLine" [ConstString("I am Int")])
                              typedDef "OnFloat" [] "void" (staticMethodCall "Console" "WriteLine" [ConstString("I am Float")])
                              ] >>
                         (classDef "MyFloat" 
                              [
                              implements "Number"
                              typedDef "MyFloat" [] "" endProgram
                              typedDef "Visit" ["NumberVisitor", "visitor"] "void" (methodCall "visitor" "OnFloat" [])
                              ] >> 
                          (classDef "MyInt" 
                              [
                              implements "Number"
                              typedDef "MyInt" [] "" endProgram
                              typedDef "Visit" ["NumberVisitor", "visitor"] "void" (methodCall "visitor" "OnInt" [])
                              ] >> 
                              ((typedDeclAndInit "visitor" "NumberVisitor" (newC "TypePrettyPrinter" [])) >>
                               ((typedDeclAndInit "aNumber" "Number" (newC "MyFloat" [])) >>
                                ((methodCall "aNumber" "Visit" [var "visitor"]) >> dots)))))))),
                    TypeCheckingState.Zero, false)


    ItemsBlock
      [
        ! @"Points: \textit{3 (30\% of total).}"
        ! @"Grading: one point per correctly filled-in type checking step."
        ! @"Associated learning objective: \textit{type checking}."
      ]
    Section("Question 3")
    TextBlock @"Given the following UML diagram of the decorator method, fill in the missing parts."
    Image("decoratorExamMockup", 0.7)
    TextBlock @"For the solution see below:"
    Image("decorator", 0.7)
    ItemsBlock
      [
        ! @"Points: \textit{4 (40\% of total).}"
        ! @"Grading: one point per correctly filled-in part."
        ! @"Associated learning objective: \textit{abstraction}."
      ]
    ]
