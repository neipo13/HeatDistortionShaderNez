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

namespace HeatWaveShader.Components
{
    public class PlayerController : Component, IUpdatable
    {
        TiledMapMover mover;
        TiledMapMover.CollisionState collisionState = new TiledMapMover.CollisionState();
        BoxCollider moveBox;
        Sprite<PlayerAnimations> sprite;
        AnimationManager animationController;
        InputHandler input;
        Weapon weapon;


        float moveSpeed = 175f;
        float gravity = 800f;
        float jumpHeight = (NezGame.TileSize * 4) + (NezGame.TileSize/4); //16px tiles * tiles high + buffer


        Vector2 velocity = new Vector2();
        bool isGrounded => collisionState.below;
        bool isMovingHorizontal => velocity.X > 0f || velocity.X < 0f;

        public override void onAddedToEntity()
        {
            input = entity.getComponent<InputHandler>();
            mover = entity.getComponent<TiledMapMover>();
            animationController = entity.getComponent<AnimationManager>();
            sprite = animationController.sprite;
            moveBox = entity.getComponent<BoxCollider>();
            weapon = entity.getComponent<Weapon>();
        }

        public void update()
        {
            // movement
            velocity.X = input.axialInput.X * moveSpeed;
            //jumping
            if(input.JumpButtonInput.isPressed && isGrounded)
            {
                velocity.Y = -Mathf.sqrt(2 * jumpHeight * gravity);
            }
            if (!collisionState.below && input.JumpButtonInput.isReleased)
            {
                velocity.Y *= 0.5f;
            }
            //apply gravity
            velocity.Y += gravity * Time.deltaTime;

            mover.move(velocity * Time.deltaTime, moveBox, collisionState);

            //sprite adjustments
            if (velocity.X > 0)
            {
                sprite.flipX = false;
                weapon.setFlipX(false);
            }
            else if (velocity.X < 0)
            {
                sprite.flipX = true;
                weapon.setFlipX(true);
            }
            if (!isGrounded || !isMovingHorizontal)
            {
                animationController.Play(PlayerAnimations.Idle);
            }
            else if (isGrounded && isMovingHorizontal)
            {
                animationController.Play(PlayerAnimations.Run);
            }

            // fire weapon
            if (input.ShootButtonInput.isPressed)
            {
                Direction lr = Direction.Right;
                if (sprite.flipX)
                {
                    lr = Direction.Left;
                }
                weapon.Fire(lr);
            }
        }
    }
}
