package com.gdx.designpatterns;

import com.badlogic.gdx.graphics.Color;
import java.util.ArrayList;
import java.util.List;

/*
  Describes a class controlling the update and
  draw behavior all GUI elements
 */
public class GUIManager implements IUpdatable, IDrawable {

//TODO: MISSING CODE
    
    public GUIManager(Runnable exitAction) {
        elements = new ListIterator<IGUIElement>();
        elements.add(new Label("Hi Ahmed!", new Point(0.0f, 0.0f), 10, Color.WHITE));
        elements.add(new Button("Click me!", new Point(0.0f, 100.0f), 10, Color.BLACK, 100.0f, 30.0f,
                () -> {
                    System.out.println("Action triggered");
                    this.elements = new ListIterator<IGUIElement>();
                    this.elements.add(new Button("Exit", new Point(0.0f, 0.0f), 10, Color.BLACK, 100.0f, 30.0f,
                            () -> {
                                exitAction.run();
                            }
                    ));
                }));
    }

    @Override
    public void draw(IDrawVisitor visitor) {
        visitor.drawGUI(this);
    }

    @Override
    public void update(IUpdateVisitor visitor, Float dt) {
        visitor.updateGUI(this, dt);
    }
}
