using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheonixFireball : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Vector3 dirToPlayer;

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("VicViper");
        }
    }

    private void Homing()
    {
        dirToPlayer = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void FixedUpdate()
    {
        Homing();
    }
}
