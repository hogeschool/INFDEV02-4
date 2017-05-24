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
    InputManager InputManager;
    DrawingManager DrawingManager;
    DrawVisitor DrawVisitor;
    UpdateVisitor UpdateVisitor;

    protected override void Initialize()
    {
      base.Initialize();
      this.IsMouseVisible = true;
      GuiManager = new GuiManager(() => Exit());
      InputManager = new MonogameMouse();
      UpdateVisitor = new DefaultUpdateVisitor(InputManager);

    }

    protected override void LoadContent()
    {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      DrawingManager = new MonogameDrawingAdapter(spriteBatch, Content);
      DrawVisitor = new DefaultDrawVisitor(DrawingManager);
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
