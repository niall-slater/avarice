﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static List<Vein> Veins = new List<Vein>();

    public static List<Building> Buildings = new List<Building>();

    public static Building GetNearestBuilding(Vector3 position)
    {
        if (Buildings.Count == 0)
        {
            return null;
        }

        var dist = float.MaxValue;
        var result = Buildings[0];

        foreach (Building b in Buildings)
        {
            var test = Vector3.Distance(position, b.transform.position);
            if (test < dist)
            {
                test = dist;
                result = b;
            }
        }

        return result;
    }

    public static Building GetRandomBuildingWithinRange(Vector3 position, float range)
    {
        if (Buildings.Count == 0)
            return null;

        var dist = range;
        var result = Buildings[0];

        var viableBuildings = Buildings.Where(x => Vector3.Distance(position, x.transform.position) < range) as List<Building>;
        if (viableBuildings == null)
            return null;

        return viableBuildings[Random.Range(0, viableBuildings.Count())];
    }
}
