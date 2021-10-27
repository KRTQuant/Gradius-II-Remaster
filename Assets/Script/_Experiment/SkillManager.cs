using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skillset
{
    SET_A,
    SET_B,
    SET_C,
    SET_D
}

public enum MissileType
{
    MISSILE,
    BOMB,
    TORPEDO,
    TWOWAY
}

public enum DoubleType
{
    SPLIT,
    TAIL
}

public enum LaserType
{
    BULLET,
    RIPPLE
}

public enum SkillBar
{ 
    NULL,
    SPEED,
    MISSILE,
    DOUBLE,
    LASER,
    OPTION,
    FORCEFIELD
}

public enum ProjectileWeapon
{
    NORMAL,
    DOUBLE,
    LASER
}
public class SkillManager : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private ProjectileWeapon weapon; 

    [Header("Speed")]
    [SerializeField] public int speedLevel;
    [SerializeField] private int speedMaxLevel;

    [Header("Missile")]
    [SerializeField] public MissileType missileType;
    [SerializeField] private int missileLevel;
    [SerializeField] private int missileMaxLevel = 1;
    [SerializeField] private bool isMissileActive;

    [Header("Double")]
    [SerializeField] private DoubleType doubleType;
    [SerializeField] private int doubleLevel;
    [SerializeField] private int doubleMaxLevel = 2;

    [Header("Laser")]
    [SerializeField] private LaserType laserType;
    [SerializeField] private int laserLevel;
    [SerializeField] private int laserMaxLevel = 2;

    [Header("Option")]
    [SerializeField] private int optionAmount;
    [SerializeField] private int optionLevel;
    [SerializeField] private int optionMaxLevel = 5;
    [SerializeField] private bool isFunnelFlowActive;
    [SerializeField] private float funnelFlowTimer;
    [SerializeField] private float funnelFlowDuration;

    [Header("Force Field")]
    [SerializeField] private int ffLevel;
    [SerializeField] private int ffMaxLevel = 1;
    [SerializeField] private bool isFieldActive;

    [Header("Capsule")]
    [SerializeField] private int heldCapsule;
    [SerializeField] private int maxCapsule;

    [Header("Reference")]
    [SerializeField] private SpriteRenderer powerupFrame;
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        weapon = ProjectileWeapon.NORMAL;
        InitSkill_Level();
        AccessComponent();
    }

    private void FixedUpdate()
    {
        ActiveFunnelFlow();
    }

    //trigger when collide with 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Capsule"))
        {
            IncreaseCapsule();
        }
    }

    //set object refecence at start
    private void AccessComponent()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    //set skill level to Zero
    private void InitSkill_Level()
    {
        speedLevel = 0;
        missileLevel = 0;
        doubleLevel = 0;
        laserLevel = 0;
        optionLevel = 0;
        ffLevel = 0;
    }

    //Increase number of capsule that player held
    private void IncreaseCapsule()
    {
        heldCapsule++;
        if (heldCapsule == 0)
            powerupFrame.gameObject.SetActive(false);
        if (heldCapsule > 0)
        {
            //Debug.Log("Active Power-Up Border");
            powerupFrame.gameObject.SetActive(true);
        }
        if (heldCapsule > 6)
        {
            heldCapsule = 1;
        }
        SetPowerFramePosition();
    }

    //Set frame of position
    private void SetPowerFramePosition()
    {
        powerupFrame.transform.localPosition = new Vector3(-3645 + ((heldCapsule - 1) * 1478.4f), -405, 0);
    }

    //Set skill preset
    public void SetSkillPreset(Skillset type)
    {
        switch (type)
        {
            case Skillset.SET_A:
                missileType = MissileType.MISSILE;
                doubleType = DoubleType.SPLIT;
                laserType = LaserType.BULLET;
                break;

            case Skillset.SET_B:
                missileType = MissileType.BOMB;
                doubleType = DoubleType.TAIL;
                laserType = LaserType.BULLET;
                break;

            case Skillset.SET_C:
                missileType = MissileType.TORPEDO;
                doubleType = DoubleType.SPLIT;
                laserType = LaserType.RIPPLE;
                break;

            case Skillset.SET_D:
                missileType = MissileType.TWOWAY;
                doubleType = DoubleType.TAIL;
                laserType = LaserType.RIPPLE;
                break;
        }
    }

    //Active when level increase
    private void IncreaseSkillLevel(SkillBar skillBar)
    {
        switch (skillBar)
        {
            case SkillBar.SPEED:
                if (speedLevel < speedMaxLevel)
                {
                    speedLevel++;
                }
                break;

            case SkillBar.MISSILE:
                if (missileLevel < missileMaxLevel)
                {
                    missileLevel++;
                    if (missileLevel > 0)
                        isMissileActive = true;
                }
                break;

            case SkillBar.DOUBLE:
                if (doubleLevel < doubleMaxLevel)
                {
                    doubleLevel++;
                    weapon = ProjectileWeapon.DOUBLE;
                }
                break;

            case SkillBar.LASER:
                if (laserLevel < laserMaxLevel)
                {
                    laserLevel++;
                    weapon = ProjectileWeapon.LASER;
                }
                break;

            case SkillBar.OPTION:
                if (optionLevel < optionMaxLevel)
                {
                    optionLevel++;
                    optionAmount++;
                    if(optionLevel > 4)
                    {
                        isFunnelFlowActive = true;
                    }
                }
                break;

            case SkillBar.FORCEFIELD:
                if (ffLevel < ffMaxLevel)
                {
                    ffLevel++;

                }
                break;
        }
    }

    public void HandleUpgradeSkill()
    {
        if(heldCapsule <= 0)
        {
            Debug.Log("You cannot upgrade anything");
        }
        IncreaseSkillLevel((SkillBar)heldCapsule);
    }

    private void ActiveFunnelFlow()
    {
        if(isFunnelFlowActive && funnelFlowTimer > 0)
        {
            funnelFlowTimer -= Time.deltaTime;
        }

        if(funnelFlowTimer <= 0)
        {
            funnelFlowTimer = funnelFlowDuration;
            isFunnelFlowActive = false;
        }
    }
}
