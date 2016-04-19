using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
  interface Option<T>
  {
    U Visit<U>(Func<U> onNone, Func<T, U> onSome);
  }
  class Some<T> : Option<T>
  {
    T value;
    public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
    {
      return onSome(value);
    }
  }
  class None<T> : Option<T>
  {
    public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
    {
      return onNone();
    }
  }
  class Program
  {
    static void Main(string[] args)
    {
      Option<int> number = new None<int>();
      var inc_number = number.Visit(() => { throw new Exception("Expexting a value.."); }, i => i + 1);
    }
  }
}
