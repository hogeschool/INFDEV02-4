package iterator1.safeCollections;


import visitor.optionLambda.IOption;

public interface Iterator<T> {
  IOption<T> getNext();
}
