using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEyeBoss : MonoBehaviour
{
    [SerializeField] private enum Behavior { OPEN , CLOSE }
    [SerializeField] private Behavior currentBehavior;
    [SerializeField] private float timer;
    [SerializeField] private float closeEyeTime;
    [SerializeField] private float openEyeTime;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private bool isChange;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject armBullet;
    [SerializeField] private float armBulletSpeed;
    [SerializeField] private Transform armTopPos;
    [SerializeField] private Transform armBottomPos;
    [SerializeField] private float armTimer;
    [SerializeField] private float gunCooldown;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimer();
        if (currentBehavior == Behavior.OPEN)
            HandleOpenEye();
        if (currentBehavior == Behavior.CLOSE)
            HandleCloseEye();
        HandleArmBullet();

    }

    private void HandleOpenEye()
    {
        box.isTrigger = false;
        sprite.color = Color.red;
    }

    private void HandleCloseEye()
    {
        box.isTrigger = false;
        sprite.color = Color.black;
    }

    private void HandleTimer()
    {
        if(timer > 0)
        {
            isChange = false;
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            if(currentBehavior == Behavior.CLOSE && !isChange)
            {
                timer = openEyeTime;
                currentBehavior = Behavior.OPEN;
                isChange = true;
            }
            if (currentBehavior == Behavior.OPEN && !isChange)
            {
                timer = closeEyeTime;
                currentBehavior = Behavior.CLOSE;
                GameObject bullet = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * Vector2.left;
                isChange = true;
            }
        }
    }

    private void HandleArmBullet()
    {
        if(armTimer > 0)
        {
            armTimer -= Time.deltaTime;
        }
        if(armTimer <= 0)
        {
            GameObject bulletTop = Instantiate<GameObject>(armBullet, armTopPos.position, Quaternion.identity);
            GameObject bulletBottom = Instantiate<GameObject>(armBullet, armBottomPos.position, Quaternion.identity);
            bulletTop.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed ;
            bulletBottom.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed ;
            armTimer = gunCooldown;
        }
    }
}
