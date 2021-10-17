using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private enum disposeObject { SETFALSE, DESTROY }
    [SerializeField] private disposeObject currentDisposeType;

    private void SetHealth()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public virtual void HandleDeath()
    {
        if(currentDisposeType == disposeObject.DESTROY)
        {
            Destroy(this.gameObject);
        }

        if(currentDisposeType == disposeObject.SETFALSE)
        {
            this.gameObject.SetActive(false);
        }
    }
}
