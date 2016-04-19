module Utils

open System
open System.Windows.Forms
open System.Drawing
open System.Drawing.Drawing2D

let arrowsPen = 
  let pen = new Pen(Color.Black, 4.0f)
  do pen.StartCap <- LineCap.ArrowAnchor
  do pen.EndCap <- LineCap.NoAnchor
  pen
let consolas12 = new Font("Consolas", 12.0f)
let times a b = a,b

type System.Drawing.Size
  with 
    member s.ToVector2 with get() = Microsoft.Xna.Framework.Vector2(s.Width |> float32, s.Height |> float32)

type System.Drawing.Point
  with 
    member p.ToVector2 with get() = Microsoft.Xna.Framework.Vector2(p.X |> float32, p.Y |> float32)
    member p.ToPointF with get() = System.Drawing.PointF(p.X |> float32, p.Y |> float32)

type Microsoft.Xna.Framework.Vector2
  with 
    member v.Perpendicular = Microsoft.Xna.Framework.Vector2(-v.Y,v.X)
    member v.Normalized = Microsoft.Xna.Framework.Vector2.Normalize(v)

let random = new System.Random()
