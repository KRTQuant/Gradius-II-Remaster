using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AramamentControl : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float waitTime; //wait time before disappear
    [SerializeField] private float speedMultily;
    [SerializeField] public enum AmmoType { SINGLE, PULSE, BEAM, TAIL, SECOND, MISSILE, SPREAD, TORPEDO, TWOWAYUP, TWOWAYDOWN }
    [SerializeField] public AmmoType currentAmmoType;

    [SerializeField] public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        if(currentAmmoType == AmmoType.PULSE) {
            rb.velocity = new Vector2(bulletSpeed * Time.deltaTime, rb.velocity.y);
        }
        if (currentAmmoType == AmmoType.BEAM)
        {
            rb.velocity = new Vector2(bulletSpeed * Time.deltaTime, rb.velocity.y);
            transform.position = new Vector3(transform.position.x, player.transform.position.y);
        }
        if (currentAmmoType == AmmoType.TAIL)
        {
            //Debug.Log("Tail: " + rb.velocity.x);
            rb.velocity = new Vector2(-bulletSpeed * Time.deltaTime, rb.velocity.y);
        }
        if (currentAmmoType == AmmoType.SECOND)
        {
            rb.velocity = VectorFromAngle(45) * bulletSpeed * Time.deltaTime;
        }
        if (currentAmmoType == AmmoType.MISSILE)
        {
            rb.velocity = VectorFromAngle(315) * bulletSpeed * Time.deltaTime;
        }
        if (currentAmmoType == AmmoType.SPREAD)
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(bulletSpeed * Time.deltaTime, rb.velocity.y);
        }
        if (currentAmmoType == AmmoType.TORPEDO)
        {
            rb.velocity = new Vector2(rb.velocity.x, bulletSpeed* Time.deltaTime);
        }
        if (currentAmmoType == AmmoType.TWOWAYUP)
        {
            rb.velocity = VectorFromAngle(45)* bulletSpeed * Time.deltaTime;
        }
        if (currentAmmoType == AmmoType.TWOWAYDOWN)
        {
            rb.velocity = VectorFromAngle(-45)* bulletSpeed * Time.deltaTime;
        }
        if(currentAmmoType == AmmoType.SINGLE)
        {
            rb.velocity = new Vector2(bulletSpeed* Time.deltaTime, rb.velocity.y);
        }
    }

    private void OnEnable()
    {
        if (currentAmmoType == AmmoType.PULSE)
        {
            StartCoroutine(ScaleUp());
        }
    }

    #region PULSE BULLET
    IEnumerator ScaleUp()
    {
        yield return new WaitForSeconds(0.1f);
        if (col.size.x < 7.5)
        {
            Debug.Log("Scaling X up");
            col.size = new Vector2(col.size.x + 1f, col.size.y);
        }
        if (col.size.y < 18.5)
        {
            Debug.Log("Scaling X up");
            col.size = new Vector2(col.size.x, col.size.y + 3.5f);
        }
        if (col.size.x < 7.5 || col.size.y < 18.5)
        {
            StartCoroutine(ScaleUp());
        }
    }
    #endregion

    Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)); // Trig is fun
    }
}
