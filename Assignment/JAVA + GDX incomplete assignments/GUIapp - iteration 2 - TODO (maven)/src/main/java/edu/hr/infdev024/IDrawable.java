package edu.hr.infdev024;

// Any drawable object should implement this interface
interface IDrawable {

    void draw(IDrawVisitor visitor);
}
