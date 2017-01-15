using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game6 {

  public class BasicPlant : Plant {
    
    /// <summary>
    ///   The plant's current sprite
    /// </summary>
    public static List<Texture2D> Sprites { get; set; }

    public static Vector2 DrawOffset = new Vector2(16f, 32f);

    public BasicPlant() : base() {
      NumGrowthStages = 5;
      GrowthDelay = 10;
    }
    
    #region *** Draw ***
    
    override public void Initialize(ContentManager manager) {
      base.Initialize(manager);
      if (Sprites == null) {
	Sprites = new List<Texture2D>();
	for (int i = 0; i < NumGrowthStages; i++)
	  Sprites.Add(manager.Load<Texture2D>("Plant" + i));
      }
    }

    
    override public void Draw(SpriteBatch spriteBatch) {
      base.Draw(spriteBatch);
      Vector2 drawPos = Pos - DrawOffset;
      spriteBatch.Draw(Sprites[GrowthStage], drawPos, Color.White); 
    }
    
    #endregion
  }
}