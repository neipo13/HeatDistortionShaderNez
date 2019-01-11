using HeatWaveShader.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using Nez.Tiled;

namespace HeatWaveShader
{
    public class TestScene : Scene
    {
        Texture2D tiles;
        public Subtexture[] tileSubtextures;

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
            tileMapComponent.renderLayer = 0;
            tileMapComponent.physicsLayer = Constants.PhysicsLayers.tiles;
            var collisionLayer = tileMapComponent.collisionLayer;

            var spawn = tiledMap.getObjectGroup("spawn").objects[0]; // hard assuming only one spawn point
            addEntity(new Player(new Vector2(spawn.x, spawn.y), tileSubtextures, collisionLayer));

        }
    }
}
