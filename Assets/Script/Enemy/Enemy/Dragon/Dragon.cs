using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;

    public float rotateSpeed;
    public float speed;
    void Start()
    {
        player = GameObject.Find("VicViper").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Solution2();
    }

    public void Solution2() //Homing
    {
        Vector2 dir = (Vector2)player.position - (Vector2)transform.position;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = transform.up * speed;
    }
}
