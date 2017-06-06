package com.gdx.designpatterns;

// Describes a concrete GUIElementCreator that makes labels
public class LabelConstructor extends GUIElementCreator {
    @Override
    public GUIElement instantiate(String text, Point top_left, Integer size, CustomColor color) {
	return new Label(text, top_left, size, color);
    }
    
    @Override
    public GUIElement instantiate(String text, Point top_left, Integer size, CustomColor color, Float width, Float height, Runnable action) {
	return new Label(text, top_left, size, color);
    }
}
