package edu.hr.infdev024;

public abstract class GUIElementCreator {

    public abstract GUIElement instantiate(String context, Point top_left, Integer size, CustomColor color);

    public abstract GUIElement instantiate(String text, Point top_left, Integer size, CustomColor color, Float width, Float height, Runnable action);
}
