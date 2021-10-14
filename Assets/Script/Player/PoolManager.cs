using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    NormalBullet,
    SplitBullet,
    TailBullet,
    LaserBullet,
    LaserPulse,
    Missile,
    Torpedo,
    Bomb,
    Twoway,
    Option,
    ForceField
}

[Serializable] public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject Container;
    public int order = 0;

    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
}

public class PoolManager : MonoBehaviour
{
    [SerializeField] List<PoolInfo> listofPool;

    private void Start()
    {
        for(int i = 0; i < listofPool.Count; i++)
        {

        }
    }

    private void FillPool(PoolInfo info)
    {
        for (int i = 0; i < info.amount; i++)
        {
            GameObject objInstance = null;
            objInstance = Instantiate(info.prefab, info.Container.transform.position, info.Container.transform.rotation, info.Container.transform);
            objInstance.SetActive(false);
            info.pool.Add(objInstance);
        }
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;
        GameObject called = pool[selected.order];
        selected.order++;

        if (selected.order >= pool.Count)
        {
            selected.order = 0;
        }
        return called;
    }

    public PoolInfo GetPoolByType(PoolObjectType type)
    {
        for(int i = 0; i<listofPool.Count;i++)
        {
            if (type == listofPool[i].type)
                return listofPool[i];
        }
        return null;
    }
}
