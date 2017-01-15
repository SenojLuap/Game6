using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game6 {
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public partial class Game1 : Game {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    RenderTarget2D renderTarget;

    public KeyboardState CurrentKeyboardState { get; set; }

    public List<Plant> Plants { get; set; }
    
    public Game1() {
      graphics = new GraphicsDeviceManager(this);
      graphics.PreferredBackBufferWidth =
	Convert.ToInt32(Constants.RenderWidth * Constants.RenderScale);
      graphics.PreferredBackBufferHeight =
	Convert.ToInt32(Constants.RenderHeight * Constants.RenderScale);
      graphics.ApplyChanges();
      Content.RootDirectory = "Content/Images";

      CreatePlayer();
    }
    
    /// <summary>
    /// Initialize
    /// </summary>
    protected override void Initialize() {
      base.Initialize();

      renderTarget = new RenderTarget2D(GraphicsDevice,
					Constants.RenderWidth,
					Constants.RenderHeight,
					false,
					GraphicsDevice.PresentationParameters.BackBufferFormat,
					DepthFormat.Depth24);
      Plants = new List<Plant>();
    }
    
    /// <summary>
    /// Load Content
    /// </summary>
    protected override void LoadContent() {
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);
      if (Player != null) Player.Initialize(Content);
      // TODO: use this.Content to load your game content here
    }
    
    /// <summary>
    /// Unload content
    /// </summary>
    protected override void UnloadContent() {
      // TODO: Unload any non ContentManager content here
    }

    /// <summary>
    /// Game update tick
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Update(GameTime gameTime) {
      
      CurrentKeyboardState = Keyboard.GetState();
      
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
	  || CurrentKeyboardState.IsKeyDown(Keys.Escape))
        Exit();
      
      if (Player != null) Player.Update(this, gameTime);
      foreach (var plant in Plants) {
	plant.Update(this, gameTime);
      }

      base.Update(gameTime);
    }

    /// <summary>
    /// Draw.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime) {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      GraphicsDevice.SetRenderTarget(renderTarget);
      SpriteBatch preBatch = new SpriteBatch(GraphicsDevice);
      preBatch.Begin(sortMode: SpriteSortMode.FrontToBack);
      
      if (Player != null) Player.Draw(preBatch);
      foreach (var plant in Plants) {
	plant.Draw(preBatch);
      }
      
      preBatch.End();
      
      GraphicsDevice.SetRenderTarget(null);

      SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
      spriteBatch.Begin(samplerState: SamplerState.PointClamp);
      spriteBatch.Draw(renderTarget, RealRenderRectangle(), Color.White);
      spriteBatch.End();

      base.Draw(gameTime);
    }

    /// <summary>
    ///   Get a rendering rectangle, scale to presented size.
    /// </summary>
    public Rectangle RealRenderRectangle() {
      return new Rectangle(0, 0, Convert.ToInt32(Constants.RenderWidth * Constants.RenderScale), Convert.ToInt32(Constants.RenderHeight * Constants.RenderScale));
    }
  }
}
