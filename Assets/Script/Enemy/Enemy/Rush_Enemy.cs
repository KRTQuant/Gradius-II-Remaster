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

    private void Awake()
    {
        SetHealth();
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        spriteSize = sr.bounds.extents;
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckDeath();
        MoveInY();
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

    private void MoveInY()
    {
        if (player.transform.position.y > transform.position.y + (spriteSize.y / 4))
        {
            rb.velocity = Vector2.up * speed;
        }

        if (player.transform.position.y < transform.position.y + (spriteSize.y / 4))
        {
            rb.velocity = Vector2.down * speed;
        }

        if (player.transform.position.y <= transform.position.y + (spriteSize.y / 4) && player.transform.position.y > transform.position.y - (spriteSize.y/4))
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
}
