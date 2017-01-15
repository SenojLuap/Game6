using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Game6 {
  
  public class Player : ILocalDrawable, IUpdateable {
    public Vector2 Pos { get; set; }
    public Vector2 Velocity { get; set; }
    public Texture2D Sprite { get; set; }
    public int SeedCount { get; set; }
    public int Speed { get; set; }
    public float PlantSeedCooldown { get; set; }
    
    
    public Player() {
      Pos = new Vector2(200f, 200f);
      Speed = 100;
      SeedCount = 5;
    }
    
    
    public void Draw(SpriteBatch spriteBatch) {
      spriteBatch.Draw(Sprite, Pos, Color.White);
    }
    
    
    public void Initialize(ContentManager manager) {
      Sprite = manager.Load<Texture2D>("Player");
    }
    
    
    public void Update(Game1 game, GameTime gameTime) {
      KeyboardState kb = game.CurrentKeyboardState;
      
      Vector2 newVelocity = new Vector2();
      if (kb.IsKeyDown(Keys.A)) {
	newVelocity.X = -Speed;
      } else if (kb.IsKeyDown(Keys.D)) {
	newVelocity.X = Speed;
      } else {
	newVelocity.X = 0;
      }
      
      if (kb.IsKeyDown(Keys.W)) {
	newVelocity.Y = -Speed;
      } else if (kb.IsKeyDown(Keys.S)) {
	newVelocity.Y = Speed;
      } else {
	newVelocity.Y = 0;
      }
      if (newVelocity.LengthSquared() > 1.0f) {
	newVelocity.Normalize();
      }

      ApplyVelocity(newVelocity);
      float secondsPassed = (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
      UpdatePosition(secondsPassed);
      if (PlantSeedCooldown > 0.0) PlantSeedCooldown -= secondsPassed;

      if (kb.IsKeyDown(Keys.E) && SeedCount > 0 && PlantSeedCooldown <= 0.0) {
	BasicPlant plant = new BasicPlant();
	plant.Initialize(game.Content);
	plant.Pos = Pos;
	game.Plants.Add(plant);
	SeedCount--;
	PlantSeedCooldown = 1.0f;
      }
    }
    
    
    /// <summary>
    ///   Apply a new velocity to the player.
    /// </summary>
    public void ApplyVelocity(Vector2 newVelocity) {
      Velocity = newVelocity;
    }
    
    
    /// <summary>
    ///   Update the player's position.
    /// </summary>
    public void UpdatePosition(float deltaTime) {
      Vector2 deltaPos = Vector2.Multiply(Velocity, deltaTime * Speed);
      Pos += deltaPos;
    }
  }
}