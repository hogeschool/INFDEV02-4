package com.gdx.designpatterns;

// Any drawable object should implement this interface
interface IDrawable {
    void draw(IDrawVisitor visitor);
}
