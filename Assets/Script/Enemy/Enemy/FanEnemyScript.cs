using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanEnemyScript : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject capsule;

    [Header("Health")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

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

            //case enemyStatus.ARRIVED:

        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            TakeDamage(collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            Debug.Log("Spawn power up capsule");
            this.gameObject.SetActive(false);
            Instantiate(capsule, transform.position, transform.rotation);
            Debug.Log("Spawn power up capsule");
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
}
