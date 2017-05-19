package edu.hr.infdev024;

import com.badlogic.gdx.graphics.Color;
import java.util.ArrayList;
import java.util.List;

/*
  Describes a class controlling the update and
  draw behavior all GUI elements
 */
public class GUIManager implements IUpdatable, IDrawable {

    public List<IGUIElement> elements;

    public GUIManager(Runnable exitAction) {
        this.elements = new ArrayList<IGUIElement>();
        elements.add(new Label("Hi Ahmed!", new Point(0.0f, 0.0f), 10, Color.WHITE));
        elements.add(new Button("Click me!", new Point(0.0f, 100.0f), 10, Color.BLACK, 100.0f, 30.0f,
                () -> {
                    System.out.println("Action triggered");
                    this.elements = new ArrayList();
                    this.elements.add(new Button("Exit", new Point(0.0f, 0.0f), 10, Color.BLACK, 100.0f, 30.0f,
                            () -> {
                                exitAction.run();
                            }
                    ));
                }));
    }

    public void draw(IDrawVisitor visitor) {
        visitor.drawGUI(this);
    }

    public void update(IUpdateVisitor visitor, Float dt) {
        //TODO: ADD MISSING CODE HERE
    }
}
