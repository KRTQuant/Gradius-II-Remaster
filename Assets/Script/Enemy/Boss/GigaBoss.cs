using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class GigaBoss : UnitAbs
{
    [SerializeField] private enum Behavior { PATTERN1, PATTERN2, PATTERN3 }
    [SerializeField] private Behavior currentBehavior;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Transform player;


    #region Pattern 1 Variables
    [Header("Pattern 1")]
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private enum Pattern1 { DROP, BOUNCE, JUMPBACK, DISABLED }
    [SerializeField] private Pattern1 statePattern1;
    [SerializeField] private int jumpCounter = 0;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpBackForce;
    [SerializeField] private bool isJumping;
    [SerializeField] private Transform lowestPoint;
    [SerializeField] private float degree;
    [SerializeField] private float jumpBackDegree;
    [SerializeField] private bool isGround;
    [SerializeField] private bool isFinishAppear; 
    [SerializeField] private bool isActivePattern1;
    [SerializeField] private bool finishJump;
    [SerializeField] private bool finishPattern1;
    [SerializeField] private Transform middleRightPoint;
    [SerializeField] private float jumpBackOffset;
    #endregion

    public override void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        SetHealth();
    }

    #region Pattern 2 Variables
    [Header("Pattern 2 Variables")]
    [SerializeField] private Pattern2 statePattern2;
    [SerializeField] private enum Pattern2 { GOFORTH, GOBACK, DISABLED }
    [SerializeField] private bool isGoForth;
    [SerializeField] private bool isGoBack;
    [SerializeField] private bool isSetDir;
    [SerializeField] private Transform stopPointLeft;
    [SerializeField] private Transform stopPointRight;
    #endregion

    #region Pattern 3 Variables
    [Header("Pattern 3")]
    [SerializeField] private Pattern3 statePattern3;
    [SerializeField] private enum Pattern3 { MOVE1, MOVE2, BEAM, DISABLED }
    [SerializeField] private Vector2 lastPos;
    [SerializeField] private bool isDistanceFull;
    [SerializeField] private bool isDirUp;
    [SerializeField] private float maxDistance;
    [SerializeField] private float totalDistance;
    [SerializeField] private float maxDistanceCounter;
    [SerializeField] private float verticalOffset;
    [SerializeField] private bool isMoving;
    #endregion

    #region Pattern1
    private void HandleDrop()
    {
        if (lowestPoint.position.y >= transform.position.y && rb.velocity.y < 0)
        {
            if (jumpCounter >= 5)
            {
                finishJump = true;
                HandleJumpBack();
                statePattern1 = Pattern1.JUMPBACK;
            }
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            if (jumpCounter < 5)
                Jump();
        }
    }
    private void JumpBack()
    {
        Vector2 dir = new Vector2(Mathf.Sin(jumpBackDegree * Mathf.Deg2Rad), Mathf.Cos(jumpBackDegree * Mathf.Deg2Rad));
        //jumpBackForce = 15;
        rb.gravityScale = 1;
        rb.velocity = dir * jumpBackForce;
    }
    private void HandleJumpBack()
    {
        if (middleRightPoint.position.x - jumpBackOffset < transform.position.x && middleRightPoint.position.x + jumpBackOffset/ 5 > transform.position.x)
        {
            Debug.Log("Stop from JumpBack");
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }
    private void Jump()
    {
        statePattern1 = Pattern1.BOUNCE;
        Debug.Log("Pattern 1 's State : " + statePattern1.ToString());
        rb.gravityScale = 1;
        Vector2 dir = jumpCounter < 1 ? new Vector2(Mathf.Sin(280 * Mathf.Deg2Rad), Mathf.Cos(280 * Mathf.Deg2Rad)) :
    new Vector2(Mathf.Sin(degree * Mathf.Deg2Rad), Mathf.Cos(degree * Mathf.Deg2Rad));
        jumpForce = jumpCounter < 1 ? 5 : 9 - jumpCounter;
        rb.velocity = dir * jumpForce;
        jumpCounter++;
        statePattern1 = Pattern1.DROP;
        Debug.Log("Pattern 1 's State : " + statePattern1.ToString());
    }
    #endregion

    private void Update()
    {
        if(isStart)
        {
            CheckDeath();
            //Debug.DrawRay(transform.position, new Vector2(Mathf.Sin(degree * Mathf.Deg2Rad), Mathf.Cos(degree * Mathf.Deg2Rad)), Color.red);
            if (currentBehavior == Behavior.PATTERN1)
            {
                if (isFinishAppear && !isActivePattern1) //Active Pattern 1
                {
                    Jump();
                    isActivePattern1 = true;
                }
                if (statePattern1 == Pattern1.DROP)
                {
                    HandleDrop();
                }
                if (statePattern1 == Pattern1.JUMPBACK)
                {
                    HandleJumpBack();
                }
                if (finishJump)
                {
                    JumpBack();
                    finishJump = false;
                    statePattern1 = Pattern1.DISABLED;
                }
                if (statePattern1 == Pattern1.DISABLED)
                {
                    if (middleRightPoint.position.x - jumpBackOffset < transform.position.x && middleRightPoint.position.x + jumpBackOffset > transform.position.x)
                    {
                        Debug.Log("Stop from JumpBack");
                        rb.velocity = Vector2.zero;
                        rb.gravityScale = 0;
                        currentBehavior = Behavior.PATTERN2;
                    }
                }
            }

            if (currentBehavior == Behavior.PATTERN2)
            {
                HandlePattern2();
            }
        }

    }

    private void FixedUpdate()
    {
        if (currentBehavior == Behavior.PATTERN3)
        {
            HandlePattern3();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage((int)collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
        }
    }

    #region Pattern2
    private void HandlePattern2()
    {
        if (statePattern2 == Pattern2.GOFORTH)
        {
            rb.velocity = Vector2.left * speed;
            if(stopPointLeft.position.x > transform.position.x)
            {
                rb.velocity = Vector2.zero;
                statePattern2 = Pattern2.GOBACK;
                isGoForth = true;
            }
        }
        if(statePattern2 == Pattern2.GOBACK)
        {
            rb.velocity = Vector2.right * speed;
            if (stopPointRight.position.x < transform.position.x)
            {
                rb.velocity = Vector2.zero;
                isGoBack = true;
            }
        }
        if(isGoBack && isGoForth)
        {
            statePattern2 = Pattern2.DISABLED;
            currentBehavior = Behavior.PATTERN3;
            statePattern3 = Pattern3.MOVE1;
            isGoForth = false;
            isGoBack = false;
            lastPos = transform.position;
            Debug.Log("Finish Pattern 2");
        }
    }
    #endregion

    #region  Pattern 3
    private void HandlePattern3()
    {
        HandleCounter();
        HandleVerticalMovement();
    }

    private void ShootBeam()
    {
        Debug.Log("Shoot beam");
        statePattern3 = Pattern3.BEAM;
        statePattern3 = Pattern3.DISABLED;
        statePattern2 = Pattern2.GOFORTH;
        currentBehavior = Behavior.PATTERN2;
    }

    private void HandleCounter()
    {
        //Save LastPosition
        //Get distance between LastPos and current position
        //last position = current position
        //add travelled distace into counter
        //if distance > max distance -> rb.velo = zero
        //if counter max >= 2 -> Shoot
        float dist = Vector2.Distance(lastPos, transform.position);
        //Debug.Log("Last pos : " + lastPos);
        //Debug.Log("Current pos : " + transform.position);
        totalDistance += dist;
        dist = 0;
        lastPos = transform.position;
        //Debug.Log(Vector2.Distance(lastPos, transform.position) + "   " + totalDistance);
        if (totalDistance > maxDistance)
        {
            isDistanceFull = true;
            //Debug.Log(totalDistance);
            totalDistance = 0;
            maxDistanceCounter++;
            rb.velocity = Vector2.zero;
            Debug.Log("max distance counter increased");
            Debug.Log("Stop");
            isDistanceFull = false;
            isMoving = false;
            if(statePattern3 == Pattern3.MOVE1)
            {
                rb.velocity = Vector2.zero;
                statePattern3 = Pattern3.MOVE2;
                Debug.Log("Behavior: Move2");
            }
            else if (statePattern3 == Pattern3.MOVE2)
            {
                rb.velocity = Vector2.zero;
                statePattern3 = Pattern3.BEAM;
                Debug.Log("Behavior: Move2");
                ShootBeam();
            }
        }
/*        if(statePattern3 == Pattern3.MOVE2)
        {
            if (maxDistanceCounter >= 2)
            {
                ShootBeam();
                maxDistanceCounter = 0;
            }
        }*/
    }

    private void HandleVerticalMovement()
    {
        if(statePattern3 == Pattern3.MOVE1 || statePattern3 == Pattern3.MOVE2)
        {
            if(player.position.y > transform.position.y && !isMoving)
            {
                rb.velocity = Vector2.up * speed;
                isMoving = true;
            }
            if (player.position.y < transform.position.y && !isMoving)
            {
                rb.velocity = Vector2.down * speed;
                isMoving = true;
            }
            if (player.position.y <= transform.position.y + verticalOffset && player.position.y >= transform.position.y - verticalOffset && !isMoving)
            {
                if (!isDistanceFull)
                {
                    isDirUp = Random.Range(0, 2) == 1 ? true : false;
                    if (isDirUp)
                    {
                        rb.velocity = Vector2.up * speed;
                        isMoving = true;
                    }
                    if (!isDirUp)
                    {
                        rb.velocity = Vector2.down * speed;
                        isMoving = true;
                    }
                }
            }

        }
    }
    #endregion
}
