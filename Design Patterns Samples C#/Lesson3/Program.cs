using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lesson2;
using OptionLambda;

namespace Lesson3
{

  public class OptionAdapter<T> : Iterator<T>
  {
    private Option<T> option;
    private bool visited = false;
    public OptionAdapter(Option<T> option)
    {
      this.option = option;
    }
    public Option<T> GetNext()
    {
      if (visited)
        return new None<T>();
      else
      {
        visited = true;
        return option.Visit<Option<T>>(() => new None<T>(), t => new Some<T>(t));
      }
    }
  }
  public class MakeSafe<T> : Iterator<T>
  {
    private TraditionalIterator<T> iterator;
    public MakeSafe(TraditionalIterator<T> iterator)
    {
      this.iterator = iterator;
    }

    public Option<T> GetNext()
    {
      if (iterator.MoveNext())
      {
        return new Some<T>(iterator.Current);
      }
      else return new None<T>();
    }

  }
  public class MakeUnsafe<T> : TraditionalIterator<T>
  {
    private Iterator<T> iterator;
    public MakeUnsafe(Iterator<T> iterator)
    {
      this.iterator = iterator;
    }
    private T _current;
    public T Current
    {
      get
      {
        return _current;
      }
    }
    public bool MoveNext()
    {
      Option<T> opt = iterator.GetNext();
      if (opt.Visit(() => false, _ => true))
      {
        _current = opt.Visit(() => default(T), v => v);
        return true;
      }
      return false;
    }
  }




  class Program
  {
    static void Main(string[] args)
    {
    }
  }
}
