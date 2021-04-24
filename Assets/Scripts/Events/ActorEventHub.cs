using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorEventHub
{
    private static ActorEventHub _instance;

    public static ActorEventHub Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ActorEventHub();
            }
            return _instance;
        }
    }

    public static void Destroy() { _instance = null; }

    /// <summary>
    /// A monster spawns.
    /// </summary>
    public delegate void MonsterSpawned(Monster monster); public event MonsterSpawned OnMonsterSpawned;
    public void RaiseOnMonsterSpawned(Monster monster) { OnMonsterSpawned?.Invoke(monster); }

    /// <summary>
    /// A monster is killed.
    /// </summary>
    public delegate void MonsterKilled(Monster monster); public event MonsterKilled OnMonsterKilled;
    public void RaiseOnMonsterKilled(Monster monster) { OnMonsterKilled?.Invoke(monster); }
}
