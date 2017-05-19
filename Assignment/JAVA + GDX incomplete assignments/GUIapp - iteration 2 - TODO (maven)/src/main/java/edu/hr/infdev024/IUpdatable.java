package edu.hr.infdev024;

// Any updatable GUI element should implement this interface
interface IUpdatable {

    void update(IUpdateVisitor visitor, Float dt);
}
