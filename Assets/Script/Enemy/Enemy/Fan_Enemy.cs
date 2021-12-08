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

    public override void OnEnable()
    {
        base.OnEnable();
        SetDestination();
        currentStatus = enemyStatus.MOVE;
    }

    public void OnDisable()
    {
        currentStatus = enemyStatus.ARRIVED;
        destination.Clear();
        passedDestination = 0;
    }

    private void Awake()
    {
        SetHealth();
    }

    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if(isStart)
        {
            Debug.Log("Move");
            RotateSprite();
            CheckDeath();
            Move();
            if (currentStatus == enemyStatus.STOP && isStart)
            {
                this.gameObject.SetActive(false);
            }
        }

        if(destination.Count == 0)
            SetDestination();
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

    public override void OnBecameVisible()
    {
        //Debug.Log(name + " isStart = true");
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

    public override void OnBecameInvisible()
    {
        if (currentStatus == enemyStatus.STOP && isStart)
        {
            this.gameObject.SetActive(false);
        }
    }


}
