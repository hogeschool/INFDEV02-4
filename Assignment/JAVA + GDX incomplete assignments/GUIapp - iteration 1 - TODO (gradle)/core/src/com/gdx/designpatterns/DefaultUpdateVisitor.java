package com.gdx.designpatterns;

// Describes a concrete visitor containing methods for updating each GUI element
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.graphics.Color;

public class DefaultUpdateVisitor implements IUpdateVisitor {

    public void updateButton(Button element, Float dt) {
       if //TODO: ADD MISSING CODE HERE
       //TODO: ADD MISSING CODE HERE
        } else {
            element.color = Color.WHITE;
        }
    }

    @Override
    public void updateLabel(Label element, Float dt) {
    }

    @Override
    public void updateGUI(GUIManager guiManager, Float dt) {
       //TODO: ADD MISSING CODE HERE
    }
}
