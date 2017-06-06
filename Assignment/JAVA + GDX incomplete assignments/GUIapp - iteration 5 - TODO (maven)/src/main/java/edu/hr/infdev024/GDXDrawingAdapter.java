package edu.hr.infdev024;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.Color;

// Describes the concrete libgdx drawing functionality
public class GDXDrawingAdapter implements IDrawingManager {

    private SpriteBatch batch;
    private Texture textureWhite;
    private BitmapFont font;

    public GDXDrawingAdapter(SpriteBatch spriteBatch) {
        this.batch = spriteBatch;
        this.textureWhite = new Texture("white_pixel.jpg");
        this.font = new BitmapFont();
    }

    // Converts a framework-agnostic color into a libgdx color
    private Color convertColor(CustomColor color) {
        switch (color) {
            case WHITE:
                return Color.WHITE;

            case BLACK:
                return Color.BLACK;

            case BLUE:
                return Color.BLUE;

            default:
                return Color.RED;
        }
    }

    @Override
    public void drawRectangle(Point top_left, Float width, Float height, CustomColor color) {
        this.batch.draw(
                this.textureWhite,
                top_left.getX(),
                Gdx.graphics.getHeight() - (top_left.getY() + height),
                width, height);
    }

    @Override
    public void drawString(String text, Point top_left, Integer size, CustomColor color) {
        this.font.setColor(convertColor(color));
        this.font.draw(
                this.batch,
                text,
                top_left.getX(),
                Gdx.graphics.getHeight() - (top_left.getY() + size));
    }
}
