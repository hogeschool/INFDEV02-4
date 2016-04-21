using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptionLambda;
namespace Lesson2
{
  abstract class Observable<T>
  {
    internal List<Observer<T>> observers = new List<Observer<T>>();
    public void register_observer(Observer<T> new_observer)
    {
      observers.Add(new_observer);
    }
    public abstract void notify_observers();
  }
  interface Observer<T>
  {
    void Notify(Option<T> t);
  }

  public interface Iterator<T>
  {
    Option<T> GetNext();
    bool HasNext();
  }
  class Stream<T> : Observable<T>, Iterator<T>
  {
    List<T> elements = new List<T>();
    public Stream(List<T> elements)
    {
      this.elements = elements;

    }
    int current = -1;
    public Option<T> GetNext()
    {
      if (HasNext())
      {
        current++;
        return new Some<T>(elements[current]);
      }
      else
        return new None<T>();
    }
    public bool HasNext()
    {
      return current < elements.Count - 1;
    }

    public override void notify_observers()
    {
      foreach (var observer in observers)
        if (HasNext())
          observer.Notify(GetNext());
    }
  }
  class Form : Observer<Widget>
  {
    List<Widget> widgets = new List<Widget>();
    public void Notify(Option<Widget> t)
    {
      widgets.Add(t.Visit(() => { throw new Exception("No widget.. :("); },
                          widget =>
                          {
                            Console.WriteLine("Got widget.. :)");
                            return widget;
                          }));
    }
  }

  class WidgetCreator
  {
    public Stream<Widget> GetWidgets()
    {
      List<Widget> widgets = new List<Widget>();
      widgets.Add(new Button());
      widgets.Add(new Button());
      widgets.Add(new Label());
      return new Stream<Widget>(widgets);
    }
  }

  interface Widget { }
  public class Button : Widget { }
  public class Label : Widget { }

  class Program
  {
    static void Main(string[] args)
    {
      Form form = new Form();
      WidgetCreator widgetCreator = new WidgetCreator();
      var widget_stream = widgetCreator.GetWidgets();
      widget_stream.register_observer(form);
      while (true)
      {
        widget_stream.notify_observers();
      }
    }
  }
}