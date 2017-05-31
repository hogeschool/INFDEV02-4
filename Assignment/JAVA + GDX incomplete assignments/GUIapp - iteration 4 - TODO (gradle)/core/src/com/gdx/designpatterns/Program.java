/*
  This file contains startup logic for the application
  and does not need to be modified by the student
 */
package com.gdx.designpatterns;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;

public class Program extends ApplicationAdapter {

    private SpriteBatch batch;
    private GUIManager guiManager;
    private IDrawVisitor drawVisitor;
    private IUpdateVisitor updateVisitor;
    private Texture backgroundImage;
    private IDrawingManager drawingManager;
    private IInputManager inputManager;

    @Override
    public void create() {
        this.batch = new SpriteBatch();
        this.backgroundImage = new Texture("coffee.jpg");
        
        GUIConstructor guiConstructor = new GUIConstructor();
        this.guiManager = guiConstructor.instantiate("1", () -> {
            Gdx.app.exit();
        });
        
        this.drawingManager = new GDXDrawingAdapter(this.batch);
        this.drawVisitor = new DefaultDrawVisitor(this.drawingManager);
        
        this.inputManager = new GDXMouse();
        this.updateVisitor = new DefaultUpdateVisitor(this.inputManager);
    }

    @Override
    public void render() {
        Gdx.gl.glClearColor(1, 0, 0, 1);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

        this.batch.begin();
        this.batch.draw(this.backgroundImage, 0, 0, Gdx.graphics.getWidth(), Gdx.graphics.getHeight());
        this.guiManager.update(this.updateVisitor, Gdx.graphics.getDeltaTime());
        this.guiManager.draw(this.drawVisitor);
        this.batch.end();
    }

    @Override
    public void dispose() {
        this.batch.dispose();
        this.backgroundImage.dispose();
    }
}
