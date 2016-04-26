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
  public class NaturalList : IEnumerator<int>
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
  public class CircularList<T> : IEnumerator<T>
  {
    private List<T> list;
    private int index = -1;
    public CircularList(List<T> list)
    {
      this.list = list;
    }
    private CircularList() { }
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
  public class Array<T> : IEnumerator<T>
  {
    private T[] array;
    private int index = -1;
    public Array(T[] array) { this.array = array; }
    private Array() { }
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


  public class Map<T, U> : IEnumerator<U>
  {
    private IEnumerator<T> decoratedCollection;
    Func<T, U> f;
    public Map(IEnumerator<T> collection, Func<T, U> f)
    {
      this.decoratedCollection = collection;
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





  class Program
  {
    static void Main(string[] args)
    {
      IEnumerator<int> elems = new NaturalList();
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