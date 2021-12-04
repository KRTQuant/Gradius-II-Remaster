using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbs : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] public GameManager gameManager;

    [Header("Score")]
    [SerializeField] public int score;

    [Header("Health")]
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;

    [Header("Active")]
    [SerializeField] public bool isStart;

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
        Debug.Log("TriggerOnDeath was call");
    }

    public virtual void OnBecameVisible()
    {
        Debug.Log(name + " Activated");
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
}
