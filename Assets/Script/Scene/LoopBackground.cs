using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;
    public float width;
    public float speed = -3f;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        width = boxCollider.size.x;
        rb.velocity = new Vector2(speed, 0);
    }

    private void Update()
    {
        if(transform.position.x < -width)
        {
            Debug.Log("under condition");
            Debug.Log(-width);
            Reposition();
        }
    }

    private void Reposition()
    {
        Vector2 vector = new Vector2(width * 2f, 0);
        transform.position = (Vector2)transform.position + vector;
    }
}
