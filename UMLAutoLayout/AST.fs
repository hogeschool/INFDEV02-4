module AST
open System
open System.Windows.Forms
open System.Drawing
open System.Drawing.Drawing2D
open Utils

type Type = string

type FieldSignature = { Name : string; Type : Type }

type MethodSignature = { Arguments : List<Type*string>; Return : Type }

type ClassSignature = { IsInterface : bool; Name : string; Implementations : List<string>; Methods : List<MethodSignature>; Fields : List<FieldSignature> }

type Module = { Classes : List<ClassSignature> }


let findClassSignatureSize (i:ClassSignature) (pos:System.Drawing.Point) (width:int) : Size =
  Size(width, 18 * (1 + i.Methods.Length + i.Fields.Length))

let drawClassSignature (i:ClassSignature) (pos:System.Drawing.Point) (width:int) (gfx:Graphics) : Unit =
  do gfx.DrawString(i.Name, consolas12, System.Drawing.Brushes.Black, pos.ToPointF)
  do gfx.DrawRectangle(System.Drawing.Pens.Black, new System.Drawing.Rectangle(pos, Size(width, 18)))
  let verticalOffset = 1
  for i,f in i.Fields |> Seq.mapi times do
    let fieldSig = sprintf "- %s : %s" f.Name f.Type
    let pos = Point(pos.X, pos.Y + 18 * (i+verticalOffset))
    do gfx.DrawString(fieldSig, consolas12, System.Drawing.Brushes.Black, pos.ToPointF)
  for i,m in i.Methods |> Seq.mapi times do
    let args = 
      if List.isEmpty m.Arguments then
        "()"
      else 
        m.Arguments |> List.map (fun (t,n) -> n + ":" + t) |> List.reduce (fun a b -> a + " x " + b)
    let methodSig = sprintf "+ %s -> %s" args m.Return
    let pos = Point(pos.X, pos.Y + 18 * (i+verticalOffset))
    do gfx.DrawString(methodSig, consolas12, System.Drawing.Brushes.Black, pos.ToPointF)
  let size = Size(width, 18 * (1 + i.Methods.Length + i.Fields.Length))
  do gfx.DrawRectangle(System.Drawing.Pens.Black, new System.Drawing.Rectangle(pos, size))
