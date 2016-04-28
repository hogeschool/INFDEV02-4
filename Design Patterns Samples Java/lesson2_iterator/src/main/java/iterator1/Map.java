package iterator1;

import java.util.function.Function;

  public class Map<T, U> implements IEnumerator<U>
  {
    private IEnumerator<T> decoratedCollection;
    private Function<T, U> f;
    
    public Map(IEnumerator<T> collection, Function<T, U> f)
    {
      this.decoratedCollection = collection;
      this.f = f;
    }

    public U getCurrent()
    {
         return f.apply(decoratedCollection.getCurrent());
    }

    public boolean moveNext()
    {
      return decoratedCollection.moveNext();
    }

    public void reset()
    {
      decoratedCollection.reset();
    }
  }

