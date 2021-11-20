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

    private void Start()
    {
        spriteSize = sr.bounds.extents;
        player = GameObject.Find("VicViper");
    }

    private void Update()
    {
        MoveInY();
        if (finishXaxisMove && !isFinishSetVelo)
        {
            MoveInX();
        }

        if (isFinishSetVelo)
        {
            if (isMoveToRight)
                rb.velocity = Vector2.right * speed * Time.deltaTime;
            if (!isMoveToRight)
                rb.velocity = Vector2.left * speed * Time.deltaTime;
        }
    }

    private void MoveInY()
    {
        if (player.transform.position.y > transform.position.y + spriteSize.y)
        {
            rb.velocity = Vector2.up * speed * Time.deltaTime;
        }

        if (player.transform.position.y < transform.position.y + spriteSize.y)
        {
            rb.velocity = Vector2.down * speed * Time.deltaTime;
        }

        if (player.transform.position.y <= transform.position.y + spriteSize.y && player.transform.position.y > transform.position.y - spriteSize.y)
        {
            finishXaxisMove = true;
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
}
