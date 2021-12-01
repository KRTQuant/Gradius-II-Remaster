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
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 dir;
    [SerializeField] private float delayTime;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform stopPoint;
    [SerializeField] private bool isFlyDown;
    [SerializeField] private bool isFinish;

    [SerializeField] private PheonixMinion_Moveset moveset;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinish)
        {
            if(moveset == PheonixMinion_Moveset.POSITION)
            {
                FlyinVertical();
            }
            else
            {

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
}
