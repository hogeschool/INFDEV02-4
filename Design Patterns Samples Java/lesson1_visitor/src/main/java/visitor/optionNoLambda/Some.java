/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package visitor.optionNoLambda;

public class Some<T> implements IOption<T> {

    private T value;

    public Some(T value) {
        this.value = value;
    }

    public <U> U visit(IOptionVisitor<T, U> visitor) {
        return visitor.onSome(value);
    }
    
    public T getValue() {
        return this.value;
    }
    
    public Boolean isNone() {
        return false;
    }
}