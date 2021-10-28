using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{ 
    PLAYER,
    ENEMY
}


public abstract class UnitAbs : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;

    private void SetHealth()
    {
        currentHealth = maxHealth;
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collide with Something");
    }

    public virtual void OnBecameVisible()
    {
        Debug.Log("Display me");
    }

    public virtual void OnBecameInvisible()
    {
        Debug.Log("Hide me");
    }


}
