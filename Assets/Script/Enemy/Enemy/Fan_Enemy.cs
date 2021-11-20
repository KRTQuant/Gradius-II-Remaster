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

    [SerializeField] private enum Direction { ABOVE, BELOW }
    [SerializeField] private Direction dir;

    private void Start()
    {
        SetReference();
        SetHealth();
        SetDestination();
    }

    private void Update()
    {
        if(isStart)
        {
            RotateSprite();
            CheckDeath();
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage((int)collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
            player = GameObject.Find("Player");
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

    private void SetReference()
    {
        player = GameObject.Find("Player");
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnBecameVisible()
    {
        Debug.Log(name + " isStart = true");
        isStart = true;
    }

    private void SetDestination()
    {
        GameObject tmp;
        switch (dir)
        {
            case Direction.ABOVE:
                tmp = GameObject.Find("TopDestination");
                for (int i = 0; i < tmp.transform.childCount; i++)
                {
                    this.destination.Add(tmp.transform.GetChild(i));
                }
                break;

            case Direction.BELOW:
                tmp = GameObject.Find("DownDestination");
                for (int i = 0; i < tmp.transform.childCount; i++)
                {
                    this.destination.Add(tmp.transform.GetChild(i));
                }
                break;
        }

    }


}
