using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionNoLambda
{
  interface IOptionVisitor<T, U>
  {
    U onSome(T value);
    U onNone();
  }
  interface Option<T> { U Visit<U>(IOptionVisitor<T, U> visitor); }
  class Some<T> : Option<T>
  {
    public T value;
    public Some(T value) { this.value = value; }
    public U Visit<U>(IOptionVisitor<T, U> visitor)
    {
      return visitor.onSome(this.value);
    }
  }
  class None<T> : Option<T>
  {
    public U Visit<U>(IOptionVisitor<T, U> visitor)
    {
      return visitor.onNone();
    }
  }
  class PrettyPrinterIntIOptionVisitor : IOptionVisitor<int, string>
  {
    public string onNone()
    {
      return "I am nothing...";
    }

    public string onSome(int value)
    {
      return value.ToString();
    }
  }
  class LambdaIOptionVisitor<T, U> : IOptionVisitor<T, U>
  {
    Func<T, U> _onSome;
    Func<U> _onNone;
    public LambdaIOptionVisitor(Func<T, U> onSome, Func<U> onNone)
    {
      this._onSome = onSome;
      this._onNone = onNone;
    }
    public U onNone()
    {
      return onNone();
    }

    public U onSome(T value)
    {
      return onSome(value);
    }
  }
}
namespace OptionLambda
{
  public interface IOption<T>
  {
    U Visit<U>(Func<U> onNone, Func<T, U> onSome);
  }
  public class Some<T> : IOption<T>
  {
    T value;
    public Some(T value) { this.value = value; }
    public U Visit<U>(Func<U> onNone, Func<T, U> onSome)
    {
      return onSome(value);
    }
  }
  public class None<T> : IOption<T>
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
  //using OptionLambda;
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
      //IOptionVisitor<int, int> opt_visitor = new LambdaIOptionVisitor<int, int>(i => i + 1, () => { throw new Exception("Expecting a value..."); });
      //Option<int> opt = new Some<int>(5);
      //int res = opt.Visit(opt_visitor);
      //Console.WriteLine(res);

      //OPTION VISITOR version 2
      //Option<int> number = new Some<int>(5);
      //int inc_number = number.Visit(() => { throw new Exception("Expecting a value..."); }, i => i + 1);
      //Console.WriteLine(inc_number);
      //number = new None<int>();
      //inc_number = number.Visit(() => { throw new Exception("Expecting a value..."); }, i => i + 1);
      //Console.WriteLine(inc_number);




    }
  }
}
