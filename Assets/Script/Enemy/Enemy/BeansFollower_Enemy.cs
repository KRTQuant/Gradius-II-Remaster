using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeansFollower_Enemy : UnitAbs
{
    public override void Start()
    {
        base.Start();
        SetHealth();
    }

    private void Update()
    {
        CheckDeath();
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
        Destroy(this.gameObject);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.IncreaseScore(score);
        Debug.Log("TriggerOnDeath was call");
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
