using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using Nez.Textures;
using HeatWaveShader.Util;

namespace HeatWaveShader.Entities
{
    public class Explosive : Entity
    {
        Subtexture explosionSubtexture;
        public Explosive(Subtexture explosionSubtexture)
        {
            this.explosionSubtexture = explosionSubtexture;
        }
        public override void onRemovedFromScene()
        {
            base.onRemovedFromScene();
            //spawn explosion
            CreateExplosion(this.scene, explosionSubtexture, position);

        }

        public static Entity CreateExplosion(Scene scene, Subtexture subtexture, Vector2 position, float lifespan = 0.3f, float scalar = 6f)
        {
            var e = scene.createEntity("explosion");
            e.position = position;


            var s = e.addComponent(new Sprite(subtexture));
            s.renderLayer = -10;
            Core.schedule(lifespan / 2f, (timer) => e.removeComponent(s));

            e.tween("scale", new Vector2(scalar), lifespan).setEaseType(Nez.Tweens.EaseType.ExpoOut).start();

            e.addComponent(new DestroyOnTimer(lifespan));

            return e;
        }
    }
}
