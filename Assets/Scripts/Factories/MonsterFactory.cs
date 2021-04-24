using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MonsterFactory
{
    private static MonsterFactory _instance;

    public static MonsterFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MonsterFactory();
            }
            return _instance;
        }
    }

    public static void Destroy() { _instance = null; }

    public Monster CreateMonster(Vector3 position)
    {
        var monster = GameObject.Instantiate(Resources.Load<GameObject>(PrefabPaths.MonsterPrefab), position, Quaternion.identity, null).GetComponent<Monster>();
        return monster;
    }
}
