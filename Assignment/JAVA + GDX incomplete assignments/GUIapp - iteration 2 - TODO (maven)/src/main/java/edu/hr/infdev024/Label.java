package edu.hr.infdev024;

// Describes a label GUI element
import com.badlogic.gdx.graphics.Color;

public class Label implements IGUIElement {

    public String content;
    public Integer size;
    public Color color;
    public Point top_left;

    public Label(String content, Point top_left, Integer size, Color color) {
        this.size = size;
        this.color = color;
        this.top_left = top_left;
        this.content = content;
    }

    @Override
    public void draw(IDrawVisitor visitor) {
        visitor.drawLabel(this);
    }

    @Override
    public void update(IUpdateVisitor visitor, Float dt) {
        // Label doesn't require any update at this time
    }
}
