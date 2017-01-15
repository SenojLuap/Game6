using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game6 {
  public class Plant : IUpdateable, ILocalDrawable {


    /// <summary>
    ///   The last growth stage.
    /// </summary>
    public int LastGrowthStage { 
      get {
	return NumGrowthStages - 1;
      }
    }


    public float GrowthProgress { get; set; }
    public Vector2 Pos { get; set; }
    public int GrowthStage { get; set; }
    public float GrowthDelay { get; set; }
    public int NumGrowthStages { get; set; }

    public Plant() {
      GrowthStage = 0;
    }

    
    #region *** Update ***

    /// <summary>
    ///   Update to game tick.
    /// </summary>
    public void Update(Game1 game, GameTime gameTime) {
      if (GrowthStage < LastGrowthStage) {
	float secondsPassed = (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
	GrowthProgress += secondsPassed;
	if (GrowthProgress >= (float)GrowthDelay) IncreaseGrowth(game);
      }
    }

    #endregion
    

    #region *** Growth ***
    
    /// <summary>
    ///   Increase to the next growth stage.
    /// </summary>
    virtual public void IncreaseGrowth(Game1 game) {
      if (GrowthStage < LastGrowthStage ) {
	GrowthStage++;
	GrowthProgress -= GrowthDelay;
      }
    }

    #endregion

    #region *** Draw ***

    public virtual void Initialize(ContentManager manager) { }

    public virtual void Draw(SpriteBatch spriteBatch) { }

    #endregion
  }
}