using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RugalEnemy : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    private void Update()
    {
        if(player == null)
        {
            player = GameObject.Find("VicViper");
        }
    }

    private void Homing()
    {
        Vector2 dir = player.transform.position - this.transform.position;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir, -transform.right).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = -transform.right * speed;
    }

    private void FixedUpdate()
    {
        Homing();
    }
}
