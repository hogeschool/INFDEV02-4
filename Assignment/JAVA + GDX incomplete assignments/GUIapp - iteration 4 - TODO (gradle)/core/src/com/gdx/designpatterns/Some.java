package com.gdx.designpatterns;

import java.util.function.Consumer;
import java.util.function.Function;
import java.util.function.Supplier;

// Class of type T that contains concrete data
public class Some<T> implements IOption <T> {
    T value;

    public Some(T value) {
        this.value = value;
    }

    @Override
    public <U> U visit(Supplier<U> onNone, Function<T, U> onSome) {
        return onSome.apply(this.value);
    }
    
    @Override
    public void visit(Runnable onNone, Consumer<T> onSome) {
        onSome.accept(this.value);
    }
}
