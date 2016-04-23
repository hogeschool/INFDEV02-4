/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package optionNoLambda;



public interface IOptionVisitor<T, U> {

    U visit(Some<T> option);

    U visit(None<T> option);
}
