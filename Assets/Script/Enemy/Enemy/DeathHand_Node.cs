using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class DeathHand_Node : MonoBehaviour
{
    [SerializeField] private DeathHand_Enemy deathHand_Enemy;
    [SerializeField] public bool isActiveCheat = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            deathHand_Enemy.HandleBulletCollide();
        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Keypad0) && isActiveCheat)
        //{
        //    deathHand_Enemy.HandleBulletCollide();
        //}
    }
}
