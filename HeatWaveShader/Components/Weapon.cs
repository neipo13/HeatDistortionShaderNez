using Nez;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez.Textures;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using HeatWaveShader.Entities;

namespace HeatWaveShader.Components
{
    public class Weapon: Component, IUpdatable
    {
        private float startSpeed = 200f;
        private float endSpeed = 2000f;

        private Sprite sprite;

        public float cooldown = 0.5f;
        public float cooldown_timer = -1f;

        Subtexture subtexture;
        Subtexture bulletTexture;
        Subtexture explosionTexture;

        public Weapon(Subtexture weaponSubtexture, Subtexture bulletSubtexture, Subtexture explosionSubtexture)
        {
            this.subtexture = weaponSubtexture;
            this.bulletTexture = bulletSubtexture;
            this.explosionTexture = explosionSubtexture;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            sprite = entity.addComponent(new Sprite(subtexture));
            sprite.localOffset = new Vector2(8, 0);
            sprite.renderLayer = -5;
        }


        public void Fire(Direction dir)
        {
            if (cooldown_timer > 0f) return; if (cooldown_timer > 0f) return;
            //spawn a new bullet in the scene
            AddBullet(dir);

            cooldown_timer = cooldown;
        }

        private void AddBullet(Direction dir)
        {
            //spawn a new bullet in the scene
            var bullet = entity.scene.addEntity(new Explosive(explosionTexture));
            bullet.position = entity.position;

            float x = startSpeed * (dir == Direction.Left ? -1 : 1);
            float xEnd = endSpeed * (dir == Direction.Left ? -1 : 1);
            var mover = bullet.addComponent(new BulletMover(new Vector2(x, 0), (r) =>
            {
                bullet.destroy();
                //spawn explosion!
            }));

            mover.tween("velocity", new Vector2(xEnd, 0), 1f).start();

            var wallCollider = bullet.addComponent(new BoxCollider(12, 2));
            wallCollider.physicsLayer = Constants.PhysicsLayers.bullet;
            wallCollider.collidesWithLayers = Constants.PhysicsLayers.tiles;

            var bs = bullet.addComponent(new Sprite(bulletTexture));
            bs.flipX = sprite.flipX;

        }

        public void update()
        {
            if (cooldown_timer > 0f) cooldown_timer -= Time.deltaTime;
        }

        public void setFlipX(bool flipped)
        {
            if (sprite == null) return;
            sprite.flipX = flipped;
            if (flipped)
            {
                sprite.localOffset = new Vector2(-8, 0);
            }
            else
            {
                sprite.localOffset = new Vector2(8, 0);
            }

        }
    }
}
