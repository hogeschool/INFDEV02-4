package com.gdx.designpatterns;

// Describes a location in terms of X and Y coordinates
public class Point {
    private Float x;
    private Float y;
    
    public Point(Float x, Float y) {
	this.x = x;
	this.y = y;
    }

    public Float getX() {
	return this.x;
    }

    public Float getY() {
	return this.y;
    }

    public void setX(Float x) {
	this.x = x;
    }

    public void setY(Float y) {
	this.y = y;
    }
}
