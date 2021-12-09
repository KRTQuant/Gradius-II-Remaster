using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Node : MonoBehaviour
{
    [SerializeField] private Dragon_Enemy dragon;

    private void Start()
    {
        dragon = GetComponentInParent<Dragon_Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            dragon.TakeDamage((int)collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
        }
    }

    public void OnBecameVisible()
    {
        dragon.isStart = true;
    }
}
