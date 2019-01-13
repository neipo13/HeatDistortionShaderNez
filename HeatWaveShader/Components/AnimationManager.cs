
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using Nez.Textures;

namespace HeatWaveShader.Components
{
    public enum Anims
    {
        Explosion
    }

    public class AnimationManager : Component
    {
        public Sprite<Anims> sprite;
        List<Subtexture> subtextures;
        public AnimationManager(Sprite<Anims> sprite, List<Subtexture> subtextures)
        {
            this.sprite = sprite;
            this.subtextures = subtextures;
        }


        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            sprite.addAnimation(Anims.Explosion, new SpriteAnimation(subtextures.GetRange(97, 2)));

            sprite.play(Anims.Explosion);
        }

        public override void onRemovedFromEntity()
        {
            base.onRemovedFromEntity();
            sprite = null;
            subtextures = null;
        }

        public void Play(Anims animation)
        {
            var currentAnimation = sprite.currentAnimation;
            if (currentAnimation != animation)
                sprite.play(animation);
        }
    }
}