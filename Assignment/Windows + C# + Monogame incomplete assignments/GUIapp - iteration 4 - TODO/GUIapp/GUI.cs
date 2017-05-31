using System;

namespace GUIapp
{

  public abstract class GuiMenuCreator
  {
    //MISSING CODE
  }

  public class GuiConstructor : GuiMenuCreator
  {
    public override GuiManager Instantiate(string option, System.Action exit)
    {
      GuiManager guiManager = new GUIapp.GuiManager();
      //MISSING CODE
    }
      return guiManager;
    }
  }

  public abstract class GuiElementCreator
  {
  //MISSING CODE
}

public interface GuiElement : Drawable, Updateable { }
  public class ButtonConstructor : GuiElementCreator
  {
    public override GuiElement Instantiate(string text, Point top_left_corner, int size, Colour color)
    {
    //MISSING CODE
  }

  public override GuiElement Instantiate(string text, Point top_left_corner, int size, Colour color, float width, float height, Action action)
    {
    //MISSING CODE
  }
}
  public class LabelConstructor //MISSING CODE
{
  //MISSING CODE
}

public class Label : GuiElement
  {
    public string content;

    public int size;
    public Colour color;
    public Point top_left_corner;
    public Label(string content, Point top_left_corner, int size, Colour color)
    {
      this.size = size;
      this.color = color;
      this.content = content;
      this.top_left_corner = top_left_corner;

    }

    public void Draw(DrawVisitor visitor)
    {
      visitor.DrawLabel(this);
    }
    public void Update(UpdateVisitor visitor, float dt) { }

    
  }

  public class Button : GuiElement
  {
    public float width, height;
    public Action action;
    public Colour color;
    public Label label;
    public Point top_left_corner;
    public Button(string text, Point top_left_corner, int size, Colour color, float width, float height, Action action) 
    {
      this.action = action;
      this.width = width;
      this.height = height;
      this.color = color;
      this.top_left_corner = top_left_corner;
      label = new Label(text, top_left_corner, size, color);
    }
    public void Draw(DrawVisitor visitor)
    {
      visitor.DrawButton(this);
    }
    public bool is_intersecting(Point point)
    {
      return point.X > top_left_corner.X && point.Y > top_left_corner.Y &&
             point.X < top_left_corner.X + width && point.Y < top_left_corner.Y + height;
    }
    public void Update(UpdateVisitor visitor, float dt)
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
