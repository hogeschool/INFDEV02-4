open Compile

[<EntryPoint>]
let main argv = 
  do batchProcess LatexDefinition.generatePresentation Chapter1.Week1.v2.slides "INFDEV02_4_Lec1_DP_intro" "The INFDEV team" "Introduction" true true
  0    