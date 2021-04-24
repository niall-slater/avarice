using System;
using System.Collections;
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

        return viableBuildings[UnityEngine.Random.Range(0, viableBuildings.Count())];
    }

    public static bool ValidateBuildingPlacement(Building selectedBlueprint, Vector3 position)
    {
        var result = false;

        if (selectedBlueprint is Mine)
        {
            foreach (Vein v in Map.Veins)
            {
                if (v.transform.position == position)
                {
                    result = true;
                }
            }

            return result;
        }

        result = true;

        foreach (Building b in Map.Buildings)
        {
            if (b.transform.position == position)
            {
                result = false;
            }
        }

        return result;
    }
}
