using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beans_Enemy : UnitAbs
{
    [SerializeField] private float frequency = 5f;
    [SerializeField] private float magnitude;
    [SerializeField] private float speed;
    [SerializeField] private float divisor;
    [SerializeField] private float waitTime;
    [SerializeField] private HordeFollowManager hordeFollow;
    [SerializeField] private bool isTriggerOnDeathActive;

    [SerializeField] private Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        SetHealth();
        hordeFollow = GetComponent<HordeFollowManager>();
    }

    private void Update()
    {
        CheckDeath();
        CheckDisable();
    }

    private void FixedUpdate()
    {
        if (isStart)
        {
            transform.position -= transform.right * Time.deltaTime * speed;
            if (divisor == 0)
                transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
            else
                transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude / divisor;
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
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.IncreaseScore(score);
        Debug.Log("TriggerOnDeath was call");
    }

    public override void OnBecameInvisible()
    {
        GetComponent<SpriteRenderer>().enabled = true;

    }

    private void CheckDisable()
    {
        if (IsAllUnitDeactive() && isStart)
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

    public override void CheckDeath()
    {
        if (health <= 0 && !isTriggerOnDeathActive)
        {
            TriggerOnDeath();
            isTriggerOnDeathActive = true;
        }
    }

}
