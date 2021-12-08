using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dee_Enemy : UnitAbs
{
    [SerializeField] private GameObject player;
    [SerializeField] private float gunCooldown = 1.5f;
    [SerializeField] private float fireRate;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private bool isReverse;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> spriteList;

    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        SetHealth();
    }

    private void Update()
    {
        CheckDeath();
    }

    private void FixedUpdate()
    {
        if(isStart)
            Solution2();
    }

    private void Solution2()
    {
        Vector2 dir = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dir, Color.green);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg ;
        Debug.Log((int)angle);

        if(isReverse)
        {
            if (angle < -144 && angle >= -180)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[0];
                Shoot();
            }
            if (angle < -108 && angle >= -144)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 45);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[1];
                Shoot();

            }
            if (angle < -72 && angle >= -108)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 90);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[2];
                Shoot();

            }
            if (angle < -36 && angle >= -72)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 135);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[3];
                Shoot();

            }
            if (angle < -0 && angle >= -36)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 180);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[4];
                Shoot();
            }
        }

        if(!isReverse)
        {
            if (angle > 144 && angle <= 180)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, 0);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[0];
                Shoot();
            }
            if (angle > 108 && angle <= 144)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, -45);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[1];
                Shoot();
            }
            if (angle > 72 && angle <= 108)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, -90);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[2];
                Shoot();
            }
            if (angle > 36 && angle <= 72)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, -135);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[3];
                Shoot();
            }
            if (angle > 0 && angle <= 36)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, -180);
                transform.rotation = rotation;
                spriteRenderer.sprite = spriteList[4];
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (isStart)
        {
            if (fireRate <= 0)
            {
                GameObject bullet = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = -transform.right * speed;
                fireRate = gunCooldown;
            }
            else
            {
                fireRate -= Time.deltaTime;
            }
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

    public override void TriggerOnDeath()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.IncreaseScore(score);
    }
}
