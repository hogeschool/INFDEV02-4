package com.gdx.designpatterns;

// Describes a concrete GUIElementCreator that makes buttons
public class ButtonConstructor extends GUIElementCreator {
    @Override
    public GUIElement instantiate(String text, Point top_left, Integer size, CustomColor color) {
	return new Button(text, top_left, size, color, 40.0f, 40.0f, () -> {});
    }
    
    @Override
    public GUIElement instantiate(String text, Point top_left, Integer size, CustomColor color, Float width, Float height, Runnable action) {
        return new Button(text, top_left, size, color, width, height, action);
    }
}
