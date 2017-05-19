package edu.hr.infdev024;

// Describes a concrete visitor containing methods for drawing each GUI element
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;

public class DefaultDrawVisitor //TODO: MISSING CODE HERE
{

    private SpriteBatch batch;
    private Texture textureWhite;
    private BitmapFont font;

    public DefaultDrawVisitor(SpriteBatch spriteBatch) {
        this.batch = spriteBatch;
        this.textureWhite = new Texture("white_pixel.jpg");
        this.font = new BitmapFont();
    }

    public void drawButton(Button element) {
        //TODO: ADD MISSING CODE HERE
    }

    public void drawLabel(Label element) {
        this.font.setColor(element.color);
        this.font.draw(
                this.batch,
                element.content,
                element.top_left.getX(),
                Gdx.graphics.getHeight() - (element.top_left.getY() + element.size));
    }

    public void drawGUI(GUIManager guiManager) {
        //TODO: ADD MISSING CODE HERE
    }
}
