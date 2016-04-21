using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionNoLambda
{
  interface IOptionVisitor<T, U>
  {
    U Visit(Some<T, U> option);
    U Visit(None<T, U> option);
  }
  class IncOptionVisitor<T, U> : IOptionVisitor<T, U>
  {
    Func<T, U> onSome;
    Func<U> onNone;
    public IncOptionVisitor(Func<T, U> onSome, Func<U> onNone) {
      this.onSome = onSome;
      this.onNone = onNone;
    }

    public U Visit(None<T, U> option)
    {
      return onNone();
    }

    public U Visit(Some<T, U> option)
    {
      return onSome(option.value);
    }

  }
  interface Option<T, U> { U Accept(IncOptionVisitor<T, U> visitor); }
  class Some<T, U> : Option<T, U>
  {
    public T value;
    public Some(T value) { this.value = value; }
    public U Accept(IncOptionVisitor<T, U> visitor)
    {
      return visitor.Visit(this);
    }
  }
  class None<T, U> : Option<T, U>
  {
    public U Accept(IncOptionVisitor<T, U> visitor)
    {
      return visitor.Visit(this);
    }
  }
}
namespace OptionLambda
{
  interface Option<T>
  {
    U Visit<U>(Func<U> onNone, Func<T, U> onSome);
  }
  class Some<T> : Option<T>
  {
    T value;
    public Some(T value) { this.value = value; }
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
}
namespace Number
{ 
  interface INumberVisitor
  {
    void Visit(MyFloat number);
    void Visit(MyInt number);
  }
  class NumberVisitor : INumberVisitor
  {
    public void Visit(MyFloat number) { Console.WriteLine("Found a float and now?"); }
    public void Visit(MyInt number) { Console.WriteLine("Found an int and now?!"); }
  }
  interface Number { void Visit(INumberVisitor visitor); }
  class MyInt : Number
  {
    void Number.Visit(INumberVisitor visitor)
    {
      visitor.Visit(this);
    }
  }
  class MyFloat : Number
  {
    void Number.Visit(INumberVisitor visitor)
    {
      visitor.Visit(this);
    }
  }

}
namespace MusicLibrary
{
  interface IMusicLibraryVisitor
  {
    void Visit(HeavyMetal number);
    void Visit(Jazz number);
  }
  class MusicLibraryVisitor : IMusicLibraryVisitor
  {
    public List<HeavyMetal> heavyMetal = new List<HeavyMetal>();
    public List<Jazz> jazz = new List<Jazz>();

    public void Visit(HeavyMetal song) { heavyMetal.Add(song); }
    public void Visit(Jazz song) { jazz.Add(song); }
  }
  interface Song { void Visit(IMusicLibraryVisitor visitor); }
  class HeavyMetal : Song
  {
    string title;
    public HeavyMetal(string title) { this.title = title; }
    public void Visit(IMusicLibraryVisitor visitor)
    {
      visitor.Visit(this);
    }
  }
  class Jazz : Song
  {
    string title;
    public Jazz(string title) { this.title = title; }
    public void Visit(IMusicLibraryVisitor visitor)
    {
      visitor.Visit(this);
    }
  }
}

namespace ConsoleApplication1
{
  using Number;
  using OptionNoLambda;
  using MusicLibrary;
  using OptionLambda;
  class Program
  {
    static void Main(string[] args)
    {

      //NumberVisitor n_visitor = new NumberVisitor();
      //Number n = new MyInt();
      //n.Visit(n_visitor);

      //MusicLibraryVisitor music_library_visitor = new MusicLibraryVisitor();
      //List<Song> songs = new List<Song>();
      //songs.Add(new HeavyMetal("Hallowed Be Thy Name"));
      //songs.Add(new Jazz("Autumn Leaves"));
      //songs.Add(new HeavyMetal("War Pigs"));
      //foreach (var song in songs) { song.Visit(music_library_visitor); }
      //Console.WriteLine("Amount of heavy metal music: " + music_library_visitor.heavyMetal.Count);
      //Console.WriteLine("Amount of jazz music: " + music_library_visitor.jazz.Count);

      //OPTION VISITOR version 1
      //IncOptionVisitor<int, int> opt_visitor = new IncOptionVisitor<int, int>(i => i + 1, () => { throw new Exception("Expecting a value.."); });
      //Option<int, int> opt = new Some<int, int>(5);
      //int res = opt.Accept(opt_visitor);
      //Console.WriteLine(res);

      //OPTION VISITOR version 2
      Option<int> number = new Some<int>(5);
      int inc_number = number.Visit(() => { throw new Exception("Expecting a value.."); }, i => i + 1);
      Console.WriteLine(inc_number);
      number = new None<int>();
      inc_number = number.Visit(() => { throw new Exception("Expecting a value.."); }, i => i + 1);
      Console.WriteLine(inc_number);




    }
  }
}
