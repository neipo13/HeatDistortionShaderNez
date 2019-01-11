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
    public enum PlayerAnimations
    {
        Idle,
        Run
    }

    public class AnimationManager : Component
    {
        public Sprite<PlayerAnimations> sprite;
        List<Subtexture> subtextures;
        public AnimationManager(Sprite<PlayerAnimations> sprite, List<Subtexture> subtextures)
        {
            this.sprite = sprite;
            this.subtextures = subtextures;
        }


        public override void onAddedToEntity()
        {
            base.onAddedToEntity();

            var runSpriteAnim = new SpriteAnimation(subtextures.GetRange(36, 5));
            runSpriteAnim.fps = 16;
            var runAnim = sprite.addAnimation(PlayerAnimations.Run, runSpriteAnim);
            var idleAnim = sprite.addAnimation(PlayerAnimations.Idle, new SpriteAnimation(subtextures.GetRange(32, 4)));

            sprite.play(PlayerAnimations.Idle);
        }

        public override void onRemovedFromEntity()
        {
            base.onRemovedFromEntity();
            sprite = null;
            subtextures = null;
        }

        public void Play(PlayerAnimations animation)
        {
            var currentAnimation = sprite.currentAnimation;
            if (currentAnimation != animation)
                sprite.play(animation);
        }
    }
}
