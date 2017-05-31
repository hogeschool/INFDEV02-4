package com.gdx.designpatterns;

/*
  This interface may be used to implement specific 
  logic for variety of shapes
*/
interface IDrawingManager {
    void drawRectangle(Point top_left, Float width, Float height, CustomColor color);
    void drawString(String text, Point top_left, Integer size, CustomColor color);
}
