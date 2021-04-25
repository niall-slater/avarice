using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class GameVariables
{
    public const float DEFAULT_MONSTER_HP = 5F;

    public const float DEPTH_LEVEL_0 = 150f;
    public const float DEPTH_LEVEL_1 = 250f;
    public const float DEPTH_LEVEL_2 = 450f;
    public const float DEPTH_LEVEL_3 = 750f;

    public const float BIO_BOMB_FUSE = 10f;

    public enum TEAM
    {
        PLAYER,
        MONSTER
    }
}