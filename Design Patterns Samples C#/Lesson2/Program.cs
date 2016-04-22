using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptionLambda;
namespace Lesson2
{
  public interface IEnumerator<T>
  {
    bool MoveNext();
    T Current { get; }
    void Reset();
  }
  public class NaturalsListEnumerator : IEnumerator<int>
  {
    private int current = -1;
    public int Current
    {
      get
      {
        if (current < 0)
          throw new Exception("MoveNext first.");
        return current;
      }
    }

    public bool MoveNext()
    {
      current++;
      return true;
    }

    public void Reset() { current = -1; }
  }
  public class InfiniteLoopListEnumerator<T> : IEnumerator<T>
  {
    private List<T> list;
    private int index = -1;
    public InfiniteLoopListEnumerator(List<T> list)
    {
      this.list = list;
    }
    private InfiniteLoopListEnumerator() { }
    public T Current
    {
      get
      {
        if (index < 0)
          throw new Exception("MoveNext first.");
        return list[index];
      }
    }
    public bool MoveNext()
    {
      if (index + 1 < list.Count)
        index = 0;
      else
        index++;
      return true;
    }
    public void Reset()
    {
      index = -1;
    }
  }
  public class ArrayEnumerator<T> : IEnumerator<T>
  {
    private T[] array;
    private int index = -1;
    public ArrayEnumerator(T[] array) { this.array = array; }
    private ArrayEnumerator() { }
    public T Current
    {
      get
      {
        if (index < 0)
          throw new Exception("MoveNext first.");
        return array[index];
      }
    }
    public bool MoveNext()
    {
      if (index + 1 < array.Length)
        return false;
      index++;
      return true;
    }
    public void Reset()
    {
      index = -1;
    }

  }


  //optional
  public abstract class IEnumeratorDecorator<T> : IEnumerator<T>
  {
    protected IEnumerator<T> decoratedCollection;

    public IEnumeratorDecorator(IEnumerator<T> c)
    {
      this.decoratedCollection = c;
    }

    public T Current
    {
      get
      {
        return decoratedCollection.Current;
      }
    }

    public bool MoveNext()
    {
      return decoratedCollection.MoveNext();
    }

    public void Reset()
    {
      decoratedCollection.Reset();
    }
  }
  public class Map<T, U> : IEnumeratorDecorator<T>
  {
    Func<T, U> f;
    public Map(IEnumerator<T> collection, Func<T, U> f) : base(collection)
    {
      this.f = f;
    }

    new public U Current
    {
      get
      {
        return f(decoratedCollection.Current);
      }
    }

    new public bool MoveNext()
    {
      return decoratedCollection.MoveNext();
    }

    new public void Reset()
    {
      decoratedCollection.Reset();
    }
  }



/* <lesson summary>
-Collections are important

-They come in different shapes and implementations
--stream of data
--records of a database
--collection of cars
--array of numbers
--array of array of pixels (a matrix)
--etc.

-How do we use them? A very typical aproach is to go through all its elements one by one till we visit them all
-Of course the the way we visit them depends on the data structure
--For example if we are visiting a collection we, most likely, will go through each element in order starting from the first one
--If we iterate a tree data structure we can iterate in depth or in width
--If we iterate an a list that repeats itself we have to restart its iteration as soon as we iterate the last element of the list
--etc.

-Every collection comes with a different concrete implementation it would be wise in order to achieve reuse, maintainability, etc. to have a 
  common way to introduce collections, since after all we are simply iterating. We know that dealing with concrete types increase coupling, so
  how can we reduce it? Again by means of controlled/effective polimorphism.

-What we need then is to find a common interface that captures all behaviors of collections that allows us to iterate any, regadless the concrete impl.
-Ideally what we need is a way for interacting with it so to be able to:
--get the a value out of it
--move in order to the next value

-we now present such interface and some examples
--natural numbers
--infinite loop over list

 --formalism

*/

  class Program
  {
    static void Main(string[] args)
    {
      IEnumerator<int> elems = new NaturalsListEnumerator();
      Map<int, string> mapped_elems = new Map<int, string>(elems, x => x.ToString() + " is a string now..");
      mapped_elems.MoveNext();
      while (true)
      {
        Console.WriteLine(mapped_elems.Current);
        mapped_elems.MoveNext();
        Thread.Sleep(100);
      }

    }
  }
}