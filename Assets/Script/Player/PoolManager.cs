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


    public List<GameObject> pool = new List<GameObject>();
}

public class PoolManager : MonoBehaviour
{
    [SerializeField] List<PoolInfo> listofPool;
    [SerializeField] SkillManager skillManager;

    private void Start()
    {
        Debug.Log("Active poolManager");
        for(int i = 0; i < listofPool.Count; i++)
        {
            FillPool(listofPool[i]);
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
            //Debug.Log(info.type.ToString());
        }
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        Debug.Log(selected.pool[selected.order]);
        List<GameObject> pool = selected.pool;
        Debug.Log(pool[selected.order]);
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

    public void SetMissilePool()
    {
        skillManager = GameObject.Find("Player").GetComponent<SkillManager>();
        if (skillManager.missileType == MissileType.MISSILE)
        {
            PoolInfo poolInfo = GetPoolByType(PoolObjectType.Missile);
            Debug.Log("Set missile pool amount = 1");
            poolInfo.amount = 1;
            poolInfo.pool.RemoveAt(poolInfo.pool.Count-1);
        }
    }
}
