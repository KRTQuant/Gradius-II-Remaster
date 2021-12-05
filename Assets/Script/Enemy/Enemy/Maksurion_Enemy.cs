using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MaksurionBehavior
{
    JUMPING,
    CRAWLING
}

public class Maksurion_Enemy : UnitAbs
{
    [Header("Maksurion")]
    [Header("Reference")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float timer;
    [SerializeField] private float crawlingTime;
    [SerializeField] private float rotationDegree = 45;
    [SerializeField] private float extraWidth;

    [Header("MaksurionBehavior")]
    [SerializeField] private MaksurionBehavior currentBehavior;

    private void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        SetHealth();
        Jump();
    }

    private void Update()
    {
        CheckDeath();
        if (currentBehavior == MaksurionBehavior.CRAWLING)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                Jump();
                timer = crawlingTime;
            }

            CheckObstacle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            Crawl();
            Debug.Log("I hope you will log only one time");
        }

        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage((int)collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
        }
    }

    private void Jump()
    {
        currentBehavior = MaksurionBehavior.JUMPING;
        if (player.transform.position.y > transform.position.y)
        {
            if (player.transform.position.x > transform.position.x)
            {
                Quaternion rotation = Quaternion.AngleAxis(rotationDegree, Vector3.forward);
                rb.velocity = rotation * Vector2.right * speed;
                Debug.Log("To UpRight");
            }
            if (player.transform.position.x < transform.position.x)
            {
                Quaternion rotation = Quaternion.AngleAxis(rotationDegree, Vector3.forward);
                rb.velocity = rotation * Vector2.up * speed;
                Debug.Log("To UpLeft");
            }
        }
        if (player.transform.position.y < transform.position.y)
        {
            if (player.transform.position.x > transform.position.x)
            {
                Quaternion rotation = Quaternion.AngleAxis(rotationDegree, Vector3.forward);
                rb.velocity = rotation * Vector2.down * speed;
                Debug.Log("To DownRight");
            }
            if (player.transform.position.x < transform.position.x)
            {
                Quaternion rotation = Quaternion.AngleAxis(rotationDegree, Vector3.forward);
                rb.velocity = rotation * Vector2.left * speed;
                Debug.Log("To DownLeft");
            }
        }
    }

    private void Crawl()
    {
        currentBehavior = MaksurionBehavior.CRAWLING;
        if (timer > 0)
        {
            rb.velocity = Vector2.right * speed;

        }
        if (timer <= 0)
        {
            Jump();
            timer = crawlingTime;
        }

    }

    private void CheckObstacle()
    {
        RaycastHit2D ray = Physics2D.Raycast(sr.bounds.center, Vector2.right, sr.bounds.extents.x + extraWidth);
        Debug.DrawRay(transform.position, Vector2.right * (sr.bounds.extents.x + extraWidth), Color.green);
        if (ray.collider != null)
        {
            if (ray.collider.name != gameObject.name)
            {
                Debug.Log("JUST JUMP IDIOT");
                Jump();
            }
        }
        else
        {
            Debug.Log("I collide with nothing");
        }
    }
}
