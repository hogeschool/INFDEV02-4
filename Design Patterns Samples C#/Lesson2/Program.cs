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
        IOption<T> GetNext();
    }
    public interface UnsafeIterator<T>
    {
        bool MoveNext();
        T Current { get; }
    }
    public class IOptionAdapter<T> : Iterator<T>
    {
        private IOption<T> option;
        private bool visited = false;
        public IOptionAdapter(IOption<T> option)
        {
            this.option = option;
        }
        public IOption<T> GetNext()
        {
            if (visited)
                return new None<T>();
            else
            {
                visited = true;
                return option.Visit<IOption<T>>(() => new None<T>(), t => new Some<T>(t));
            }
        }
    }
    public class UnsafeIteratorAdapter<T> : Iterator<T>
    {
        private UnsafeIterator<T> iterator;
        public UnsafeIteratorAdapter(UnsafeIterator<T> iterator)
        {
            this.iterator = iterator;
        }

        public IOption<T> GetNext()
        {
            if (iterator.MoveNext())
            {
                return new Some<T>(iterator.Current);
            }
            else return new None<T>();
        }

    }

    namespace SafeCollections
    {
        public class NaturalList : Iterator<int>
        {
            private int current = -1;

            public IOption<int> GetNext()
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
            public IOption<T> GetNext()
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
            public IOption<T> GetNext()
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
        public class NaturalList : UnsafeIterator<int>
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
        public class CircularList<T> : UnsafeIterator<T>
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
        public class Array<T> : UnsafeIterator<T>
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
        public class Map<T, U> : UnsafeIterator<U>
        {
            private UnsafeIterator<T> decoratedCollection;
            Func<T, U> f;
            public Map(UnsafeIterator<T> collection, Func<T, U> f)
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

            Iterator<int> list = new UnsafeIteratorAdapter<int>(new UnsafeCollections.NaturalList());

            //UnsafeIterator<int> elems = new NaturalList();
            //Map<int, string> mapped_elems = new Map<int, string>(elems, x => x.ToString() + " is a string now..");
            //mapped_elems.MoveNext();
            //while (true)
            //{
            //  Console.WriteLine(mapped_elems.Current);
            //  mapped_elems.MoveNext();
            //  Thread.Sleep(100);
            //}

        }
    }
}