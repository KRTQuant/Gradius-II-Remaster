using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrimp_Enemy : UnitAbs
{
    [SerializeField] private Transform currentDestination;
    [SerializeField] private List<Transform> destinationList = new List<Transform>();
    //[SerializeField] private bool isMoveRight;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        SetHealth();
    }

    private void Update()
    {
        if(isStart)
        {
            Solution1();
            CheckDeath();
        }
    }

    private void Solution1()
    {
        if(isStart)
        {
            if (currentDestination == destinationList[1])
            {
                if (transform.position.x >= destinationList[1].position.x)
                {
                    Debug.Log("Change to RT");
                    currentDestination = destinationList[2];
                }
                else
                {
                    rb.velocity = Vector2.right * speed * Time.deltaTime;
                }
            }
            else if (currentDestination == destinationList[2])
            {
                if (transform.position.y <= destinationList[2].position.y)
                {
                    Debug.Log("Change to RB");
                    currentDestination = destinationList[3];
                }
                else
                {
                    rb.velocity = Vector2.down * speed * Time.deltaTime;
                }
            }
            else if (currentDestination == destinationList[3])
            {
                if (transform.position.x <= destinationList[3].position.x)
                {
                    Debug.Log("Change to LB");
                    currentDestination = destinationList[0];
                }
                else
                {
                    rb.velocity = Vector2.left * speed * Time.deltaTime;
                }
            }
            else if (currentDestination == destinationList[0])
            {
                if (transform.position.y >= destinationList[0].position.y)
                {
                    currentDestination = destinationList[1];
                }
                else
                {
                    rb.velocity = Vector2.up * speed * Time.deltaTime;
                }
            }
        }
        //Debug.Log(rb.velocity);
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
