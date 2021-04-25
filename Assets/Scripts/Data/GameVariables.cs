using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class GameVariables
{
    public static float DEFAULT_MONSTER_HP = 5f;
    public static float GIANT_MONSTER_HP = 350f;

    public static float DEFAULT_MONSTER_ATTACKPOWER = 1f;
    public static float GIANT_MONSTER_ATTACKPOWER = 25f;
    public static float APC_RAMMING_DAMAGE = 3f;
    public static float BULLET_DAMAGE = 1f;

    public static int MONSTER_CAP = 600;
    public static int BULLET_CAP = 600;

    public static float DEPTH_LEVEL_0 = 150f;
    public static float DEPTH_LEVEL_1 = 750f;
    public static float DEPTH_LEVEL_2 = 2500f;

    public static float DEPTH_LEVEL_3 = 6500f;

    public static float BIO_BOMB_FUSE = 150f;

    public enum TEAM
    {
        PLAYER,
        MONSTER
    }
}