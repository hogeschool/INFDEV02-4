using System;

namespace GUIapp
{

  public abstract class GuiMenuCreator
  {
    public abstract GuiManager Instantiate(string option, System.Action exit);
  }

  public class GuiConstructor : GuiMenuCreator
  {
    public override GuiManager Instantiate(string option, System.Action exit)
    {
      GuiManager guiManager = new GUIapp.GuiManager();
      switch (option)
      {
        default:
          {
            GuiElementCreator buttonConstructor = new ButtonConstructor();
            GuiElementCreator labelConstructor = new LabelConstructor();
            guiManager.elements = new List<GuiElement>();
            guiManager.elements.Add(labelConstructor.Instantiate("Hi Ahmed!", new Point(0, 0), 10, Colour.Black));
            guiManager.elements.Add(buttonConstructor.Instantiate("Click me", new Point(0, 100), 10, Colour.Black, 100, 30,
                                                        () => {
                                                          guiManager.elements = new List<GuiElement>();
                                                          guiManager.elements.Add(buttonConstructor.Instantiate("Exit", new Point(0, 0), 10, Colour.Black, 100, 30,
                                                                  () => {
                                                                                exit();
                                                                              }
                                                                  ));
                                                        }
                                                        ));
            break;
          }
      }
      return guiManager;
    }
  }

  public abstract class GuiElementCreator
  {
    public abstract GuiElement Instantiate(string content, Point top_left_corner, int size, Colour color);
    public abstract GuiElement Instantiate(string text, Point top_left_corner, int size, Colour color, float width, float height, Action action);
  }

  public abstract class GuiElement : Drawable, Updateable
  {
    public Point top_left_corner;
    public GuiElement(Point top_left_corner)
    {
      this.top_left_corner = top_left_corner;
    }
    public abstract void Draw(DrawVisitor visitor);
    public abstract void Update(UpdateVisitor visitor, float dt);

  }
  public class ButtonConstructor : GuiElementCreator
  {
    public override GuiElement Instantiate(string text, Point top_left_corner, int size, Colour color)
    {
      return new Button(text, top_left_corner, size, color, 40, 40, () => { });
    }

    public override GuiElement Instantiate(string text, Point top_left_corner, int size, Colour color, float width, float height, Action action)
    {
      return new Button(text, top_left_corner, size, color, width, height, action);
    }
  }
  public class LabelConstructor : GuiElementCreator
  {
    public override GuiElement Instantiate(string text, Point top_left_corner, int size, Colour color)
    {
      return new Label(text, top_left_corner, size, color);
    }

    public override GuiElement Instantiate(string text, Point top_left_corner, int size, Colour color, float width, float height, Action action)
    {
      return new Label(text, top_left_corner, size, color);
    }
  }
  public class Label : GuiElement
  {
    public string content;

    public int size;
    public Colour color;
    
    public Label(string content, Point top_left_corner, int size, Colour color) : base(top_left_corner)
    {
      this.size = size;
      this.color = color;
      this.content = content;
    }

    public override void Draw(DrawVisitor visitor)
    {
      visitor.DrawLabel(this);
    }
    public override void Update(UpdateVisitor visitor, float dt) { }

    
  }
  public abstract class GuiDecorator //MISSING CODE

  public class Button : GuiDecorator
  {
    public float width, height;
    public Action action;
    public Colour color;
    public Button(string text, Point top_left_corner, int size, Colour color, float width, float height, Action action)
                  //MISSING CODE
    {
      this.action = action;
      this.width = width;
      this.height = height;
      this.color = color;
    }
    public override void Draw(DrawVisitor visitor)
    {
      visitor.DrawButton(this);
    }
    public bool is_intersecting(Point point)
    {
      return point.X > top_left_corner.X && point.Y > top_left_corner.Y &&
             point.X < top_left_corner.X + width && point.Y < top_left_corner.Y + height;
    }
    public override void Update(UpdateVisitor visitor, float dt)
    {
      visitor.UpdateButton(this, dt);
    }

    
  }

  
  public class GuiManager : Updateable, Drawable
  {
    public List<GuiElement> elements;

    public void Draw(DrawVisitor visitor)
    {
      visitor.DrawGui(this);
    }
    
    public void Update(UpdateVisitor visitor, float dt)
    {
      visitor.UpdateGui(this, dt);
    }
  }

  

}
