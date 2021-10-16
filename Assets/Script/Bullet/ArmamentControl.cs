using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmamentControl : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime; //wait time before disappear
    [SerializeField] public enum AmmoType { SINGLE, PULSE, BEAM, TAIL, SECOND, MISSILE, SPREAD, TORPEDO, TWOWAYUP, TWOWAYDOWN }
    [SerializeField] public AmmoType currentAmmoType;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        HandleMovement();
        HandleOutOfRange();
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void HandleMovement()
    {
        if (currentAmmoType == AmmoType.PULSE)
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }
        if (currentAmmoType == AmmoType.TAIL)
        {
            //Debug.Log("Tail: " + rb.velocity.x);
            rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
        }
        if (currentAmmoType == AmmoType.SECOND)
        {
            rb.velocity = VectorFromAngle(45) * speed * Time.deltaTime;
        }
        if (currentAmmoType == AmmoType.MISSILE)
        {
            rb.velocity = VectorFromAngle(315) * speed * Time.deltaTime;
        }
        if (currentAmmoType == AmmoType.SPREAD)
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }
        if (currentAmmoType == AmmoType.TORPEDO)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed * Time.deltaTime);
            Debug.Log(rb.velocity);
        }
        if (currentAmmoType == AmmoType.TWOWAYUP)
        {
            rb.velocity = VectorFromAngle(45) * speed * Time.deltaTime;
        }
        if (currentAmmoType == AmmoType.TWOWAYDOWN)
        {
            rb.velocity = VectorFromAngle(-45) * speed * Time.deltaTime;
        }
        if (currentAmmoType == AmmoType.SINGLE)
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }
    }

    private void HandleOutOfRange()
    {
/*        if (!sprite.isVisible)
            this.gameObject.SetActive(false);*/
    }

    private void OnEnable()
    {
        if(currentAmmoType == AmmoType.SINGLE)
        {
            StartCoroutine(Deactive());
        }
        if (currentAmmoType == AmmoType.PULSE)
        {
            StartCoroutine(Deactive());
            StartCoroutine(ScaleUp());
        }
        if (currentAmmoType == AmmoType.BEAM)
        {
            StartCoroutine(Deactive());
        }
        if (currentAmmoType == AmmoType.TAIL)
        {
            StartCoroutine(Deactive());
        }
        if (currentAmmoType == AmmoType.SECOND)
        {
            StartCoroutine(Deactive());
        }
        if (currentAmmoType == AmmoType.MISSILE)
        {
            StartCoroutine(Deactive());
        }
        if (currentAmmoType == AmmoType.SPREAD)
        {
            StartCoroutine(Deactive());
        }
        if (currentAmmoType == AmmoType.TORPEDO)
        {
            StartCoroutine(Deactive());
        }
        if (currentAmmoType == AmmoType.TWOWAYDOWN)
        {
            StartCoroutine(Deactive());
        }
        if (currentAmmoType == AmmoType.TWOWAYUP)
        {
            StartCoroutine(Deactive());
        }
    }

    #region PULSE BULLET
    IEnumerator ScaleUp()
    {
        yield return new WaitForSeconds(0.1f);
        if (col.size.x < 0.4)
        {
            Debug.Log("Scaling X up");
            col.size = new Vector2(col.size.x + 0.1f, col.size.y);
        }
        if (col.size.y < 1.2)
        {
            Debug.Log("Scaling X up");
            col.size = new Vector2(col.size.x, col.size.y + 0.28f);
        }
        if (col.size.x < 0.4 || col.size.y < 1.2)
        {
            StartCoroutine(ScaleUp());
        }
    }
    #endregion

    Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)); // Trig is fun
    }

    IEnumerator Deactive()
    {
        yield return new WaitForSeconds(waitTime);
/*        this.gameObject.SetActive(false);*/
    }
}
