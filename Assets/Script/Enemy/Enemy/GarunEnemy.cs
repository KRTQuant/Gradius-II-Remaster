using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarunEnemy : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private GameObject capsule;
    [SerializeField] private bool isSpawnCapsule;

    [Header("Health")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private float frequency;
    [SerializeField] private float magnitude;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GarunEnemy script;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        script = GetComponent<GarunEnemy>();

        SetHealth();
    }

    private void Update()
    {
        HandleMove();
        //HandleActive();
        //Debug.Log("Test");
        Debug.Log(sprite.isVisible);
    }

    private void HandleMove()
    {
        transform.position -= transform.right * Time.deltaTime * speed;
        transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude/100;
    }

/*    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void OnBecameVisible()
    {
        gameObject.SetActive(true);
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage(collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            Debug.Log("Spawn power up capsule");
            if(isSpawnCapsule)
                Instantiate(capsule, transform.position, transform.rotation);
            this.gameObject.SetActive(false);
        }
    }

    private void SetHealth()
    {
        health = maxHealth;
    }

    private void HandleActive()
    {
        if (!sprite.isVisible)
            gameObject.SetActive(false);

        if (sprite.isVisible)
            gameObject.SetActive(true);
    }

    private void OnBecameVisible()
    {
        Debug.Log("Display me");
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Hide me");
    }


}
