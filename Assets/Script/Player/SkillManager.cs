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
    [SerializeField] public ProjectileWeapon weapon; 

    [Header("Speed")]
    [SerializeField] public int speedLevel;
    [SerializeField] private int speedMaxLevel;

    [Header("Missile")]
    [SerializeField] public MissileType missileType;
    [SerializeField] private int missileLevel;
    [SerializeField] private int missileMaxLevel = 1;
    [SerializeField] public bool isMissileActive;

    [Header("Double")]
    [SerializeField] public DoubleType doubleType;
    [SerializeField] private int doubleLevel;
    [SerializeField] private int doubleMaxLevel = 2;

    [Header("Laser")]
    [SerializeField] public LaserType laserType;
    [SerializeField] private int laserLevel;
    [SerializeField] private int laserMaxLevel = 2;
    [SerializeField] public float laserBulletDelay;
    [SerializeField] public float laserPulseDelay;
    [SerializeField] public float laserTimer;

    [Header("Option")]
    [SerializeField] private GameObject funnelPrefab;
    [SerializeField] private int funnelAmount;
    [SerializeField] private int funnelLevel;
    [SerializeField] private int optionMaxLevel = 5;
    [SerializeField] private bool isFunnelFlowActive;
    [SerializeField] private float funnelFlowTimer;
    [SerializeField] private float funnelFlowDuration;
    [SerializeField] public List<GameObject> listofFunnel;
    [SerializeField] public GameObject FunnelPoolParent;

    [Header("Force Field")]
    [SerializeField] private int ffLevel;
    [SerializeField] private int ffMaxLevel = 1;
    [SerializeField] public bool isFieldActive;

    [Header("Capsule")]
    [SerializeField] public int heldCapsule;
    [SerializeField] private int maxCapsule;

    [Header("Reference")]
    [SerializeField] private SpriteRenderer powerupFrame;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PoolManager poolManager;

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

    private void Update()
    {
        HandleTimer();

    }

    //trigger when collide with 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("YellowCapsule"))
        {
            IncreaseCapsule();
        }
    }

    //set object refecence at start
    private void AccessComponent()
    {
        playerMovement = GetComponent<PlayerMovement>();
        powerupFrame = GameObject.Find("border").GetComponent<SpriteRenderer>();
    }

    //set skill level to Zero
    private void InitSkill_Level()
    {
        speedLevel = 0;
        missileLevel = 0;
        doubleLevel = 0;
        laserLevel = 0;
        funnelLevel = 0;
        ffLevel = 0;
    }

    //Increase number of capsule that player held
    public void IncreaseCapsule()
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
                    laserLevel = 0;
                    weapon = ProjectileWeapon.DOUBLE;
                }
                break;

            case SkillBar.LASER:
                if (laserLevel < laserMaxLevel)
                {
                    laserLevel++;
                    doubleLevel = 0;
                    weapon = ProjectileWeapon.LASER;
                }
                break;

            case SkillBar.OPTION:
                if (funnelLevel < optionMaxLevel)
                {
                    funnelLevel++;
                    CreateFunnel();
                    if(funnelLevel > 4)
                    {
                        isFunnelFlowActive = true;
                    }
                }
                break;

            case SkillBar.FORCEFIELD:
                if (ffLevel < ffMaxLevel)
                {
                    ffLevel++;
                    if (ffLevel != 0)
                    {
                        isFieldActive = true;
                        ActiveForceField();
                    }
                }
                break;
        }
    }

    //Active when press X
    public void HandleUpgradeSkill()
    {
        IncreaseSkillLevel((SkillBar)heldCapsule);
        if (heldCapsule <= 0)
        {
            Debug.Log("You cannot upgrade anything");
        }
        powerupFrame.gameObject.SetActive(false);
        heldCapsule = 0;
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

    private void HandleTimer()
    {
        if (laserTimer > 0)
        {
            laserTimer -= Time.deltaTime;
            if (laserTimer < 0)
                laserTimer = 0;
        }

        if (funnelFlowTimer > 0)
        {
            funnelFlowTimer -= Time.deltaTime;
            if (laserTimer < 0)
                isFunnelFlowActive = false;
        }
    }

    private void ActiveForceField()
    {
        if (isFieldActive)
        {
            Debug.Log("Call Force Field");
            GameObject forcefield = poolManager.GetPoolObject(PoolObjectType.ForceField);
            forcefield.transform.position = gameObject.transform.position;
            forcefield.SetActive(true);
        }
    }

    public void DisableField()
    {
        isFieldActive = false;
        GameObject forcefield = poolManager.GetPoolObject(PoolObjectType.ForceField);
        if(forcefield.activeSelf)
        {
            forcefield.SetActive(false);
        }
        else
        {
            Debug.Log("Disable force field was call but force field doesn't active");
        }
    }

    private void CreateFunnel()
    {
        if(funnelLevel > funnelAmount)
        {
            GameObject funnel = poolManager.GetPoolObject(PoolObjectType.Option);
            funnel.GetComponent<OptionFollowScript>().frameDelay = funnel.GetComponent<OptionFollowScript>().frameDelay * funnelLevel;
            listofFunnel.Add(funnel);
            funnel.SetActive(true);
            //Debug.Log("Create funnel");
        }
    }
}
