package com.gdx.designpatterns;



public class GUIConstructor extends GUIMenuCreator {
    @Override
    public GUIManager instantiate(String option, Runnable exitAction) {
	GUIManager guiManager = new GUIManager();

       //TODO: ADD MISSING CODE HERE

	return guiManager;
    }
}
