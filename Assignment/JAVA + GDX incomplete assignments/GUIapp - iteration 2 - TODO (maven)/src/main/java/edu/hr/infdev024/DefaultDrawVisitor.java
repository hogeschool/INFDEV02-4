package edu.hr.infdev024;

// Describes a concrete visitor containing methods for drawing each GUI element
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;

public class DefaultDrawVisitor implements IDrawVisitor {

    private SpriteBatch batch;
    private Texture textureWhite;
    private BitmapFont font;

    public DefaultDrawVisitor(SpriteBatch spriteBatch) {
        this.batch = spriteBatch;
        this.textureWhite = new Texture("white_pixel.jpg");
        this.font = new BitmapFont();
    }

    @Override
    public void drawButton(Button element) {
        this.batch.draw(
                this.textureWhite,
                element.top_left.getX(),
                Gdx.graphics.getHeight() - (element.top_left.getY() + element.height),
                element.width, element.height);
        element.label.draw(this);
    }

    @Override
    public void drawLabel(Label element) {
        this.font.setColor(element.color);
        this.font.draw(
                this.batch,
                element.content,
                element.top_left.getX(),
                Gdx.graphics.getHeight() - (element.top_left.getY() + element.size));
    }

    @Override
    public void drawGUI(GUIManager guiManager) {
        guiManager.elements.reset();
        while //TODO: ADD MISSING CODE HERE
                 {
            guiManager.elements.getCurrent().visit(() -> {
            }, element -> element.draw(this));
        }
    }
}
