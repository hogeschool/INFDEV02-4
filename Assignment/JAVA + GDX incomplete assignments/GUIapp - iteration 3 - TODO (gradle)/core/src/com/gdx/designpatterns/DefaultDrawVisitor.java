package com.gdx.designpatterns;

// Describes a concrete visitor containing methods for drawing each GUI element

public class DefaultDrawVisitor implements IDrawVisitor {
    private IDrawingManager drawingManager;

    public DefaultDrawVisitor(IDrawingManager drawingManager) {
        this.drawingManager = drawingManager;
    }

    @Override
    public void drawButton(Button element) {
 	this.drawingManager.drawRectangle(element.top_left, element.width, element.height, element.color);
	element.label.draw(this);
    }

    @Override
    public void drawLabel(Label element) {
	this.drawingManager.drawString(element.content, element.top_left, element.size, element.color);
    }

    @Override
    public void drawGUI(GUIManager guiManager) {
        guiManager.elements.reset();
        while (guiManager.elements.getNext().visit(() -> false, element -> true)) {
            guiManager.elements.getCurrent().visit(() -> {
            }, element -> element.draw(this));
        }
    }
}