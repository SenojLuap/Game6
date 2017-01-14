using Microsoft.Xna.Framework;

namespace Game6 {

  public partial class Game1 : Game {

    public Player Player { get; set; }

    /// <summary>
    ///   Create the player instance.
    /// </summary>
    public void CreatePlayer() {
      this.Player = new Player();
    }
  }
}