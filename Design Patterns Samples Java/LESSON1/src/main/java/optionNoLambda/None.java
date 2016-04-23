/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package optionNoLambda;

class None<T> implements IOption<T> {

    public <U> U visit(IOptionVisitor<T, U> visitor) {
        return visitor.visit(this);
    }
}