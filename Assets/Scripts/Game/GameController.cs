using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float StartingCash;

    public static float Cash;
    public static float MaxDepthReached;

    public float CaravanInterval;
    public static float CaravanTimer;

    public float SwarmInterval;
    public static float _swarmTicker;

    public static int MonsterCap = 500;
    public static int BulletCap = 600;

    public static List<Monster> MonsterPool;
    public static List<Bullet> BulletPool;

    public static List<Actor> PlayerUnits;

    private static int _monsterCount => MonsterPool.Count(x => x.Alive);

    public static int MonsterCount;

    private static int _bulletCount => BulletPool.Count(x => x.Alive);

    public static int BulletCount;

    public Transform CreaturesRoot;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    public List<Actor> StartingUnits;

    public enum VictoryState
    {
        IN_PROGRESS,
        WIN,
        LOSE
    }

    public VictoryState CurrentState;

    void Start()
    {
        PlayerUnits = new List<Actor>();
        CurrentState = VictoryState.IN_PROGRESS;
        Cash = StartingCash;
        MaxDepthReached = 0f;
        _swarmTicker = SwarmInterval * 2f;
        CaravanTimer = CaravanInterval;
        MonsterPool = new List<Monster>();
        BulletPool = new List<Bullet>();

        ActorEventHub.Instance.OnMonsterSpawned += HandleMonsterSpawn;
        ActorEventHub.Instance.OnMonsterKilled += HandleMonsterDeath;

        ActorEventHub.Instance.OnActorDestroyed += HandleActorDestroyed;

        ScoreEventHub.Instance.OnBioBombBuilt += HandleBioBombBuilt;
        ScoreEventHub.Instance.OnBioBombDetonation += Win;

        PlayerUnits.AddRange(StartingUnits);

        FillMonsterPool();
        FillBulletPool();
    }

    private void OnDestroy()
    {
        ActorEventHub.Instance.OnMonsterSpawned -= HandleMonsterSpawn;
        ActorEventHub.Instance.OnMonsterKilled -= HandleMonsterDeath;
        ActorEventHub.Instance.OnActorDestroyed -= HandleActorDestroyed;
        ScoreEventHub.Instance.OnBioBombBuilt -= HandleBioBombBuilt;
        ScoreEventHub.Instance.OnBioBombDetonation -= Win;
    }

    private void HandleBioBombBuilt(BioBomb bomb)
    {
        var offset = Vector3.one + UnityEngine.Random.insideUnitSphere;
        offset.z = 0;
        CreateSwarm(bomb.transform.position + offset, 10, "SWARM ATTRACTED\nBY BOMB");
    }

    void Update()
    {
        CaravanTimer -= Time.deltaTime;

        if (CaravanTimer <= 0f)
        {
            SpawnCaravan();
            CaravanTimer = CaravanInterval;
        }

        _swarmTicker -= Time.deltaTime;
        
        if (_swarmTicker <= 0f)
        {
            var depthMultiplier = MaxDepthReached / GameVariables.DEPTH_LEVEL_3;
            if (MaxDepthReached >= GameVariables.DEPTH_LEVEL_3)
            {
                depthMultiplier = 1f;
            }
            _swarmTicker = SwarmInterval * UnityEngine.Random.Range(.75f, 1.25f);
            _swarmTicker -= 30f * depthMultiplier;
            CreateSwarm(Map.GetRandomPosition(), Mathf.RoundToInt(5 + 25 * depthMultiplier));
        }
    }

    private void FillMonsterPool()
    {
        for (var i = 0; i < MonsterCap; i++)
        {
            var monster = MonsterFactory.Instance.CreateMonster(Vector3.zero);
            monster.HP = 0;
            monster.gameObject.SetActive(false);
            monster.transform.SetParent(CreaturesRoot, true);
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

    public static void SpawnGiantMonster(Vector3 position)
    {
        var giant = MonsterFactory.Instance.CreateGiantMonster(position);
        giant.Reinitialise(position);
    }

    public static void CreateSwarm(Vector3 origin, int size, string text = "")
    {
        var textToUse = text == "" ? "SWARM DETECTED" : text;
        for (var i = 0; i < size; i++)
        {
            var offset = UnityEngine.Random.insideUnitSphere;
            offset.z = 0f;
            SpawnMonster(origin + offset);
        }
        SpawnGiantMonster(origin);
        PopUpManager.CreatePopup(textToUse, origin, 5f);
    }

    public static bool SpawnBullet(Vector3 position, Vector3 direction, Actor shooter)
    {
        if (BulletCount >= BulletCap)
            return false;

        var corpse = GetInactiveBulletFromPool();

        if (corpse == null)
            return false;

        corpse.Reinitialise(position, direction, shooter);

        return true;
    }

    public static void SpawnMarine(Vector3 position)
    {
        var marine = Instantiate(Resources.Load<GameObject>(PrefabPaths.MarinePrefab), position, Quaternion.identity, null).GetComponent<Actor>();
        PlayerUnits.Add(marine);
    }

    public static void SpawnBuilder(Vector3 position)
    {
        var builder = Instantiate(Resources.Load<GameObject>(PrefabPaths.BuilderPrefab), position, Quaternion.identity, null).GetComponent<Actor>();
        PlayerUnits.Add(builder);
    }

    public static void SpawnAPC(Vector3 position)
    {
        var APC = Instantiate(Resources.Load<GameObject>(PrefabPaths.APCPrefab), position, Quaternion.identity, null).GetComponent<Actor>();
        PlayerUnits.Add(APC);
    }

    public static void RefreshMonsterCount()
    {
        MonsterCount = MonsterPool.Count(x => x.Alive);
    }

    public static void RefreshBulletCount()
    {
        BulletCount = BulletPool.Count(x => x.Alive);
    }

    private static Monster GetInactiveMonsterFromPool()
    {
        return MonsterPool.FirstOrDefault(x => !x.Alive);
    }

    private static Bullet GetInactiveBulletFromPool()
    {
        return BulletPool.Find(x => !x.Alive);
    }

    // Events for when a monster spawns. It's already been instantiated or pooled.
    private void HandleMonsterSpawn(Monster monster)
    {
    }

    private void HandleMonsterDeath(Monster monster)
    {
    }

    private void HandleActorDestroyed(Actor actor, Actor killer)
    {
        if (actor.Team != killer.Team)
        {
            if (actor is Monster || actor is Marine)
            {
                killer.KillCount++;
            }
        }

        if (actor.Team == GameVariables.TEAM.PLAYER)
        {
            PlayerUnits.Remove(actor);

            PopUpManager.CreatePopup($"{actor.ActorName}\nhas fallen", actor.transform.position, 2f);

            if (CurrentState != VictoryState.IN_PROGRESS)
            {
                return;
            }

            //You can still win if you have a builder, a barracks or a biobomb

            if (PlayerUnits.Count == 0)
            {
                if (Map.Buildings.Count == 0)
                {
                    Lose("No units or buildings left");
                    return;
                }
            }

            if (PlayerUnits.Find(x => x is Builder) == null)
            {
                if (Map.Buildings.Find(x => x is Barracks) == null)
                {
                    if (Map.Buildings.Find(x => x is BioBomb) == null)
                    {
                        Lose("No builder, barracks or biobomb. Situation hopeless");
                        return;
                    }
                }
            }
        }
    }

    private void SpawnCaravan()
    {
        Debug.Log("Spawning caravan");
        var caravan = Instantiate(Resources.Load<GameObject>(PrefabPaths.CaravanPrefab), Map.GetIngress(), Quaternion.identity, null);
    }

    public void Win(BioBomb bomb)
    {
        CurrentState = VictoryState.WIN;
        Debug.Log("YOU WON!");
        WinScreen.SetActive(true);
    }

    public void Lose(string reason)
    {
        CurrentState = VictoryState.LOSE;
        Debug.Log("YOU LOST:\n" + reason);
        LoseScreen.SetActive(true);
        LoseScreen.GetComponent<LoseScreen>().Reason.text = reason;
    }
}
