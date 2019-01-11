using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Microsoft.Xna.Framework;
using Nez.Textures;
using Nez.Sprites;
using HeatWaveShader.Components;
using Nez.Tiled;
using HeatWaveShader.Input;

namespace HeatWaveShader.Entities
{
    public class Player : Entity
    {
        public Player(Vector2 position, Subtexture[] subtextures, TiledTileLayer collisionLayer)
        {
            this.name = "player";
            this.transform.position = position;
            var sprite = addComponent<Sprite<PlayerAnimations>>(new Sprite<PlayerAnimations>());
            var animationManager = addComponent<AnimationManager>(new AnimationManager(sprite, subtextures.ToList()));
            addComponent(new InputHandler(0));
            var box = addComponent(new BoxCollider(16, 16));
            box.physicsLayer = Constants.PhysicsLayers.move;
            box.collidesWithLayers = Constants.PhysicsLayers.tiles;
            addComponent(new TiledMapMover(collisionLayer));
            addComponent(new Weapon(subtextures[85], subtextures[96], subtextures[97]));
            addComponent(new PlayerController());
        }
    }
}
