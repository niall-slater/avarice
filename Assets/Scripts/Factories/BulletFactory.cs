using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class BulletFactory
{
    private static BulletFactory _instance;

    public static BulletFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BulletFactory();
            }
            return _instance;
        }
    }

    public static void Destroy() { _instance = null; }

    public Bullet CreateBullet(Vector3 position, Vector3 direction)
    {
        var Bullet = GameObject.Instantiate(Resources.Load<GameObject>(PrefabPaths.BulletPrefab), position, Quaternion.identity, null).GetComponent<Bullet>();
        return Bullet;
    }
}
