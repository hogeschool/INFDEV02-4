package com.gdx.designpatterns;

public class GUIConstructor extends GUIMenuCreator {
    @Override
    public GUIManager instantiate(String option, Runnable exitAction) {
	GUIManager guiManager = new GUIManager();

	switch(option) {
	default:
	    GUIElementCreator buttonConstructor = new ButtonConstructor();
	    GUIElementCreator labelConstructor = new LabelConstructor();
	    guiManager.elements = new ListIterator<GUIElement>();
	    guiManager.elements.add(labelConstructor.instantiate(
		"Hi Ahmed!", new Point(0.0f, 0.0f), 10, CustomColor.WHITE));
	    
	    guiManager.elements.add(buttonConstructor.instantiate(
		"Click me!", new Point(0.0f, 100.0f), 10, CustomColor.BLACK, 100.0f, 30.0f,
		() -> {
                    System.out.println("Action triggered");
		    guiManager.elements = new ListIterator<GUIElement>();
		    guiManager.elements.add(buttonConstructor.instantiate(
		        "Exit", new Point(0.0f, 0.0f), 10, CustomColor.BLACK, 100.0f, 30.0f,
			() -> {
			    exitAction.run();
			}
		    ));
		}
	    ));
	    
	    break;
	}

	return guiManager;
    }
}
