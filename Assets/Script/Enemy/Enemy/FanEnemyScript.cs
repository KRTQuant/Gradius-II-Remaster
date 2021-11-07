using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanEnemyScript : UnitAbs
{
    [Header("Reference")]
    [SerializeField] private GameObject capsule;
    [SerializeField] private GameObject player;

    [Header("Animation")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float rotateSpeed;
    //[SerializeField] private FleetControl fleetControl;
    [SerializeField] private Rigidbody2D rb;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveAngle;
    [SerializeField] public List<Transform> destination;
    [SerializeField] private float passedDestination;
    [SerializeField] private enum enemyStatus { MOVE, ARRIVED, STOP, DEAD }
    [SerializeField] private enemyStatus currentStatus;

    private void Start()
    {
        SetHealth();
    }

    void Update()
    {
        ControlAnim();
        Move();
        HandleActive();
    }

    private void ControlAnim()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.transform.Rotate(transform.forward * rotateSpeed * Time.deltaTime);
    }

    public void Move()
    {
        if(currentStatus == enemyStatus.MOVE)
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

        switch(currentStatus)
        {
            case enemyStatus.MOVE:
                transform.position = Vector3.MoveTowards(transform.position, destination[(int)passedDestination].position, moveSpeed * Time.deltaTime);
                if (transform.position == destination[(int)passedDestination].position)
                    currentStatus = enemyStatus.ARRIVED;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            TakeDamage((int)collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
            player = GameObject.Find("Player");
            player.GetComponent<PlayerCombat>().IncreaseScore(score);
        }
    }

    private void SetHealth()
    {
        health = maxHealth;
    }

    private void HandleActive()
    {
        if (sprite.isVisible)
            this.gameObject.SetActive(true);
    }

    public override void TriggerOnDeath()
    {
        if (health < 0)
        {

            this.gameObject.SetActive(false);
        }
    }
}
