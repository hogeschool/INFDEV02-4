using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GUIapp
{
  public interface Option<T>
  {
    U Visit<U>(Func<U> onNone, Func<T, U> onSome);
    void Visit(Action onNone, Action<T> onSome);
  }
  public class None<T> : Option<T>
  {
    public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
    {
      return onNone();
    }
    public void Visit(Action onNone, Action<T> onSome)
    {
      onNone();
    }
  }
  public class Some<T> : Option<T>
  {
    T value;
    public Some(T value)
    {
      this.value = value;
    }
    public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
    {
      return onSome(value);
    }
    public void Visit(Action onNone, Action<T> onSome)
    {
      onSome(value);
    }
  }
  public interface Iterator<T>
  {
    Option<T> GetNext();
    Option<T> GetCurrent();
    void Reset();
  }

  public class List<T> : Iterator<T>
  {
    private int size;
    private T[] array;
    private int current;
    private int amount_of_items;

    public List()
    {
      size = 10;
      amount_of_items = 0;
      current = 0;
      array = new T[10];
      Reset();
    }
    public void Add(T item)
    {
      if (amount_of_items >= size)
      {
        size *= 2;
        T[] new_array = new T[size];
        Array.Copy(array, new_array, amount_of_items);
      }
      else
      {
        array[amount_of_items] = item;
      }
      amount_of_items++;

    }

    public Option<T> GetNext()
    {
      current++;
      if (current >= amount_of_items)
      {
        return new None<T>();
      }
      return new Some<T>(array[current]);
    }

    public void Reset()
    {
      current = -1;
    }

    public Option<T> GetCurrent()
    {
      if (current == -1) return new None<T>();
      return new Some<T>(array[current]);
    }
  }

  public class Point
  {
    public Point(float x, float y)
    {
      this.X = x;
      this.Y = y;
    }
    public float X { get; set; }
    public float Y { get; set; }
  }




  //MISSING CODE
  {
    void DrawRectangle(Point top_left_coordinate, float width, float height, Colour color);
    void DrawString(string text, Point top_left_coordinate, int size, Colour color);
  }

  public enum Colour { White, Black, Blue };

  public interface InputManager
  {
  //MISSING CODE
  }

public class MonogameMouse : InputManager
  {

    public Option<Point> Click()
    {
    //MISSING CODE

    }
}

  public class MonogameDrawingAdapter : DrawingManager
  {
    SpriteBatch sprite_batch;
    ContentManager content_manager;

    Texture2D white_pixel;
    SpriteFont default_font;
    Game game;
    
    //MISSING CODE

  private Microsoft.Xna.Framework.Color convert_color(Colour color)
    {
    //MISSING CODE
    }


  //MISSING CODE
    {
    //MISSING CODE
  }

  public void DrawString(string text, Point top_left_coordinate, int size, Colour color)
    {
    //MISSING CODE
  }
}
  public interface Updateable { void Update(UpdateVisitor visitor, float dt); }
  public interface Drawable { void Draw(DrawVisitor visitor); }


  public interface DrawVisitor {
    void DrawButton(Button element);
    void DrawLabel(Label element);
    void DrawGui(GuiManager element);
  }

  

  public class DefaultDrawVisitor : DrawVisitor
  {
    DrawingManager drawing_manager;
    public DefaultDrawVisitor(DrawingManager drawing_manager)
    {
      this.drawing_manager = drawing_manager;
    }
    public void DrawButton(Button element)
    {
      drawing_manager.DrawRectangle(element.top_left_corner, element.width, element.height, element.color);
      element.label.Draw(this);
    }
    public void DrawLabel(Label element)
    {
      drawing_manager.DrawString(element.content, element.top_left_corner, element.size, element.color);
    }
    public void DrawGui(GuiManager gui_manager)
    {
      gui_manager.elements.Reset();
      while (gui_manager.elements.GetNext().Visit(() => false, _ => true))
      {
        gui_manager.elements.GetCurrent().Visit(() => { }, item => { item.Draw(this); });
      }
    }
  }


  public interface UpdateVisitor
  {
    void UpdateButton(Button element, float dt);
    void UpdateLabel(Label element, float dt);
    void UpdateGui(GuiManager element, float dt);
  }
  public class DefaultUpdateVisitor : UpdateVisitor
  {
    InputManager input_manager;
    public DefaultUpdateVisitor(InputManager input_manager)
    {
      this.input_manager = input_manager;
    }
    public void UpdateButton(Button element, float dt)
    {
      input_manager.Click().Visit(() => { element.color = Colour.White; }, position => { if (element.is_intersecting(position)) { element.color = Colour.Blue; element.action(); } });
    }
    public void UpdateLabel(Label element, float dt) {
      
    }
    public void UpdateGui(GuiManager gui_manager, float dt)
    {
      gui_manager.elements.Reset();
      while (gui_manager.elements.GetNext().Visit(() => false, _ => true))
      {
        gui_manager.elements.GetCurrent().Visit(() => { }, item => { item.Update(this, dt); });
      }
    }
  }


}
