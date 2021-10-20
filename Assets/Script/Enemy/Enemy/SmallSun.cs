using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSun : MonoBehaviour
{
    [SerializeField] private float delayTime;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayTime);
        TargetPlayer();

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
        rb.velocity = dir * speed * Time.deltaTime;
    }
}
