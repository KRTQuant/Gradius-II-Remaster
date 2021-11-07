using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] public int live;
    [SerializeField] private int maxLive;

    [SerializeField] public int score;

    [SerializeField] private PoolManager poolManager;
    [SerializeField] private SkillManager skillManager;

    private void Start()
    {
        live = maxLive;
    }

    public void HandleFireGun()
    {
        switch (skillManager.weapon)
        {
            case ProjectileWeapon.NORMAL:
                HandleBulletType(PoolObjectType.NormalBullet);
                break;

            case ProjectileWeapon.DOUBLE:
                if(skillManager.doubleType == DoubleType.SPLIT)
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
        if(type == PoolObjectType.LaserBullet)
        {
            if(skillManager.laserTimer > 0)
            {
                //.Log("Laser is reloading");
            }
            if(skillManager.laserTimer <= 0)
            {
                GameObject bullet = poolManager.GetPoolObject(type);
                if (bullet.activeSelf)
                {
                    //Debug.Log("Reloading");
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
                //Debug.Log("Laser is reloading");
            }
            if (skillManager.laserTimer <= 0)
            {
                GameObject bullet = poolManager.GetPoolObject(type);
                if (bullet.activeSelf)
                {
                    //Debug.Log("Reloading");
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
                //Debug.Log("Reloading");
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
                //Debug.Log("Reloading");
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
                //Debug.Log("Reloading");
            }
            if (!bullet.activeSelf)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
        }
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
    }
}