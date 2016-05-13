using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptionLambda;
namespace Lesson2
{
  public interface Iterator<T>
  {
    Option<T> GetNext();
  }
  public interface TraditionalIterator<T>
  {
    bool MoveNext();
    T Current { get; }
  }


  namespace SafeCollections
  {
    public class NaturalList : Iterator<int>
    {
      private int current = -1;

      public Option<int> GetNext()
      {
        current++;
        return new Some<int>(current);
      }

    }
    public class CircularList<T> : Iterator<T>
    {
      private List<T> list;
      private int index = -1;
      public CircularList(List<T> list)
      {
        this.list = list;
      }
      public Option<T> GetNext()
      {
        index = (index + 1) % list.Count;
        return new Some<T>(list[index]);
      }
    }
    public class Array<T> : Iterator<T>
    {
      private T[] array;
      private int index = -1;
      public Array(T[] array) { this.array = array; }
      public Option<T> GetNext()
      {
        if (index + 1 < array.Length)
          return new None<T>();
        index++;
        return new Some<T>(array[index]);
      }
    }
  }
  namespace UnsafeCollections
  {
    public class NaturalList : TraditionalIterator<int>
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
    public class CircularList<T> : TraditionalIterator<T>
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
    public class Array<T> : TraditionalIterator<T>
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
    public class Map<T, U> : TraditionalIterator<U>
    {
      private TraditionalIterator<T> decoratedCollection;
      Func<T, U> f;
      public Map(TraditionalIterator<T> collection, Func<T, U> f)
      {
        this.decoratedCollection = collection;
        this.f = f;
      }

      public U Current
      {
        get
        {
          return f(decoratedCollection.Current);
        }
      }

      public bool MoveNext()
      {
        return decoratedCollection.MoveNext();
      }


    }
  }




  class Program
  {
    static void Main(string[] args)
    {


      TraditionalIterator<int> elems = new UnsafeCollections.NaturalList();
      //UnsafeCollections.Map<int, string> mapped_elems = new UnsafeCollections.Map<int, string>(elems, x => x.ToString() + " is a string now..");
      elems.MoveNext();
      while (true)
      {
        Console.WriteLine(elems.Current);
        elems.MoveNext();
        Thread.Sleep(100);
      }

    }
  }
}