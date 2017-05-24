using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GUIapp
{
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
    }
    GuiManager GuiManager;
    DrawVisitor DrawVisitor;
    UpdateVisitor UpdateVisitor;

    protected override void Initialize()
    {
      base.Initialize();
      this.IsMouseVisible = true;
      GuiManager = new GuiManager(() => Exit());
      UpdateVisitor = new DefaultUpdateVisitor();

    }

    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      DrawVisitor = new DefaultDrawVisitor(spriteBatch, Content);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      GuiManager.Update(UpdateVisitor, (float)gameTime.ElapsedGameTime.TotalMilliseconds);
      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();
      GuiManager.Draw(DrawVisitor);
      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
