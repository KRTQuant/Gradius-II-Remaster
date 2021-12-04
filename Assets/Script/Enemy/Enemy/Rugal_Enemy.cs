using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rugal_Enemy : UnitAbs
{
    [Header("Rugal")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool isActive = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        SetHealth();
    }

    private void Update()
    {
        SetPlayerRef();
        CheckDeath();

        if (isActive)
        {
            Homing();
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

    public override void OnBecameVisible()
    {
        isActive = true;
    }

    private void SetPlayerRef()
    {
        if (player == null)
        {
            player = GameObject.Find("VicViper");
        }
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
