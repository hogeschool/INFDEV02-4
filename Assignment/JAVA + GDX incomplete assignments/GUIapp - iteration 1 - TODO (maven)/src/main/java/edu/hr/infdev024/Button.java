package edu.hr.infdev024;

// Describes a button GUI element
import com.badlogic.gdx.graphics.Color;

public class Button implements IGUIElement {

    public Float width;
    public Float height;
    public Color color;
    public Label label;
    public Point top_left;
    public Runnable action;

    public Button(String text, Point top_left, Integer size, Color color, Float width, Float height, Runnable action) {
        this.width = width;
        this.height = height;
        this.color = color;
        this.top_left = top_left;
        this.label = new Label(text, top_left, size, color);
        this.action = action;
    }

    @Override
    public void draw(IDrawVisitor visitor) {
        //TODO: ADD MISSING CODE HERE
    }

    // Used to check if a point is inside of the bounds of this button
    public Boolean contains(Point point) {
        return point.getX() > this.top_left.getX()
                && point.getY() > this.top_left.getY()
                && point.getX() < this.top_left.getX() + this.width
                && point.getY() < this.top_left.getY() + this.height;
    }

    @Override
    public void update(IUpdateVisitor visitor, Float dt) {
        //TODO: ADD MISSING CODE HERE
    }
}
