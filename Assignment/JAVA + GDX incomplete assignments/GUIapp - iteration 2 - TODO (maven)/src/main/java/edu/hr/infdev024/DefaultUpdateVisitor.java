package edu.hr.infdev024;

// Describes a concrete visitor containing methods for updating each GUI element
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.graphics.Color;

public class DefaultUpdateVisitor implements IUpdateVisitor {

    @Override
    public void updateButton(Button element, Float dt) {
        if (Gdx.input.isButtonPressed(Input.Buttons.LEFT)) {
            if (element.
                    contains(new Point(
                            (float) Gdx.input.getX(),
                            (float) Gdx.input.getY()
                    ))) {
                element.color = Color.BLUE;
                element.action.run();
            }
        } else {
            element.color = Color.WHITE;
        }
    }

    @Override
    public void updateLabel(Label element, Float dt) {
    }

    @Override
    public void updateGUI(GUIManager guiManager, Float dt) {
        guiManager.elements.reset();
        while //TODO: ADD MISSING CODE HERE
    }
}
