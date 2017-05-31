package com.gdx.designpatterns;

/*
  This interface may be used for describing the specifics
  of how each GUI element should be updated
*/
interface IUpdateVisitor {
    void updateButton(Button element, Float dt);
    void updateLabel(Label element, Float dt);
    void updateGUI(GUIManager element, Float dt);
}
