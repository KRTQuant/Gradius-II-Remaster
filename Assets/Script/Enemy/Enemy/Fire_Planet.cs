using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Planet : UnitAbs
{
    [Header("Fire Planet")]
    [SerializeField] private float delayTime;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    // Start is called before the first frame update

    public override void Start()
    {
        base.Start();
        SetHealth();
    }

    private void Update()
    {
        CheckDeath();

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        TargetPlayer();
        SetHealth(1);
    }

    private void TargetPlayer()
    {
        GameObject target = GameObject.Find("Player");
        Vector2 dir = target.transform.position - transform.position;
        dir.Normalize();
        MoveTowardPlayer(dir);
    }

    private void MoveTowardPlayer(Vector2 dir)
    {
        rb.velocity = speed * dir;
    }

    public override void OnBecameVisible()
    {
        base.OnBecameVisible();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Delay());
    }

    public override void OnEnable()
    {
        base.Start();
        isStart = false;
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
