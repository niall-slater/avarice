using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class GameVariables
{
    public const float DEFAULT_MONSTER_HP = 5f;
    public const float GIANT_MONSTER_HP = 350f;

    public const float DEFAULT_MONSTER_ATTACKPOWER = 1f;
    public const float GIANT_MONSTER_ATTACKPOWER = 25f;
    public const float APC_RAMMING_DAMAGE = 3f;
    public const float BULLET_DAMAGE = 1f;

    public const float DEPTH_LEVEL_0 = 150f;
    public const float DEPTH_LEVEL_1 = 750f;
    public const float DEPTH_LEVEL_2 = 2500f;

    public const float DEPTH_LEVEL_3 = 6500f;

    public const float BIO_BOMB_FUSE = 150f;

    public enum TEAM
    {
        PLAYER,
        MONSTER
    }
}