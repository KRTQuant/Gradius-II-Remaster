using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Small_Sun : UnitAbs
{
    [Header("Fire Planet")]
    [SerializeField] private float delayTime;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    // Start is called before the first frame update

    private void Start()
    {
        SetHealth();
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
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Delay());
    }
}
