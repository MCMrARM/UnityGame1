using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahou.Config
{
    struct Tags
    {
        public const string PLAYER = "Player";

        public static bool IsValidTarget(string tag)
        {
            return tag != PLAYER;
        }
    }
}
