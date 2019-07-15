using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace HeatWaveShader
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class NezGame: Core
    {
        public const int TileSize = 16;
        public const int TilesWide = 16;
        public const int TilesHigh = 9;

        public int designWidth => TilesWide * TileSize;
        public int designHeight => TilesHigh * TileSize;

        public NezGame() : base(256 * 4, 144 * 4, windowTitle: "Heat Wave Shader")
        {
            Scene.setDefaultDesignResolution(designWidth, designHeight, Scene.SceneResolutionPolicy.BestFit, 0, 0);
            Window.AllowUserResizing = true;
            // weird thing I'm required to do running Windows on a retina Macbook (¯\_(ツ)_/¯)
            Window.Position = new Point(0, 0);

        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // little hack to fix some potential bugs with post processing effects
            scene = Scene.createWithDefaultRenderer();
            base.Update(new GameTime());
            base.Draw(new GameTime());
            // load your actual scene
            scene = new TestScene();
        }
    }
}
