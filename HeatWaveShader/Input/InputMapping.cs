using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatWaveShader.Input
{
    public class InputMapping
    {

        public int index { get; set; }
        public int[] Left { get; set; }
        public int[] Right { get; set; }
        public int[] Up { get; set; }
        public int[] Down { get; set; }
        public int[] jumpKey { get; set; }
        public int[] jumpButton { get; set; }
        public int[] shootKey { get; set; }
        public int[] shootButton { get; set; }
    }
}
