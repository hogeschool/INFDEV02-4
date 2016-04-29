/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package iterator1.safeCollections;

import visitor.optionLambda.IOption;

  public interface Iterator<T>
  {
    IOption<T> getNext();
  }
