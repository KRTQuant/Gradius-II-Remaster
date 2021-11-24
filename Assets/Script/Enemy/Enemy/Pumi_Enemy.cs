using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumi_Enemy : UnitAbs
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject player;

    [Header("Sine Movement")]
    [SerializeField] private float frequency = 5f;
    [SerializeField] private float magnitude = 5f;
    [SerializeField] private float offset;

    [Header("Waypoint")]
    [SerializeField] private Transform waypointA;
    [SerializeField] private Transform waypointB;
    [SerializeField] private float delay;
    [SerializeField] private bool isMovetoB;
    [SerializeField] private float speed;
    [SerializeField] private Transform current;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float gunCooldown;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float fireRate;


    // Update is called once per frame
    void Update()
    {
        DrawRay();
    }

    private void FixedUpdate()
    {
        if(isStart)
        {
            HorizontalMove();
            Shoot();
        }
    }

    void HorizontalMove()
    {
        MoveWithWayPoint();
        //MoveWithSineWave();
    }

    void DrawRay()
    {
        Vector2 dir = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dir, Color.green);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log((int)angle);
    }

    void MoveWithSineWave()
    {
        rb.velocity = Vector2.right * Mathf.Sin(Time.time * frequency + offset) * magnitude;
    }
    void MoveWithWayPoint()
    {
        if (isMovetoB)
        {
            rb.velocity = Vector2.right * speed * Time.deltaTime;
            Debug.Log("MoveToB");
        }
        if (!isMovetoB)
        {
            rb.velocity = Vector2.left * speed * Time.deltaTime;
            Debug.Log("MoveToA");
        }
        if (transform.position.x >= waypointB.position.x && isMovetoB)
        {
            Debug.Log("Arrived B");
            current = waypointA;
            isMovetoB = false;
        }
        if (transform.position.x <= waypointA.position.x && !isMovetoB)
        {
            Debug.Log("Arrived A");
            current = waypointB;
            isMovetoB = true;
        }

    }

    void Shoot()
    {
        if (fireRate <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Vector2 dir = (player.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = dir * speed * Time.deltaTime;
            fireRate = gunCooldown;
        }
        else
        {
            fireRate -= Time.deltaTime;
        }
    }
}
