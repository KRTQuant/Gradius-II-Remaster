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
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


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
    }

    private void GetInput()
    {
        HandleMovement();
        HandleFireArmament();
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

    }

}
