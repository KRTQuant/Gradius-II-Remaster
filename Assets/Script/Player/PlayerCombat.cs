using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] public int live;
    [SerializeField] private int maxLive;
    [SerializeField] private Text liveText;

    [SerializeField] private PoolManager poolManager;
    [SerializeField] private SkillManager skillManager;

    private void Start()
    {
        live = maxLive;
        liveText = GameObject.Find("LifeNumber").GetComponent<Text>();
        liveText.text = live.ToString();
        poolManager = GameObject.FindObjectsOfType<PoolManager>()[0];
    }

    private void Update()
    {
        FindReference();
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
                if (skillManager.laserType == LaserType.RIPPLE)
                {
                    HandleBulletType(PoolObjectType.LaserPulse);
                }
                break;
        }
    }

    public void HandleFireMissile()
    {
        if(skillManager.isMissileActive)
        {
            switch (skillManager.missileType)
            {
                case MissileType.MISSILE:
                    HandleBulletType(PoolObjectType.Missile);
                    break;

                case MissileType.BOMB:
                    HandleBulletType(PoolObjectType.Bomb);
                    break;

                case MissileType.TORPEDO:
                    HandleBulletType(PoolObjectType.Torpedo);
                    break;

                case MissileType.TWOWAY:
                    HandleBulletType(PoolObjectType.Twoway);
                    break;
            }
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

        else if(type == PoolObjectType.Twoway)
        {
            GameObject bullet = poolManager.GetPoolObject(type);
            GameObject bullet2 = poolManager.GetPoolObject(type);
            bullet.GetComponent<ArmamentControl>().currentAmmoType = ArmamentControl.AmmoType.TWOWAYDOWN;
            bullet2.GetComponent<ArmamentControl>().currentAmmoType = ArmamentControl.AmmoType.TWOWAYUP;
            if (bullet.activeSelf)
            {
                //Debug.Log("Reloading");
            }
            if (!bullet.activeSelf)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
            if (bullet2.activeSelf)
            {
                //Debug.Log("Reloading");
            }
            if (!bullet2.activeSelf)
            {
                bullet2.transform.position = transform.position;
                bullet2.SetActive(true);
            }
        }
        else if(type == PoolObjectType.NormalBullet)
        {
            if (skillManager.normalBulletTimer > 0)
            {
                //Debug.Log("Laser is reloading");
            }
            if (skillManager.normalBulletTimer <= 0)
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
                skillManager.normalBulletTimer = skillManager.normalBulletDelay;
            }
            
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

    public void UpdateLiveText()
    {
        liveText.text = live.ToString();
    }

    private void OnEnable()
    {
        UpdateLiveText();
    }

    private void FindReference()
    {
        if(poolManager == null)
            poolManager = GameObject.FindObjectsOfType<PoolManager>()[0];
    }
}