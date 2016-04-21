/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package optionNoLambda;

class None<T, U> implements IOption<T, U> {

    public U accept(IncOptionVisitor<T, U> visitor) {
        return visitor.visit(this);
    }
}