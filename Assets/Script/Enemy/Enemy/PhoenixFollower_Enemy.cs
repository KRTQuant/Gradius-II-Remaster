using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixFollower_Enemy : UnitAbs
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
        StartCoroutine(DeactivateDestroy());
    }

    IEnumerator DeactivateDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
