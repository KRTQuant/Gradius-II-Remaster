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

    [SerializeField] private Rigidbody2D rb;

    public override void Start()
    {
        base.Start();
        SetHealth();
    }

    private void Update()
    {
        CheckDeath();
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

}
