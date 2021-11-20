using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammonite_Enemy : UnitAbs
{
    [SerializeField] private enum State { IDLE, MOVE }
    [SerializeField] private State currentState;
    [SerializeField] private Transform waypointA;
    [SerializeField] private Transform waypointB;
    [SerializeField] private bool isMoveToB;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private float timer;

    // Update is called once per frame
    void Update()
    {
        if (isStart)
            Move();
    }

    void Move()
    {
        if (currentState == State.MOVE && timer <= 0)
        {
            if (isMoveToB)
            {
                if (transform.position.y >= waypointB.position.y)
                {
                    rb.velocity = Vector2.down * speed * Time.deltaTime;
                }
                if (transform.position.y <= waypointB.position.y)
                {
                    rb.velocity = Vector2.zero * 0;
                    Debug.Log("STOP!!");
                    isMoveToB = false;
                    timer = waitTime;
                }
            }
            else
            {
                if (!isMoveToB && transform.position.y <= waypointA.position.y)
                {
                    rb.velocity = Vector2.up * speed * Time.deltaTime;
                }
                if (transform.position.y >= waypointA.position.y)
                {
                    rb.velocity = Vector2.zero * 0;
                    Debug.Log("STOP!!");
                    isMoveToB = true;
                    timer = waitTime;
                }
            }
        }
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
