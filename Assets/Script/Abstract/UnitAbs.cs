using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbs : MonoBehaviour
{
    [SerializeField] public int score;
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public virtual void TriggerOnDeath()
    {
        if (health < 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
