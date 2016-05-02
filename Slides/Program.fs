open Compile

let makeSlides (title : string) =
  do batchProcess LatexDefinition.generatePresentation (Chapter1.Week1.v2.slides title) (sprintf "%s_Lec1_DP_intro" title) "The INFDEV team" "Introduction" true true
  do batchProcess LatexDefinition.generatePresentation (Chapter2.Week2.v1.slides title) (sprintf "%s_Lec2_DP_iterator" title) "The INFDEV team" "Iterating collections" true true

[<EntryPoint>]
let main argv =
//  do makeSlides "INFSEN02-2"
  do makeSlides "INFDEV02-4"
  0    