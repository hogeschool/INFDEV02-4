/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package iterator1.unsafeCollections;

 public interface ITraditionalIterator<T>
  {
    boolean moveNext();
    T getCurrent();
  }
