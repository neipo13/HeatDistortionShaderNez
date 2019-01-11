using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatWaveShader.Util
{
    public class DestroyOnTimer : Component, IUpdatable
    {
        float timer = 0f;
        public DestroyOnTimer(float time)
        {
            timer = time;
        }
        public void update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                this.entity.destroy();
            }
        }
    }
}
