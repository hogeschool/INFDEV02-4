package com.gdx.designpatterns;

// Describes a concrete visitor containing methods for drawing each GUI element
public class DefaultDrawVisitor implements IDrawVisitor {
    private IDrawingManager drawingManager; // All elements are drawn by a specific drawing manager

    public DefaultDrawVisitor(IDrawingManager drawingManager) {
	this.drawingManager = drawingManager;
    }
    
    public void drawButton(Button element) {
	// TODO: Missing code
    }

    public void drawLabel(Label element) {
	this.drawingManager.drawString(element.content, element.top_left, element.size, element.color);
    }

    public void drawGUI(GUIManager guiManager) {
        guiManager.elements.reset();
        while(guiManager.elements.getNext().visit(
                () -> false,
                element -> {
                    element.draw(this);
                    return true;
                })) {
            // Empty loop body; only the condition matters
        }
    }
}
