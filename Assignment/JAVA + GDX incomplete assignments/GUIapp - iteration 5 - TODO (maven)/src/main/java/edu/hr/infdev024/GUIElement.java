package edu.hr.infdev024;

// The base class of each GUI element
public abstract class GUIElement implements IDrawable, IUpdatable {

    public Point top_left;

    public GUIElement(Point top_left) {
        this.top_left = top_left;
    }

    @Override
    public abstract void draw(IDrawVisitor visitor);

    @Override
    public abstract void update(IUpdateVisitor visitor, Float dt);
}
