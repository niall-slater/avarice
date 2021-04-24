using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static float Cash;
    public static float MaxDepthReached;
    public static float CaravanTimer;

    public static int MonsterCap = 200;

    public static List<Monster> MonsterPool;

    private static int _monsterCount => MonsterPool.Count(x => x.Alive);

    public static int MonsterCount;

    void Start()
    {
        Cash = 1500;
        MaxDepthReached = 0f;
        CaravanTimer = 300f;
        MonsterPool = new List<Monster>();

        ActorEventHub.Instance.OnMonsterSpawned += HandleMonsterSpawn;
        ActorEventHub.Instance.OnMonsterKilled += HandleMonsterDeath;

        FillMonsterPool();
    }

    private void FillMonsterPool()
    {
        for (var i = 0; i < MonsterCap; i++)
        {
            var monster = MonsterFactory.Instance.CreateMonster(Vector3.zero);
            monster.Alive = false;
            monster.gameObject.SetActive(false);
            MonsterPool.Add(monster);
        }
    }

    public static void SpawnMonster(Vector3 position)
    {
        if (MonsterCount >= MonsterCap)
            return;

        var corpse = GetInactiveMonsterFromPool();

        if (corpse == null)
            return;

        corpse.Reinitialise(position);
    }

    private static Monster GetInactiveMonsterFromPool()
    {
        return MonsterPool.FirstOrDefault(x => !x.Alive);
    }

    // Events for when a monster spawns. It's already been instantiated or pooled.
    private void HandleMonsterSpawn(Monster monster)
    {
        MonsterCount++;
    }

    private void HandleMonsterDeath(Monster monster)
    {
        MonsterCount--;
        monster.gameObject.SetActive(false);
    }

    void Update()
    {
        CaravanTimer -= Time.deltaTime;

        if (CaravanTimer <= 0f)
        {
            SpawnCaravan();
            CaravanTimer = 300f;
        }
    }

    private void SpawnCaravan()
    {
        Debug.Log("Spawning caravan");
        //throw new NotImplementedException();
    }
}
