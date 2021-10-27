using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Skillset")]
    #region Skillset
    [SerializeField] public Skillset selectedSkillset;
    [SerializeField] public enum Skillset { SET_A, SET_B, SET_C, SET_D }
    #region Speed
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
    [SerializeField] private float laserPulseCooldown;
    [SerializeField] private float laserPulseTimer;
    [SerializeField] private float laserBulletCooldown;
    [SerializeField] private float laserBulletTimer;
    #endregion
    #region Option
    [SerializeField] private int OptionAmount;
    #endregion
    #region Force Field
    [SerializeField] private bool isForceFieldActive;
    #endregion
    #endregion
    [Header("Gun Weapon")]
    #region Gun Weapon
    [SerializeField] private PrimaryGunType primaryGun;
    [SerializeField] private enum PrimaryGunType { NORMAL, PROJECTILE, LASER }
    #endregion
    [Header("Lives")]
    #region Lives
    [SerializeField] private float maxLive;
    [SerializeField] private float currentLive;
    [SerializeField] private Text liveText;
    #endregion
    [Header("Capsule")]
    #region Capsule
    [SerializeField] private int currentCapsule;
    [SerializeField] private float maxCapsule;
    #endregion
    [Header("PowerUp Level")]
    #region PowerUp Level
    [SerializeField] private PowerupList currentPowerup;
    [SerializeField] private enum PowerupList { NULL, SPEED, MISSILE, DOUBLE, LASER, OPTION, FORCEFIELD }
    [SerializeField] public int speedLevel;
    [HideInInspector] private int speedMaxLevel = 7;
    [SerializeField] private int missileLevel;
    [HideInInspector] private int missileMaxLevel = 1;
    [SerializeField] public float projectileLevel;
    [HideInInspector] private int projectileMaxLevel = 2;
    [SerializeField] public float laserLevel;
    [HideInInspector] private float laserMaxLevel = 2;
    [SerializeField] public float optionLevel;
    [HideInInspector] private float optionMaxLevel = 4;
    [SerializeField] public float forcefieldLevel;
    [HideInInspector] private float forcefieldMaxLevel = 1;

    [SerializeField] private SpriteRenderer powerupBorder;
    #endregion
    [Header("Pause")]
    [SerializeField] private bool isPause;

    [Header("Reference")]
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private SpriteRenderer sprite;


    public void HandleSelectSkillset(Skillset select)
    {
        switch(select)
        {
            case Skillset.SET_A:
                currentMissile = MissileType.MISSILE;
                currentProjectile = ProjectileGun.SPLIT;
                currentLaser = LaserGun.BULLET;
                Debug.Log("Skillset was set to A");
                break;

            case Skillset.SET_B:
                currentMissile = MissileType.BOMB;
                currentProjectile = ProjectileGun.TAIL;
                currentLaser = LaserGun.BULLET;
                Debug.Log("Skillset was set to B");
                break;

            case Skillset.SET_C:
                currentMissile = MissileType.TORPEDO;
                currentProjectile = ProjectileGun.SPLIT;
                currentLaser = LaserGun.PULSE;
                Debug.Log("Skillset was set to C");
                break;

            case Skillset.SET_D:
                currentMissile = MissileType.TWOWAY;
                currentProjectile = ProjectileGun.TAIL;
                currentLaser = LaserGun.PULSE;
                Debug.Log("Skillset was set to D");
                break;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        SetMaxLive();

    }

    private void Update()
    {
        GetInput();

        HandleForceField();
    }

    private void FixedUpdate()
    {
        // HandlePause();
        HandleCooldown();
    }

    private void GetInput()
    {
        HandleMovement();
        HandleFireArmament();
        HandleFireMissile();
        HandlePowerup();
        HandleDebug();
        HandlePause();
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
                if (currentLaser == LaserGun.BULLET && laserBulletTimer <= 0)
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
                        laserBulletTimer = laserBulletCooldown;
                    }

                    //tab => call 2 bullet
                    //hold => call all viable bullet
                }

                if (currentLaser == LaserGun.PULSE && laserPulseTimer <= 0)
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
                        laserPulseTimer = laserPulseCooldown;
                    }
                }
            }
        }

    }

    private void HandleCooldown()
    {
        if (laserPulseTimer > 0)
            laserPulseTimer -= Time.deltaTime;
        if (laserPulseTimer <= 0)
            laserPulseTimer = 0;

        if (missileTimer > 0)
            missileTimer -= Time.deltaTime;
        if (missileTimer <= 0)
            missileTimer = 0;

        if (laserBulletTimer > 0)
            laserBulletTimer -= Time.fixedDeltaTime;
        if (laserBulletTimer <= 0)
            laserBulletTimer = 0;
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

    private void HandlePowerup()
    {
        SetPowerupBorderPosition();
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentCapsule == 0)
            {
                Debug.Log("Cannot upgrade any shit");
            }
            HandleIncreaseLevel((PowerupList)currentCapsule);
            Debug.Log((PowerupList)(currentCapsule));
            currentCapsule = 0;
        }
    }

    private void HandlePause()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (isPause)
            {
                Time.timeScale = 1;
                isPause = false;
            }
            else
            {
                Time.timeScale = 0;
                isPause = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Capsule"))
        {
            Debug.Log("Compare with capsule");
            currentCapsule++;
            if(currentCapsule > 6)
            {
                currentCapsule = 1;
            }
            collision.gameObject.SetActive(false);
        }

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("collide enemy");
            currentLive--;
            StartCoroutine(Delay());
            sprite.enabled = false;
        }
    }

    private void HandleIncreaseLevel(PowerupList type)
    {
        if(type == PowerupList.SPEED)
        {
            if (speedLevel < speedMaxLevel)
                speedLevel++;
            else
            {
                Debug.Log("Max speed");
            }
        }
        if (type == PowerupList.MISSILE)
        {
            if (missileLevel < missileMaxLevel)
                missileLevel++;
            if (missileLevel >= 0)
                isMissileActive = true;
            else
            {
                Debug.Log("Max MISSILE");
            }
        }
        if (type == PowerupList.DOUBLE)
        {
            if (projectileLevel < projectileMaxLevel)
                projectileLevel++;
            if (projectileLevel >= 0)
            {
                primaryGun = PrimaryGunType.PROJECTILE;
                laserLevel = 0;
            }
            else
            {
                Debug.Log("Max DOUBLE");
            }
        }
        if (type == PowerupList.LASER)
        {
            if (laserLevel < laserMaxLevel)
                laserLevel++;
            if (laserLevel >= 0)
            {
                primaryGun = PrimaryGunType.LASER;
                projectileLevel = 0;
            }
            else
            {
                Debug.Log("Max LASER");
            }
        }
        if (type == PowerupList.OPTION)
        {
            if (optionLevel < optionMaxLevel)
                optionLevel++;
            else
            {
                Debug.Log("Max OPTION");
            }
        }
        if (type == PowerupList.FORCEFIELD)
        {
            if (forcefieldLevel < forcefieldMaxLevel)
                forcefieldLevel++;
            if(forcefieldLevel != 0)
            {
                isForceFieldActive = true;
            }
            else
            {
                Debug.Log("Max FORCEFIELD");
            }
        }

    }

    private void HandleDebug()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("Increase current capsule");
            currentCapsule++;

            if(currentCapsule > maxCapsule)
            {
                currentCapsule = 1;
            }
        }

    }

    private void SetPowerupBorderPosition()
    {
        if (currentCapsule == 0)
            powerupBorder.gameObject.SetActive(false);
        if (currentCapsule > 0)
        {
            //Debug.Log("Active Power-Up Border");
            powerupBorder.gameObject.SetActive(true);
        }
        powerupBorder.transform.localPosition = new Vector3(-3645 + ((currentCapsule-1) * 1478.4f), -405, 0);
        //Debug.Log(powerupBorder.transform.localPosition);
    }

    private void SetLive()
    {
        liveText.text = "Life : 0" + currentLive;
    }

    private void SetMaxLive()
    {
        currentLive = maxLive;
        SetLive();
    }

    private void HandleForceField()
    {
        GameObject forcefield = poolManager.GetPoolObject(PoolObjectType.ForceField);
        if (isForceFieldActive)
        {
            forcefield.transform.position = transform.position;
            forcefield.SetActive(true);
        }
        else
        {
            forcefield.SetActive(false);
        }
    }

    IEnumerator Delay()
    {
        Debug.Log("call delay");
        yield return new WaitForSeconds(2);
        Debug.Log("Finish delay");
        SceneManager.LoadScene("UI_Menu");
    }

}
