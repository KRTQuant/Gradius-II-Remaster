using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush_Enemy : UnitAbs
{
    [Header("Reference")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Vector2 spriteSize;

    [Header("Rush")]
    [SerializeField] private float speed;
    [SerializeField] private bool finishXaxisMove = false;
    [SerializeField] private bool isMoveToRight;
    [SerializeField] private bool isFinishSetVelo;
    [SerializeField] private bool isGetDir = false;

    private void Awake()
    {
        SetHealth();
    }

    public override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        spriteSize = sr.bounds.extents;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckDeath();
        MoveInY();
        if(isStart)
        {
            if (finishXaxisMove && !isFinishSetVelo)
            {
                MoveInX();
            }

            if (isFinishSetVelo)
            {
                if (isMoveToRight)
                    rb.velocity = Vector2.right * speed;
                if (!isMoveToRight)
                    rb.velocity = Vector2.left * speed;
            }
        }

    }

    private void MoveInY()
    {
        if (player.transform.position.y > transform.position.y - (spriteSize.y / 2) && !isGetDir)
        {
            rb.velocity = Vector2.up * speed;
            isGetDir = true;
        }

        else if (player.transform.position.y < transform.position.y + (spriteSize.y / 2) && !isGetDir)
        {
            rb.velocity = Vector2.down * speed;
            isGetDir = true;
        }

        if (player.transform.position.y <= transform.position.y + (spriteSize.y / 2) && player.transform.position.y > transform.position.y - (spriteSize.y/2))
        {
            finishXaxisMove = true;
            //Debug.Log(spriteSize.y / 4);
        }

    }

    private void MoveInX()
    {

        if (player.transform.position.x < transform.position.x)
        {
            isMoveToRight = false;
            isFinishSetVelo = true;

        }

        if (player.transform.position.x > transform.position.x)
        {
            isMoveToRight = true;
            isFinishSetVelo = true;
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

    public override void OnBecameInvisible()
    {
        if (isStart)
        {
            Destroy(this.gameObject);
            Debug.Log("Destroyed");
        }
    }
}
