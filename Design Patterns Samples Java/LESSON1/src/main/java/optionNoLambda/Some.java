/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package optionNoLambda;

public class Some<T, U> implements IOption<T, U> {

    public T value;

    public Some(T value) {
        this.value = value;
    }

    public U accept(IncOptionVisitor<T, U> visitor) {
        return visitor.visit(this);
    }
}