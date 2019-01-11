using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nez;
using Microsoft.Xna.Framework;

namespace HeatWaveShader.Components
{
    public class BulletMover : Component, IUpdatable
    {
        Mover mover;
        public Vector2 velocity;
        float rotation = 0f;
        bool hasRotation => rotation >= 0.1f;
        CollisionResult result;

        Action<CollisionResult> onCollisionAny;
        public BulletMover(Vector2 velocity, Action<CollisionResult> onCollisonWithAnything = null, float friction = 0f, float gravity = 0f)
        {
            this.velocity = velocity;
            this.onCollisionAny = onCollisonWithAnything;
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            mover = entity.getComponent<Mover>();
            if (mover == null)
            {
                mover = entity.addComponent(new Mover());
            }
        }
        public void update()
        {
            mover.move(velocity * Time.deltaTime, out result);
            if (result.collider != null)
            {
                onCollisionAny?.Invoke(result);
            }
        }
    }
}
