package com.gdx.designpatterns;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;

/*
  A concrete implementation of IInputManager
  Contains specific input logic of libgdx
*/
public class GDXMouse implements IInputManager {
    public IOption<Point> click() {
        if(Gdx.input.isButtonPressed(Input.Buttons.LEFT)) {
            return new Some<Point>(new Point(
                (float)Gdx.input.getX(),
                (float)Gdx.input.getY()));
        }
        else {
            return new None<Point>();
        }
    }
}
