package edu.hr.infdev024;

// Describes a concrete visitor containing methods for updating each GUI element
public class DefaultUpdateVisitor implements IUpdateVisitor {

    IInputManager inputManager;

    public DefaultUpdateVisitor(IInputManager inputManager) {
        this.inputManager = inputManager;
    }

    @Override
    public void updateButton(Button element, Float dt) {
        inputManager.click().visit(() -> {
            element.color = CustomColor.WHITE;
        },
                position -> {
                    if (element.contains(position)) {
                        element.color = CustomColor.BLUE;
                        element.action.run();
                    }
                });
    }

    @Override
    public void updateLabel(Label element, Float dt) {
        // Label doesn't require any update logic at this time
    }

    @Override
    public void updateGUI(GUIManager guiManager, Float dt) {
        guiManager.elements.reset();
        while (guiManager.elements.getNext().visit(() -> false, element -> true)) {
            guiManager.elements.getCurrent().visit(() -> {
            }, element -> element.update(this, dt));
        }
    }
}
