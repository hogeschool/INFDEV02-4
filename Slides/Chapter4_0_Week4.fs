module Chapter4_0.Week4.v1


open CommonLatex
open LatexDefinition
open CodeDefinitionImperative
open Interpreter
open Runtime


let slides (title : string) = 
  [
    Section("Abstract classes")
    SubSection ("Abstract classes")
    ItemsBlock
      [
        ! @"Interfaces: fully abstract"
        ! @"Classes: fully concrete"
        ! @"We want something in the between..."
      ]
    SubSection ("About abstract classes") 
    ItemsBlock
      [
        ! @"In OO programming it is possible to design special classes containing some methods with bodies, and some without"
        ! @"These special classes are called \textit{abstract}"
      ]
    VerticalStack
      [
        ItemsBlockWithTitle ("About abstract classes") 
          [            
            ! @"In the following an abstract class \texttt{Weapon} contains a concrete method \texttt{GetAmountOfBullets} and an abstract \texttt{Fire}"
            ! @"\texttt{Fire} is abstract, since different weapons might come with different kinds of firing"
          ]
        CSharpCodeBlock(TextSize.Tiny, 
                        (abstractClassDef "Weapon" 
                                          [typedDecl "amounOfBullets" "int"|> makePublic
                                           typedDef "Weapon" ["int","amounOfBullets"] "" ((("this.amounOfBullets" := var"amounOfBullets") >> endProgram)) |> makePublic
                                           typedDef "GetAmountOfBullets" [] "int" (var "this.amounOfBullets"  |> ret) |> makePublic
                                           TypedSigAbstract("Fire", [], "void") |> makePublic]))
      ]

    SubSection ("Instantiating abstract classes")
    ItemsBlock
      [
        ! @"Not possible directly (what is the result of \texttt{new Weapon().Fire()}?)"
        ! @"Abstract classes have to be inherited in order to use their functionalities"
        ! @"All abstract methods must eventually get an implementation"
      ]
    VerticalStack
      [ 
        ItemsBlockWithTitle "Implementing our weapon"
          [
            ! @"In the following a correct implementation of our \texttt{Weapon} is provided"
          ]
        CSharpCodeBlock(TextSize.Tiny, 
                          (classDef "Gun" 
                                    [extends "Weapon"
                                     typedDefWithBase "Gun" ["int","amounOfBullets"] "" ["amounOfBullets"] endProgram |> makePublic
                                     typedDef "Fire" [] "void" ("amounOfBullets" := Code.Op(var "amounOfBullets", Operator.Minus, ConstInt(1)))|> makeOverride |> makePublic] >>
                           classDef "FastGun" 
                                    [extends "Weapon"
                                     typedDefWithBase "Gun" ["int","amounOfBullets"] "" ["amounOfBullets"] endProgram |> makePublic
                                     typedDef "Fire" [] "void" (("amounOfBullets" := Code.Op(var "amounOfBullets", Operator.Minus, ConstInt(1))) >>
                                                                ("amounOfBullets" := Code.Op(var "amounOfBullets", Operator.Minus, ConstInt(1)))) |> makeOverride |> makePublic]))
        
      ]
    SubSection ("Considerations")
    ItemsBlock
      [
        ! @"Abstract classes are a mean to combine polymorphism with concrete implementations"
      ]
    ]