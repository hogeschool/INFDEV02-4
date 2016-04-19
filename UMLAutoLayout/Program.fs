open System
open System.Windows.Forms
open Microsoft.Xna.Framework
open System.Drawing
open System.Drawing.Drawing2D
open Utils
open AST

let toPoint (pos:Vector2) = Point(pos.X |> int, pos.Y |> int)
let toSize (pos:Vector2) = Size(pos.X |> int, pos.Y |> int)

let makeArrows (classes:Map<string,Vector2*Vector2*ClassSignature>) =
  [
    for c in classes do
      let pos,size,cs = c.Value
      let start = pos + size * Microsoft.Xna.Framework.Vector2.UnitX * 0.5f
      for b in cs.Implementations do
        let b_pos,b_size,b_cs = classes.[b]
        let finish = b_pos + b_size * Microsoft.Xna.Framework.Vector2(0.5f, 1.0f)
        yield c.Key,b,start,finish
  ]

// TODO: push away wrt the classes as well, not only the arrows
let rec pushAway (classes:Map<string,Vector2*Vector2*ClassSignature>) (arrows:List<string*string*Vector2*Vector2>) =
  let mutable changed = false
  let newClasses =
    [
      for c in classes do
        let origin,size,cs = c.Value
        let area = Rectangle(origin |> toPoint, size |> toSize)
        let center = origin + size * 0.5f
        let radius = size.Length()
        let radius1 = (min size.X size.Y) / 2.0f
        let arrowOffset =
          [
            for src,tgt,s,e in arrows do
              let dir = (e - s).Normalized
              let c_s = s - center
              let c_e = e - center
              let dist_c_s = Vector2.Dot(c_s, dir)
              let dist_c_e = Vector2.Dot(c_e, dir)
              if (*dist_c_s < radius1 && dist_c_e > -radius1 &&*) src <> c.Key && tgt <> c.Key then
                let n_a = s.Perpendicular.Normalized
                let dist = Vector2.Dot(n_a, center - s)
                if dist = 0.0f then
                  do changed <- true
                  yield n_a * 5.0f * (if random.Next() % 2 = 0 then -1.0f else 1.0f)
                elif abs dist <= radius then 
                  do changed <- true
                  yield n_a * dist
          ] |> Seq.fold (+) Vector2.Zero
        let classOffset =
          [
            for c' in classes do
              if c'.Key <> c.Key then
                let origin',size',cs' = c.Value
                let area' = Rectangle(origin' |> toPoint, size' |> toSize)
                if area.IntersectsWith(area') then
                  yield (origin' - origin).Normalized * 5.0f
          ] |> Seq.fold (+) Vector2.Zero
        yield c.Key,(origin + arrowOffset,size,cs)
    ] |> Map.ofList
  if not changed then
    classes,arrows
  else
    pushAway newClasses (makeArrows newClasses)

[<EntryPoint>]
let main argv = 
  let testModule = 
    {
      Classes = 
        [
          { IsInterface = true; Name = "I_A"; Implementations = []; Methods = [ { Arguments = [ "int","A1" ]; Return = "int" } ]; Fields = [] }
          { IsInterface = false; Name = "I_B"; Implementations = ["I_A"]; Methods = [ { Arguments = [ "int","B1" ]; Return = "int" } ]; Fields = [] }
          { IsInterface = false; Name = "I_C"; Implementations = ["I_A"; "I_B"]; Methods = [ { Arguments = [ "int","C1" ]; Return = "int" } ]; Fields = [] }
          { IsInterface = false; Name = "I_D"; Implementations = ["I_A"]; Methods = [ { Arguments = [ "int","D1" ]; Return = "int" } ]; Fields = [] }
        ]
    }
  let f = new Form()
  do f.WindowState <- FormWindowState.Maximized
  let graphics = f.CreateGraphics()
  do graphics.SmoothingMode <- SmoothingMode.HighQuality
  let mutable layers = ResizeArray()
  let mutable classes = System.Collections.Generic.Dictionary()
  for c in testModule.Classes do classes.Add(c.Name, c)
  let firstLayer = ResizeArray()
  for c in classes do
    if c.Value.Implementations |> List.isEmpty then
      firstLayer.Add(c.Value)
  for c in firstLayer do classes.Remove(c.Name) |> ignore
  do layers.Add(firstLayer)
  while classes.Count > 0 do
    let nextLayer = ResizeArray()
    for c in classes do
      if c.Value.Implementations |> List.forall (fun i -> classes.ContainsKey(i) |> not) then
        nextLayer.Add(c.Value)
    for c in nextLayer do classes.Remove(c.Name) |> ignore
    do layers.Add(nextLayer)
  let classes = 
    [
      for y,layer in layers |> Seq.mapi times do
        for x,cs in layer |> Seq.mapi times do
          let pos = Point(x * 250 + 100, y * 200 + 50)
          let size = findClassSignatureSize cs pos 150
          yield cs.Name,(pos.ToVector2,size.ToVector2,cs)
    ] |> Map.ofList
  let arrows = makeArrows classes
  let classes, arrows = pushAway classes arrows
  let min_x = classes |> Seq.map (fun c -> let origin,size,_ = c.Value in origin.X + size.X * 0.5f) |> Seq.min
  let min_y = classes |> Seq.map (fun c -> let origin,size,_ = c.Value in origin.Y + size.Y * 0.5f) |> Seq.min
  let offset = 
    Vector2((if min_x < 150.0f then -min_x + 150.0f else 0.0f), 
            (if min_y < 80.0f then -min_y + 80.0f else 0.0f))
  do f.Paint.Add(fun _ ->
    for c in classes do
      let pos,size,cs = c.Value
      do drawClassSignature cs ((pos + offset) |> toPoint) 150 graphics
    for _,_,start,finish in arrows do
      // TODO: draw line and triangle separately!
      let finish = finish + offset
      let start = start + offset
      do graphics.DrawLine(arrowsPen, Point(finish.X |> int, finish.Y |> int), Point(start.X |> int, start.Y |> int)))
  do f.ShowDialog() |> ignore
  0
