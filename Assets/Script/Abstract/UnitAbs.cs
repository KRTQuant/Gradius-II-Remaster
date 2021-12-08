using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbs : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] public GameManager gameManager;
    [SerializeField] public Vector3 initPos;

    [Header("Score")]
    [SerializeField] public int score;

    [Header("Health")]
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;

    [Header("Active")]
    [SerializeField] public bool isStart;

    [Header("Deactive")]
    [SerializeField] public float deactiveDelay;

    public virtual void Start()
    {
        initPos = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = initPos;
    }

    public virtual void OnEnable()
    {
        isStart = false;
    }

    public void SetHealth()
    {
        health = maxHealth;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public virtual void TriggerOnDeath()
    {
        gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.IncreaseScore(score);
        //Debug.Log("TriggerOnDeath was call");
    }

    public virtual void OnBecameVisible()
    {
        //Debug.Log(name + " Activated");
        isStart = true;
    }


    public virtual void CheckDeath()
    {
        //Debug.Log("Checking");
        if (health <= 0)
        {
            TriggerOnDeath();
        }
    }

    public virtual void OnBecameInvisible()
    {
        if (isStart)
        {
            this.gameObject.SetActive(false);
        }
    }
}
