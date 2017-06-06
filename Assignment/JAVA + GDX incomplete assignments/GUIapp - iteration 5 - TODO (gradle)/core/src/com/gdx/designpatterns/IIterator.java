package com.gdx.designpatterns;

// Describes a common interface for iterating over collections
interface IIterator<T> {
    IOption<T> getNext();
    void reset();
}
