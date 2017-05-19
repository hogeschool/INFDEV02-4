package edu.hr.infdev024;

/*
  This interface may be used for describing the specifics
  of how each GUI element should be drawn
 */
interface IDrawVisitor {

    void drawButton(Button element);

    void drawLabel(Label element);

    void drawGUI(GUIManager gui);
}
