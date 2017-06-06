package com.gdx.designpatterns;

// Describes a label GUI element
public class Label extends GUIElement {
    public String content;
    public Integer size;
    public CustomColor color;

    public Label(String content, Point top_left, Integer size, CustomColor color) {
	super(top_left);
	this.size = size;
	this.color = color;
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
