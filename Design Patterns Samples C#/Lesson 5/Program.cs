using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptionLambda;
using Lesson2;

namespace Lesson_5
{
  using Example1;
  using Example2;
  namespace Example1
  {
    interface Decorator<T> : Iterator<T> { }

    class Map<T, U> : Decorator<U>
    {
      Func<T, U> t;
      protected Iterator<T> iterator;
      public Map(Iterator<T> iterator, Func<T, U> t)
      {
        this.iterator = iterator;
        this.t = t;
      }

      public Option<U> GetNext()
      {
        return iterator.GetNext().Visit<Option<U>>(() => new None<U>(), value => new Some<U>(t(value)));
      }
    }


    class Filter<T> : Decorator<T>
    {
      Func<T, bool> f;
      protected Iterator<T> iterator;
      public Filter(Iterator<T> iterator, Func<T, bool> f)
      {
        this.iterator = iterator;
        this.f = f;
      }

      public Option<T> GetNext()
      {
        Option<T> next = iterator.GetNext();
        if (next.Visit(() => true, _ => false))
          return next;
        return next.Visit(() => next, value =>
                          {
                            if (f(value))
                              return next;
                            else
                              return GetNext();
                          });
      }
    }
  }
  namespace Example2
  {
    public class Customer
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Address { get; set; }
    }
    public interface IRepository<T>
    {
      void Add(T entity);
      void Delete(T entity);
      void Update(T entity);
      IEnumerable<T> GetAll();
      T GetById(int id);
    }
    class Repository<T> : IRepository<T>
    {
      public void Add(T entity)
      {
        Console.WriteLine("Adding {0}", entity);
      }

      public void Delete(T entity)
      {
        Console.WriteLine("Deleting {0}", entity);
      }

      public IEnumerable<T> GetAll()
      {
        Console.WriteLine("Returning all...");
        return null;
      }

      public T GetById(int id)
      {
        Console.WriteLine("Getting entity with id: {0}", id);
        return default(T);
      }

      public void Update(T entity)
      {
        Console.WriteLine("Updating {0}", entity);
      }
    }

    class LoggerRepository<T> : IRepository<T>
    {
      private readonly IRepository<T> repository;
      public LoggerRepository(IRepository<T> repository)
      {
        this.repository = repository;
      }
      public void Log(string msg, object arg = null)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(msg, arg);
        Console.ResetColor();
      }
      public void Add(T entity)
      {
        Log("In decorator: before adding {0}", entity);
        repository.Add(entity);
        Log("In decorator: after adding {0}", entity);
      }

      public void Delete(T entity)
      {
        Log("In decorator: before deleting {0}", entity);
        repository.Delete(entity);
        Log("In decorator: after deleting {0}", entity);
      }

      public IEnumerable<T> GetAll()
      {
        Log("In decorator: before get all");
        var res = repository.GetAll();
        Log("In decorator: after get all");
        return res;
      }

      public T GetById(int id)
      {
        Log("In decorator: before get by id {0}", id);
        var res = repository.GetById(id);
        Log("In decorator: after get by id {0}", id);
        return res;
      }

      public void Update(T entity)
      {
        Log("In decorator: before update {0}", entity);
        repository.Update(entity);
        Log("In decorator: after update {0}", entity);
      }
    }
  }
  class Program
  {
    public interface I { void X(); }
    abstract class A : I
    {
      public abstract void X();
      public void Test()
      {
        Console.WriteLine("A");
      }
    }
    class B : A
    {
      public new void Test()
      {
        base.Test();
        Console.WriteLine("B");
      }

      public override void X()
      {
        throw new NotImplementedException();
      }
    }

    static void Main(string[] args)
    {

      B b = new B();
      b.Test();
      ///Example 1 test
      //Iterator<int> iterator = new Lesson2.SafeCollections.NaturalList();
      //Iterator<int> iterator = new Filter<int>(new Lesson2.SafeCollections.NaturalList(), value => value % 2 == 0);
      Iterator<string> iterator = new Map<int, string>(new Filter<int>(new Lesson2.SafeCollections.NaturalList(), value => value % 2 == 0), elem => elem + " :)");

      for (int i = 0; i < 10; i++)
      {
        iterator.GetNext().Visit<object>(() => { Console.WriteLine("Done"); return null; }, v => { Console.WriteLine(v); return null; });
      }

      ///Example 2 test
      Console.WriteLine("***\r\nBegin program\r\n");

      //IRepository<Customer> customerRepository = new Repository<Customer>();
      IRepository<Customer> customerRepository = new LoggerRepository<Customer>(new Repository<Customer>());

      var customer = new Customer
      {
        Id = 1,
        Name = "Customer 1",
        Address = "Address 1"
      };

      customerRepository.Add(customer);
      customerRepository.Update(customer);
      customerRepository.Delete(customer);

      Console.WriteLine("\r\nEnd program \r\n***");

    }
  }
}
