using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbs : MonoBehaviour
{
    [SerializeField] public int score;
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;

    public void SetHealth()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public virtual void TriggerOnDeath()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void OnBecameVisible()
    {
        Debug.Log(name + " Activated");
        gameObject.SetActive(true);
    }
}
