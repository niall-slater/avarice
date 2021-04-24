using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class BuildingFactory
{
    private static BuildingFactory _instance;

    public static BuildingFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BuildingFactory();
            }
            return _instance;
        }
    }

    public static void Destroy() { _instance = null; }

    public Building CurrentBlueprint;

    public Building PlaceBlueprint(Vector3 position)
    {
        var Building = GameObject.Instantiate(CurrentBlueprint.gameObject, position, Quaternion.identity, null).GetComponent<Building>();
        return Building;
    }
}
