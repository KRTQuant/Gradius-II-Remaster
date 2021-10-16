using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Skillset")]
    #region Skillset
    [SerializeField] public Skillset selectedSkillset;
    [SerializeField] public enum Skillset { SET_A, SET_B, SET_C, SET_D }
    #region Speed
    [SerializeField] private int speedLevel;
    [SerializeField] private int speedMaxLevel;
    [SerializeField] private int speedAddon;
    [SerializeField] private int speed;
    #endregion
    #region Missile
    [SerializeField] private bool isMissileActive;
    [SerializeField] private float missileTimer = 0;
    [SerializeField] private float missileCooldown;
    [SerializeField] private MissileType currentMissile;
    [SerializeField] private enum MissileType { MISSILE, TORPEDO, BOMB, TWOWAY }
    #endregion
    #region Projectile
    [SerializeField] private ProjectileGun currentProjectile;
    [SerializeField] private enum ProjectileGun { SPLIT, TAIL }
    #endregion
    #region Laser
    [SerializeField] private LaserGun currentLaser;
    [SerializeField] private enum LaserGun { BULLET, PULSE }
    [SerializeField] private float laserCooldown;
    [SerializeField] private float laserTimer;
    #endregion
    #region Option
    [SerializeField] private int OptionAmount;
    #endregion
    #region Force Field
    [SerializeField] private bool isForceFieldActive;
    #endregion
    #endregion

    [SerializeField] private PrimaryGunType primaryGun;
    [SerializeField] private enum PrimaryGunType { NORMAL, PROJECTILE, LASER }

    [Header("Reference")]
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private PoolManager poolManager;


    private void HandleSelectSkillset(Skillset select)
    {
        switch(select)
        {
            case Skillset.SET_A:
                currentMissile = MissileType.MISSILE;
                currentProjectile = ProjectileGun.SPLIT;
                currentLaser = LaserGun.BULLET;
                break;

            case Skillset.SET_B:
                currentMissile = MissileType.BOMB;
                currentProjectile = ProjectileGun.TAIL;
                currentLaser = LaserGun.BULLET;
                break;

            case Skillset.SET_C:
                currentMissile = MissileType.TORPEDO;
                currentProjectile = ProjectileGun.SPLIT;
                currentLaser = LaserGun.PULSE;
                break;

            case Skillset.SET_D:
                currentMissile = MissileType.TWOWAY;
                currentProjectile = ProjectileGun.TAIL;
                currentLaser = LaserGun.PULSE;
                break;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        GetInput();
        HandleCooldown();
    }

    private void GetInput()
    {
        HandleMovement();
        HandleFireArmament();
        HandleFireMissile();
    }

    private void HandleMovement()
    {
        #region Movement
        #region GetKey and GetKeyDown
        float velocity = (speed + (speedAddon * speedLevel)) * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -velocity);
            animator.SetBool("flydown", true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, velocity);
            animator.SetBool("flyup", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocity, rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocity, rb.velocity.y);
        }
        #endregion

        #region GetKeyUP
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(0 * Time.deltaTime, rb.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0 * Time.deltaTime);
            animator.SetBool("releaseFlyup", true);
            animator.SetBool("releaseFlydown", true);
            animator.SetBool("flydown", false);
            animator.SetBool("flyup", false);
        }
        #endregion
        #endregion
    }

    private void HandleFireArmament()
    {
        if(Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space))
        {
            if (primaryGun == PrimaryGunType.NORMAL)
            {
                GameObject bullet = poolManager.GetPoolObject(PoolObjectType.NormalBullet);
                if(bullet.activeSelf)
                {
                    Debug.Log("Reloading");
                }    
                if(!bullet.activeSelf)
                {
                    bullet.transform.position = bulletSpawnPos.position;
                    bullet.SetActive(true);
                }

            }
            if (primaryGun == PrimaryGunType.PROJECTILE)
            {
                if (currentProjectile == ProjectileGun.SPLIT)
                {
                    GameObject bullet1 = poolManager.GetPoolObject(PoolObjectType.NormalBullet);
                    GameObject bullet2 = poolManager.GetPoolObject(PoolObjectType.SplitBullet);
                    bullet2.GetComponent<ArmamentControl>().currentAmmoType = ArmamentControl.AmmoType.SECOND;
                    if (bullet1.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!bullet1.activeSelf)
                    {
                        bullet1.transform.position = bulletSpawnPos.position;
                        bullet1.SetActive(true);
                    }
                    if (bullet2.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!bullet2.activeSelf)
                    {
                        bullet2.transform.position = bulletSpawnPos.position;
                        bullet2.SetActive(true);
                    }
                }

                if (currentProjectile == ProjectileGun.TAIL)
                {
                    GameObject bullet1 = poolManager.GetPoolObject(PoolObjectType.NormalBullet);
                    GameObject bullet2 = poolManager.GetPoolObject(PoolObjectType.TailBullet);
                    bullet2.GetComponent<ArmamentControl>().currentAmmoType = ArmamentControl.AmmoType.TAIL;
                    if (bullet1.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!bullet1.activeSelf)
                    {
                        bullet1.transform.position = bulletSpawnPos.position;
                        bullet1.SetActive(true);
                    }
                    if (bullet2.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!bullet2.activeSelf)
                    {
                        bullet2.transform.position = bulletSpawnPos.position;
                        bullet2.SetActive(true);
                    }
                }
            }
            if (primaryGun == PrimaryGunType.LASER)
            {
                if (currentLaser == LaserGun.BULLET)
                {
                    GameObject bullet1 = poolManager.GetPoolObject(PoolObjectType.LaserBullet);
                    if (bullet1.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!bullet1.activeSelf)
                    {
                        bullet1.transform.position = bulletSpawnPos.position;
                        bullet1.SetActive(true);
                    }
                }

                if (currentLaser == LaserGun.PULSE && laserTimer <= 0)
                {
                    GameObject bullet1 = poolManager.GetPoolObject(PoolObjectType.LaserPulse);
                    if (bullet1.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!bullet1.activeSelf)
                    {
                        bullet1.transform.position = bulletSpawnPos.position;
                        bullet1.SetActive(true);
                        laserTimer = laserCooldown;
                    }
                }
            }
        }

    }

    private void HandleCooldown()
    {
        if (laserTimer > 0)
            laserTimer -= Time.deltaTime;
        if (missileTimer > 0)
            missileTimer -= Time.deltaTime;
    }

    private void HandleFireMissile()
    {
        if(isMissileActive)
        {
            if ((Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space)) && missileTimer <= 0)
            {
                if (currentMissile == MissileType.MISSILE)
                {
                    GameObject missile = poolManager.GetPoolObject(PoolObjectType.Missile);
                    if (missile.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!missile.activeSelf)
                    {
                        missile.transform.position = bulletSpawnPos.position;
                        missile.SetActive(true);
                    }
                }

                if (currentMissile == MissileType.BOMB)
                {
                    GameObject missile = poolManager.GetPoolObject(PoolObjectType.Bomb);
                    if (missile.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!missile.activeSelf)
                    {
                        missile.transform.position = bulletSpawnPos.position;
                        missile.SetActive(true);
                    }
                }

                if (currentMissile == MissileType.TORPEDO)
                {
                    GameObject missile = poolManager.GetPoolObject(PoolObjectType.Torpedo);
                    if (missile.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!missile.activeSelf)
                    {
                        missile.transform.position = bulletSpawnPos.position;
                        missile.SetActive(true);
                    }
                }

                if (currentMissile == MissileType.TWOWAY)
                {
                    GameObject missile1 = poolManager.GetPoolObject(PoolObjectType.Missile);
                    GameObject missile2 = poolManager.GetPoolObject(PoolObjectType.Missile);
                    missile1.GetComponent<ArmamentControl>().currentAmmoType = ArmamentControl.AmmoType.TWOWAYUP;
                    missile2.GetComponent<ArmamentControl>().currentAmmoType = ArmamentControl.AmmoType.TWOWAYDOWN;
                    if (missile1.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!missile1.activeSelf)
                    {
                        missile1.transform.position = bulletSpawnPos.position;
                        missile1.SetActive(true);
                    }
                    if (missile2.activeSelf)
                    {
                        Debug.Log("Reloading");
                    }
                    if (!missile2.activeSelf)
                    {
                        missile2.transform.position = bulletSpawnPos.position;
                        missile2.SetActive(true);
                    }
                }

                missileTimer = missileCooldown;
            }
        }
    }

}
