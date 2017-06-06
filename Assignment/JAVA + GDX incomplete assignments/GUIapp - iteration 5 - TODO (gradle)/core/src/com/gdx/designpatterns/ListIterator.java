package com.gdx.designpatterns;

import java.util.ArrayList;

// Describes a concrete iterator for array lists
public class ListIterator<T> implements IIterator<T> {
    private ArrayList<T> list;
    private int current;

    public ListIterator() {
	this.current = -1;
	this.list = new ArrayList<T>();
	reset();
    }

    // Add an element
    public void add(T item) {
	this.list.add(item);
    }

    // Get the next element in the collection if it exists
    public IOption<T> getNext() {
	this.current++;
	if(this.current >= this.list.size()) {
	    return new None<T>();
	}
	else {
	    return new Some<T>(this.list.get(this.current));
	}
    }

    // Resets the iterator
    public void reset() {
	this.current = -1;
    }
}
