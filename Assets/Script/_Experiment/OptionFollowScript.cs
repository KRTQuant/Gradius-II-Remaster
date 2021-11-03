using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionFollowScript : MonoBehaviour
{
    [Header("Following")]
    public Transform followTransform;
    public int frameDelay = 60;
    private List<Vector3> positionList = new List<Vector3>();

    [Header("Combat")]
    [SerializeField] private GameObject funnelPool;

    [Header("Reference")]   
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SkillManager skillManager;
    [SerializeField] private PoolManager poolManager;

    private void Start()
    {
        SetPlayerReference();
        for (int i = 0; i < frameDelay; i++)
        {
            positionList.Add(followTransform.position);
        }
    }

    private void Update()
    {
        if (playerMovement == null || skillManager == null)
            SetPlayerReference();
        if(poolManager == null)
        {
            poolManager = Instantiate<GameObject>(funnelPool, skillManager.FunnelPoolParent.transform).GetComponentInChildren<PoolManager>();
        }
        UpdatePositionList();
        FollowPlayer();
    }

    private void UpdatePositionList()
    {
        if (playerMovement.isDownArrowPressed || playerMovement.isUpArrowPressed ||
        playerMovement.isLeftArrowPressed || playerMovement.isRightArrowPressed)
        {
            positionList.Add(followTransform.position);
        }
    }

    private void FollowPlayer()
    {
        if (positionList.Count > frameDelay)
        {
            transform.position = positionList[0];
            positionList.RemoveAt(0);
        }
    }

    public void HandleFireGun()
    {
        switch (skillManager.weapon)
        {
            case ProjectileWeapon.NORMAL:
                HandleBulletType(PoolObjectType.NormalBullet);
                break;

            case ProjectileWeapon.DOUBLE:
                if (skillManager.doubleType == DoubleType.SPLIT)
                {
                    HandleBulletType(PoolObjectType.NormalBullet);
                    HandleBulletType(PoolObjectType.SplitBullet);
                }
                if (skillManager.doubleType == DoubleType.TAIL)
                {
                    HandleBulletType(PoolObjectType.NormalBullet);
                    HandleBulletType(PoolObjectType.TailBullet);
                }
                break;

            case ProjectileWeapon.LASER:
                if (skillManager.laserType == LaserType.BULLET)
                {
                    HandleBulletType(PoolObjectType.LaserBullet);
                }
                break;
        }
    }

    public void HandleBulletType(PoolObjectType type)
    {
        if (type == PoolObjectType.LaserBullet)
        {
            if (skillManager.laserTimer > 0)
            {
                Debug.Log("Laser is reloading");
            }
            if (skillManager.laserTimer <= 0)
            {
                GameObject bullet = poolManager.GetPoolObject(type);
                if (bullet.activeSelf)
                {
                    Debug.Log("Reloading");
                }
                if (!bullet.activeSelf)
                {
                    bullet.transform.position = transform.position;
                    bullet.SetActive(true);
                }
                skillManager.laserTimer = skillManager.laserBulletDelay;
            }
        }
        else if (type == PoolObjectType.LaserPulse)
        {
            if (skillManager.laserTimer > 0)
            {
                Debug.Log("Laser is reloading");
            }
            if (skillManager.laserTimer <= 0)
            {
                GameObject bullet = poolManager.GetPoolObject(type);
                if (bullet.activeSelf)
                {
                    Debug.Log("Reloading");
                }
                if (!bullet.activeSelf)
                {
                    bullet.transform.position = transform.position;
                    bullet.SetActive(true);
                }
                skillManager.laserTimer = skillManager.laserPulseDelay;
            }
        }
        else if (type == PoolObjectType.SplitBullet)
        {
            GameObject bullet = poolManager.GetPoolObject(type);
            bullet.GetComponent<ArmamentControl>().currentAmmoType = ArmamentControl.AmmoType.SECOND;
            if (bullet.activeSelf)
            {
                Debug.Log("Reloading");
            }
            if (!bullet.activeSelf)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
            skillManager.laserTimer = skillManager.laserPulseDelay;
        }
        else if (type == PoolObjectType.TailBullet)
        {
            GameObject bullet = poolManager.GetPoolObject(type);
            bullet.GetComponent<ArmamentControl>().currentAmmoType = ArmamentControl.AmmoType.TAIL;
            if (bullet.activeSelf)
            {
                Debug.Log("Reloading");
            }
            if (!bullet.activeSelf)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
            skillManager.laserTimer = skillManager.laserPulseDelay;
        }
        else
        {
            GameObject bullet = poolManager.GetPoolObject(type);
            if (bullet.activeSelf)
            {
                Debug.Log("Reloading");
            }
            if (!bullet.activeSelf)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
        }
    }

    private void SetPlayerReference()
    {
        skillManager = GameObject.Find("Player").GetComponent<SkillManager>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
}
