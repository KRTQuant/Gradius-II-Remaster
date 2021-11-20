using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garun_Enemy : UnitAbs
{
    [Header("Garun")]
    [SerializeField] private bool isSpawnCapsule;
    [SerializeField] private float frequency;
    [SerializeField] private float magnitude;
    [SerializeField] private float speed;

    [Header("Reference")]
    [SerializeField] private GameObject capsule;

    private void Start()
    {
        SetHealth();
    }

    private void Update()
    {
        if (isStart)
        {
            CheckDeath();
            HandleMove();
        }
    }

    private void HandleMove()
    {
        transform.position -= transform.right * Time.deltaTime * speed;
        transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude/100;
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
