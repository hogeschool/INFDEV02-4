using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
  interface Iterable<T>
  {
    void MoveToNextElement();
    bool HasNoMoreElements();
    T GetCurrent();
  }

  class Safeify<T> : SafeIterable<T> {
    public Iterable<T> source;

    public Option<T> MoveNext()
    {
      if (source.HasNoMoreElements())
      {
        return new Empty<T>();
      } else
      {
        var current = source.GetCurrent();
        source.MoveToNextElement();
        return new NonEmpty<T>() { Value = current };
      }
    }
  }

  /*
 class Adapter<T,I> : I {
   T source;

   foreach method M of I {
     use source to implement M
   }
 }
 */

  interface Option<T>
  {
    U Visit<U>(Func<U> whenEmpty,
               Func<T, U> whenNonEmpty);
    void Visit(Action whenEmpty,
               Action<T> whenNonEmpty);
  }

  interface SafeIterable<T>
  {
    Option<T> MoveNext();
  }

  class Map<T,U> : SafeIterable<U>
  {
    SafeIterable<T> source;
    Func<T,U> doSomething;

    public Option<U> MoveNext()
    {
      return source.MoveNext().Visit<Option<U>>(
        () => new Empty<U>(),
        value_of_e => new NonEmpty<U>() { Value = doSomething(value_of_e) });
    }
  }

  class Filter<T> : SafeIterable<T>
  {
    SafeIterable<T> source;
    Func<T, bool> isValueAllowed;

    public Option<T> MoveNext()
    {
      return source.MoveNext().Visit<Option<T>>(
        () => new Empty<T>(),
        value_of_e =>
          this.isValueAllowed(value_of_e) ?
            new NonEmpty<T>() { Value = value_of_e }
          :
            this.MoveNext()
        );
    }
  }

  /*
   class Decorator<I> : I {
     I source;

     foreach method M of I {
       use source to implement M
     }
   }
   */










  class AllNumbers : SafeIterable<int>
  {
    int current = 0;
    public Option<int> MoveNext()
    {
      var result = new NonEmpty<int>() { Value = current };
      current = current + 1;
      return result;
    }
  }

  class AllEvenNumbers : SafeIterable<int>
  {
    int current = 0;
    public Option<int> MoveNext()
    {
      var result = new NonEmpty<int>() { Value = current };
      current = current + 2;
      return result;
    }
  }

  class NumbersUpTo : SafeIterable<int>
  {
    public int Max;
    int current = 0;
    public Option<int> MoveNext()
    {
      var result = new NonEmpty<int>() { Value = current };
      current = current + 1;
      if (current > Max)
        return new Empty<int>();
      else
        return result;
    }
  }

  class SafeArrayIterable<T> : SafeIterable<T>
  {
    public T[] Source;
    int current = 0;

    public Option<T> MoveNext()
    {
      if (current <= Source.Length - 1)
      {
        current = current + 1;
        return new NonEmpty<T>() { Value = Source[current - 1] };
      } else
      {
        return new Empty<T>();
      }
    }
  }











  class Empty<T> : Option<T>
  {
    public U Visit<U>(Func<U> whenEmpty, Func<T, U> whenNonEmpty)
    {
      return whenEmpty();
    }

    public void Visit(Action whenEmpty, Action<T> whenNonEmpty)
    {
      whenEmpty();
    }
  }

  class NonEmpty<T> : Option<T>
  {
    public T Value;

    public U Visit<U>(Func<U> whenEmpty, Func<T, U> whenNonEmpty)
    {
      return whenNonEmpty(this.Value);
    }

    public void Visit(Action whenEmpty, Action<T> whenNonEmpty)
    {
      whenNonEmpty(this.Value);
    }
  }

  class OptionIterable<T> : Iterable<T>
  {
    public Option<T> data;
    bool seen_already = false;

    public T GetCurrent()
    {
      return data.Visit<T>(
        () => throw new Exception("Invalid call"),
        Value => Value);
    }

    public bool HasNoMoreElements()
    {
      return data.Visit<bool>(
        () => true,
        Value => false) || this.seen_already;
    }

    public void MoveToNextElement()
    {
      seen_already = true;
    }
  }

  class ArrayIterable<T> : Iterable<T>
  {
    public T[] data;
    int how_far_are_we = 0;

    public T GetCurrent()
    {
      return data[how_far_are_we];
    }

    public bool HasNoMoreElements()
    {
      return how_far_are_we > data.Length - 1;
    }

    public void MoveToNextElement()
    {
      how_far_are_we = how_far_are_we + 1;
    }
  }

  class Program
  {
    static void PrintAll(SafeIterable<int> it)
    {
      it.MoveNext().Visit(
        () => Console.WriteLine("Done."),
        v =>
        {
          Console.WriteLine($"The current value is {v}");
          PrintAll(it);
        });
    }

    static void Main(string[] args)
    {
      var data = new ArrayIterable<int>() { data = new[] { 1, 2, 3, 4 } };
      PrintAll(new Safeify<int>() { source = data });

      //PrintAll(new OptionIterable<int>() { data = new Empty<int>() });
      //PrintAll(new OptionIterable<int>() { data = new NonEmpty<int>() {
      //  Value = 5 } });

      var l = new List<int>() { 1, 2, 3, 4, 5 };
      // var l1 = l.Select(x => x + 1).Where(x => x > 3).OrderBy(x => x % 2 == 0 ? 1 : -1);
      var l1 =
          from x in l
          where x > 3
          orderby x % 2 == 0 ? 1 : -1
          select x + 1;
    }
  }
}