using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PheonixMinion_Moveset
{ 
    POSITION,
    WAVE
}

public class Phoenix_Enemy : UnitAbs
{
    [Header("Pheonix")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private PheonixMinion_Moveset moveset;
    [SerializeField] private HordeFollowManager hordeFollow;
    [SerializeField] private bool isTriggerOnDeathActive = false;

    [Header("In Order Movement")]
    [SerializeField] private float speed;
    [SerializeField] private Vector2 dir;
    [SerializeField] private float delayTime;
    [SerializeField] private Transform stopPoint;
    [SerializeField] private bool isFlyDown;
    [SerializeField] private bool isFinish;

    [Header("Wave Movement")]
    [SerializeField] private float magnitude;
    [SerializeField] private float frequency;


    public override void Start()
    {
        base.Start();
        hordeFollow = GetComponent<HordeFollowManager>();
        player = GameObject.Find("Player").transform;
        SetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        CheckDisable();
        if (!isFinish && isStart)
        {
            if(moveset == PheonixMinion_Moveset.POSITION)
            {
                FlyinVertical();
            }
            if(moveset == PheonixMinion_Moveset.WAVE)
            {
                WaveMovement();
            }
        }
    }

    private void FlyinHorizontal()
    {
        if (player.position.x > transform.position.x)
        {
            dir = Vector2.right;
        }

        if (player.position.x < transform.position.x)
        {
            dir = Vector2.left;
        }

        rb.velocity = dir * speed * Time.deltaTime;
        isFinish = true;
    }

    private void FlyinVertical()
    {
        if (player.position.y > transform.position.y)
        {
            dir = Vector2.up;
            isFlyDown = false;
        }

        else if (player.position.y < transform.position.y)
        {
            dir = Vector2.down;
            isFlyDown = true;
        }

        CheckConditionY();
    }

    private void CheckConditionY()
    {
        if (isFlyDown)
        {
            rb.velocity = dir * speed * Time.deltaTime;

            if (transform.position.y < stopPoint.position.y)
            {
                StartCoroutine(DelayBeforeLaunch());
            }
        }
        else
        {
            rb.velocity = dir * speed * Time.deltaTime;

            if (transform.position.y > stopPoint.position.y)
            {
                StartCoroutine(DelayBeforeLaunch());
            }
        }
    }

    IEnumerator DelayBeforeLaunch()
    {
        rb.velocity = Vector2.zero * 0;
        yield return new WaitForSeconds(delayTime);
        FlyinHorizontal();
        StopCoroutine(DelayBeforeLaunch());
    }

    private void WaveMovement()
    {
        transform.position -= transform.right * Time.deltaTime * speed * Time.deltaTime;
        transform.position = transform.position + (transform.up * Mathf.Sin(Time.time * frequency) * magnitude / 100);
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
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.IncreaseScore(score);
        Debug.Log("TriggerOnDeath was call");
    }

    private void CheckDisable()
    {
        if(IsAllUnitDeactive() && isStart)
        {
            this.gameObject.SetActive(false);
        }
    }

    private bool IsAllUnitDeactive()
    {
        if (hordeFollow.fleetBody.Count == 1)
        {
            return true;
        }
        if (hordeFollow.fleetBody.Count != 0)
        {
            foreach (var unit in hordeFollow.fleetBody)
            {
                if (unit.activeSelf)
                {
                    return false;
                }
            }
        }
        return true;
    }
    public override void OnBecameInvisible()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void CheckDeath()
    {
        if (health <= 0 && !isTriggerOnDeathActive)
        {
            TriggerOnDeath();
            isTriggerOnDeathActive = true;
        }
    }


}
