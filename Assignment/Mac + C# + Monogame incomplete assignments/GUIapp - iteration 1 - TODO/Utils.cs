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







  public interface Updateable {
    //MISSING CODE HERE
  }
  public interface Drawable { void Draw(DrawVisitor visitor); }


  public interface DrawVisitor {
    void DrawButton(Button element);
    void DrawLabel(Label element);
    void DrawGui(GuiManager element);
  }

  

  public class DefaultDrawVisitor //MISSING CODE HERE
  {
    SpriteBatch sprite_batch;
    ContentManager content_manager;
    Texture2D white_pixel;
    SpriteFont default_font;

    public DefaultDrawVisitor(SpriteBatch sprite_batch, ContentManager content_manager)
    {
      this.sprite_batch = sprite_batch;
      this.content_manager = content_manager;
      white_pixel = content_manager.Load<Texture2D>("white_pixel");
      default_font = content_manager.Load<SpriteFont>("arial");
    }
    public void DrawButton(Button element)
    {
      sprite_batch.Draw(white_pixel, new Rectangle((int)element.top_left_corner.X, (int)element.top_left_corner.Y, (int)element.width, (int)element.height), element.color);
      //MISSING CODE HERE
    }
    public void DrawLabel(Label element)
    {
      sprite_batch.DrawString(default_font, element.content, new Vector2(element.top_left_corner.X, element.top_left_corner.Y), element.color);
    }
    public void DrawGui(GuiManager gui_manager)
    {

      //MISSING CODE HERE
    }
  }


  public interface UpdateVisitor {
    //MISSING CODE HERE
  }


  public class DefaultUpdateVisitor : UpdateVisitor
  {
    public void UpdateButton(Button element, float dt)
    {
      var mouse = Mouse.GetState();
      //MISSING CODE HERE
      {
        //MISSING CODE HERE
      }
      else
      {
        element.color = Color.White;
      }
    }
    public void UpdateLabel(Label element, float dt) {
      
    }
    public void UpdateGui(GuiManager gui_manager, float dt)
    {
      //MISSING CODE HERE
    }
  }


}
