package com.gdx.designpatterns;

// Describes a class with a factory method for creating menu screens
public abstract class GUIMenuCreator {
    public abstract GUIManager instantiate(String option, Runnable exitAction);
}
