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
    public static int BulletCap = 600;

    public static List<Monster> MonsterPool;
    public static List<Bullet> BulletPool;

    private static int _monsterCount => MonsterPool.Count(x => x.Alive);

    public static int MonsterCount;

    private static int _bulletCount => BulletPool.Count(x => x.Alive);

    public static int BulletCount;

    void Start()
    {
        Cash = 1500;
        MaxDepthReached = 0f;
        CaravanTimer = 300f;
        MonsterPool = new List<Monster>();
        BulletPool = new List<Bullet>();

        ActorEventHub.Instance.OnMonsterSpawned += HandleMonsterSpawn;
        ActorEventHub.Instance.OnMonsterKilled += HandleMonsterDeath;

        FillMonsterPool();
        FillBulletPool();
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

    private void FillBulletPool()
    {
        for (var i = 0; i < BulletCap; i++)
        {
            var bullet = BulletFactory.Instance.CreateBullet(Vector3.zero, Vector3.zero);
            bullet.Alive = false;
            bullet.gameObject.SetActive(false);
            BulletPool.Add(bullet);
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

    public static void SpawnBullet(Vector3 position, Vector3 direction)
    {
        if (BulletCount >= BulletCap)
            return;

        var corpse = GetInactiveBulletFromPool();

        if (corpse == null)
            return;

        corpse.Reinitialise(position, direction);
    }

    public static void RefreshMonsterCount()
    {
        MonsterCount = MonsterPool.Count(x => x.Alive);
    }

    private static Monster GetInactiveMonsterFromPool()
    {
        return MonsterPool.FirstOrDefault(x => !x.Alive);
    }

    private static Bullet GetInactiveBulletFromPool()
    {
        return BulletPool.FirstOrDefault(x => !x.Alive);
    }

    // Events for when a monster spawns. It's already been instantiated or pooled.
    private void HandleMonsterSpawn(Monster monster)
    {
    }

    private void HandleMonsterDeath(Monster monster)
    {
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
