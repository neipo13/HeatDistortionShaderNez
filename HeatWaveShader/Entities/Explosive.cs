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
using HeatWaveShader.Components;

namespace HeatWaveShader.Entities
{
    public class Explosive
    {
        public static Entity CreateExplosion(Scene scene, Vector2 position, float lifespan = 0.3f, float scalar = 2f)
        {
            var e = scene.createEntity("explosion");
            e.position = position;

            var testScene = ((TestScene)scene);
            testScene.PlayPostProcessor(position);
            var s = new Sprite<Anims>();
            s.renderLayer = -10;
            var anim = new AnimationManager(s, testScene.tileSubtextures.ToList());
            anim.Play(Anims.Explosion);
            // prob a memory leak but whatev its a demo
            s.onAnimationCompletedEvent += (playerAnim) =>
            {
                if (playerAnim == Anims.Explosion)
                {
                    e.removeComponent(s);

                }
            };
            e.addComponent(s);
            e.addComponent(anim);          

            e.addComponent(new DestroyOnTimer(lifespan));

            return e;
        }
    }
}
