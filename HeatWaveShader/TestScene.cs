using HeatWaveShader.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;
using HeatWaveShader.Effects;

namespace HeatWaveShader
{
    public class TestScene : Scene
    {
        Texture2D tiles;
        public Subtexture[] tileSubtextures;

        PixelHeatDistortionPointPostProcessor postProcessor;

        public override void initialize()
        {
            // just setting clear color to one in the palette im using
            clearColor = new Color(57, 120, 168);
            addRenderer(new DefaultRenderer());

            // load our textures/subtextures
            tiles = content.Load<Texture2D>("img/tiles");
            tileSubtextures = Subtexture.subtexturesFromAtlas(tiles, NezGame.TileSize, NezGame.TileSize).ToArray();
            

            var tiledMap = content.Load<TiledMap>($"tiled/test");
            var tiledEntity = this.createEntity("debugMap");
            var tileMapComponent = tiledEntity.addComponent(new TiledMapComponent(tiledMap, "collision"));
            tileMapComponent.setLayersToRender("collision");
            

            var effect = content.Load<Effect>("effects/PixelHeatDistortionPoint");
            postProcessor = new PixelHeatDistortionPointPostProcessor(0, effect);
            addPostProcessor(postProcessor);

        }

        public override void update()
        {
            base.update();
            postProcessor.update();
            if (Nez.Input.leftMouseButtonPressed)
            {
                Explosive.CreateExplosion(this, Nez.Input.mousePosition);
            }
        }

        public void PlayPostProcessor(Vector2 pos)
        {
            pos.X = pos.X / (NezGame.TilesWide * NezGame.TileSize);
            pos.Y = pos.Y / (NezGame.TilesHigh * NezGame.TileSize);
            postProcessor.play(pos, 750f);
        }
    }
}
