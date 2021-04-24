using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject VeinPrefab;

    public GameObject MinePrefab;

    public Transform MapRoot;

    public Transform BuildingsRoot;

    void Start()
    {
        GenerateMap(8, 12);
    }

    public void GenerateMap(int size, int numberOfVeins)
    {
        var veins = new List<Vein>();
        for (var i = 0; i < numberOfVeins; i++)
        {
            var pos = new Vector3(Random.Range(-size / 2, size / 2), Random.Range(-size / 2, size / 2), 0f);
            pos.x = pos.x % 32;
            pos.y = pos.y % 32;
            var vein = Instantiate(VeinPrefab, pos, Quaternion.identity, MapRoot).GetComponent<Vein>();
            veins.Add(vein);
        }
        Map.Veins = veins;

        var mines = new List<Building>();
        foreach (Vein v in veins)
        {
            if (Random.value < -0.25f)
            {
                var mine = Instantiate(MinePrefab, v.transform.position, Quaternion.identity, BuildingsRoot).GetComponent<Mine>();
                mines.Add(mine);
            }
        }

        Map.Buildings.AddRange(mines);
    }
}
