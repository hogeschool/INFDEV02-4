package com.gdx.designpatterns;

// Describes a button GUI element
public class Button extends GUIDecorator {
    public Float width;
    public Float height;
    public CustomColor color;
    public Runnable action;

    public Button(String text, Point top_left, Integer size, CustomColor color, Float width, Float height, Runnable action) {
	// TODO: Missing code
	this.width = width;
	this.height = height;
	this.color = color;
	this.action = action;
    }

    @Override
    public void draw(IDrawVisitor visitor) {
	visitor.drawButton(this);
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
	visitor.updateButton(this, dt);
    }
}
