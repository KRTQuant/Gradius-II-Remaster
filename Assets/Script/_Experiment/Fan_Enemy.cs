using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan_Enemy : UnitAbs
{
    [Header("Reference")]
    [SerializeField] private GameObject player;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rb;

    [Header("Rotate")]
    [SerializeField] private float rotateSpeed;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveAngle;
    [SerializeField] public List<Transform> destination;
    [SerializeField] private float passedDestination;
    [SerializeField] private enum enemyStatus { MOVE, ARRIVED, STOP, DEAD }
    [SerializeField] private enemyStatus currentStatus;

    private void Start()
    {
        SetReference();
        SetHealth();
    }

    private void Update()
    {
        RotateSprite();
        CheckDeath();
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage((int)collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
            player = GameObject.Find("Player");
            player.GetComponent<PlayerCombat>().IncreaseScore(score);
        }
    }

    private void RotateSprite()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.transform.Rotate(transform.forward * rotateSpeed * Time.deltaTime);
    }

    public void Move()
    {
        if (currentStatus == enemyStatus.MOVE)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination[(int)passedDestination].position, moveSpeed * Time.deltaTime);
            if (transform.position == destination[(int)passedDestination].position)
                currentStatus = enemyStatus.ARRIVED;
        }
        if (currentStatus == enemyStatus.ARRIVED)
        {
            if (passedDestination < destination.Count - 1)
            {
                passedDestination++;
                currentStatus = enemyStatus.MOVE;
                //Debug.Log("Passed Destination : " + passedDestination);
            }
            else
                currentStatus = enemyStatus.STOP;
        }
        if (currentStatus == enemyStatus.STOP)
        {
            return;
        }

        switch (currentStatus)
        {
            case enemyStatus.MOVE:
                transform.position = Vector3.MoveTowards(transform.position, destination[(int)passedDestination].position, moveSpeed * Time.deltaTime);
                if (transform.position == destination[(int)passedDestination].position)
                    currentStatus = enemyStatus.ARRIVED;
                break;
        }
    }

    private void CheckDeath()
    {
        if(health < 0)
        {
            TriggerOnDeath();
        }
    }

    private void SetReference()
    {
        player = GameObject.Find("Player");
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
}
