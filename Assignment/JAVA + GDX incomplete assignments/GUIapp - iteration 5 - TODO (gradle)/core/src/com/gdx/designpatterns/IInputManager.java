package com.gdx.designpatterns;

// This interface should be implemented by all clickable GUI elements
interface IInputManager {
    IOption<Point> click();
}
