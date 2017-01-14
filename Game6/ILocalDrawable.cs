using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game6 {
  public interface ILocalDrawable {

    void Initialize(ContentManager manager);
    void Draw(SpriteBatch spriteBatch);
  }
}