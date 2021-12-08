using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get { return _instance; } }
    public static PlayerMovement _instance;

    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SkillManager skillManager;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private GameManager gameManager;

    [Header("Velocity")]
    [SerializeField] public float constantSpeed;
    [SerializeField] public float addonSpeed;

    [Header("Input")]
    [SerializeField] public bool isUpArrowPressed;
    [SerializeField] public bool isDownArrowPressed;
    [SerializeField] public bool isLeftArrowPressed;
    [SerializeField] public bool isRightArrowPressed;

    [Header("Initial Pos")]
    [SerializeField] public Vector2 initPos;

    private void Awake()
    {

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        skillManager = GetComponent<SkillManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        initPos = transform.position;
    }

    private void Update()
    {
        GetInput(); 
    }

    private void FixedUpdate()
    {
        SetInputReq();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //player dead
            this.gameObject.SetActive(false);
            HandleAfterDead();
            //reset cam position and spawn at checkpoint
        }

        if(collision.CompareTag("EnemyBullet"))
        {
            if(skillManager.isFieldActive)
            {
                skillManager.DisableField();
                return;
            }
            else
            {
                this.gameObject.SetActive(false);
                //player dead
                HandleAfterDead();
                //reset and spawn at the checkpoint
            }
        }
    }


    private void GetInput()
    {
        #region Key Down
        if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKey(KeyCode.UpArrow)))
        {
            animator.SetBool("flyup", true);
            isUpArrowPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKey(KeyCode.DownArrow)))
        {
            animator.SetBool("flydown", true);
            isDownArrowPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            isLeftArrowPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKey(KeyCode.RightArrow)))
        {
            isRightArrowPressed = true;
        }
        #endregion

        #region Key Up
        if (Input.GetKeyUp(KeyCode.UpArrow) || (Input.GetKeyUp(KeyCode.DownArrow)))
        {
            isUpArrowPressed = false;
            isDownArrowPressed = false;

            animator.SetBool("releaseFlyup", true);
            animator.SetBool("releaseFlydown", true);
            animator.SetBool("flydown", false);
            animator.SetBool("flyup", false);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || (Input.GetKeyUp(KeyCode.LeftArrow)))
        {
            isRightArrowPressed = false;
            isLeftArrowPressed = false;
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.X))
        {
            skillManager.HandleUpgradeSkill();
        }

        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKey(KeyCode.Z))
        {
            playerCombat.HandleFireGun();
            playerCombat.HandleFireMissile();
            if(skillManager.listofFunnel.Count > 0)
            {
                foreach(var funnel in skillManager.listofFunnel)
                {
                    funnel.GetComponent<OptionFollowScript>().HandleFireGun();
                    funnel.GetComponent<OptionFollowScript>().HandleFireMissile();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Set time scale to 0");
        }

        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            skillManager.IncreaseCapsule();
        }
    }

    public float SetVelocity()
    {
        float velocity = ((skillManager.speedLevel) * addonSpeed) + constantSpeed;
        return velocity;
    }

    private void SetInputReq()
    {
        if (isUpArrowPressed) 
        { 
            rb.velocity = new Vector2(rb.velocity.x, SetVelocity());
        }
        if (isDownArrowPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -SetVelocity());
        }
        if (isLeftArrowPressed)
        {
            rb.velocity = new Vector2(-SetVelocity(), rb.velocity.y);
        }
        if (isRightArrowPressed)
        {
            rb.velocity = new Vector2(SetVelocity(), rb.velocity.y);
        }

        if (!isUpArrowPressed && !isDownArrowPressed)
            rb.velocity = new Vector2(rb.velocity.x, 0);
        if (!isRightArrowPressed && !isLeftArrowPressed)
            rb.velocity = new Vector2(0, rb.velocity.y);
    } 

    public void HandleAfterDead()
    {
        playerCombat.live--;
        if (playerCombat.live > 0)
        {
            //revive
            PhaseManager phaseManager = GameObject.Find("PhaseManager").GetComponent<PhaseManager>();
            phaseManager.LoadCheckPoint();
            //reset enemy phase
        }

        else
        {
            //Go to Main Menu
            Debug.Log("Go to main menu");
            gameManager.HandlePlayerDeath();
        }
    }

    public void ResetPosition()
    {
        transform.position = initPos;
    }
}
