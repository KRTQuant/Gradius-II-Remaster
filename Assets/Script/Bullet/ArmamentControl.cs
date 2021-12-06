using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmamentControl : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime; //wait time before disappear
    [SerializeField] public enum AmmoType { SINGLE, PULSE, BEAM, TAIL, SECOND, MISSILE, SPREAD, TORPEDO, TWOWAYUP, TWOWAYDOWN }
    [SerializeField] public AmmoType currentAmmoType;
    [SerializeField] private bool isStop;

    [SerializeField] public GameObject target;
    [SerializeField] private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        target = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMovement();
        HandleOutOfRange();
        Disable();
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void HandleMovement()
    {
        if(!isStop)
        {
            if (currentAmmoType == AmmoType.PULSE)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            if (currentAmmoType == AmmoType.BEAM)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
            }
            if (currentAmmoType == AmmoType.TAIL)
            {
                //Debug.Log("Tail: " + rb.velocity.x);
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            if (currentAmmoType == AmmoType.SECOND)
            {
                rb.velocity = VectorFromAngle(45) * speed;
            }
            if (currentAmmoType == AmmoType.MISSILE)
            {
                rb.velocity = VectorFromAngle(315) * speed;
            }
            if (currentAmmoType == AmmoType.SPREAD)
            {
                rb.gravityScale = 1;
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            if (currentAmmoType == AmmoType.TORPEDO)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
                //Debug.Log(rb.velocity);
            }
            if (currentAmmoType == AmmoType.TWOWAYUP)
            {
                rb.velocity = VectorFromAngle(45) * speed;
            }
            if (currentAmmoType == AmmoType.TWOWAYDOWN)
            {
                rb.velocity = VectorFromAngle(-45) * speed;
            }
            if (currentAmmoType == AmmoType.SINGLE)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
        }
    }

    private void HandleOutOfRange()
    {
/*        if (!sprite.isVisible)
            this.gameObject.SetActive(false);*/
    }

    private void OnEnable()
    {
        if (currentAmmoType == AmmoType.PULSE)
        {
            //StartCoroutine(Deactive());
            StartCoroutine(ScaleUp());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(currentAmmoType != AmmoType.SPREAD)
        {
            if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
            {
                //Debug.Log("Collide with player");
                isStop = true;
                rb.velocity = Vector2.zero;
                //Debug.Log(isStop);
                RuntimeAnimatorController rac = Resources.Load("SurfaceShooting") as RuntimeAnimatorController;
                boxCollider.enabled = false;
                animator.runtimeAnimatorController = rac;
                animator.Play("SurfaceShootingEffect");
            }

            if(collision.gameObject.CompareTag("LineWall"))
            {

                isStop = true;
                rb.velocity = Vector2.zero;
                boxCollider.isTrigger = true;
                RuntimeAnimatorController rac = Resources.Load("SurfaceShooting") as RuntimeAnimatorController;
                boxCollider.enabled = false;
                animator.runtimeAnimatorController = rac;
                animator.Play("SurfaceShootingEffect");
            }
        }

        if(currentAmmoType == AmmoType.SPREAD)
        {
            if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemy"))
            {
                //Debug.Log("Collide with player");
                isStop = true;
                rb.velocity = Vector2.zero;
                //Debug.Log(isStop);
                RuntimeAnimatorController rac = Resources.Load("BombBlast") as RuntimeAnimatorController;
                boxCollider.enabled = false;
                rb.gravityScale = 0;
                animator.runtimeAnimatorController = rac;
            }

            if (collision.gameObject.CompareTag("LineWall"))
            {

                isStop = true;
                rb.velocity = Vector2.zero;
                boxCollider.isTrigger = true;
                RuntimeAnimatorController rac = Resources.Load("BombBlast") as RuntimeAnimatorController;
                boxCollider.enabled = false;
                rb.gravityScale = 0;
                animator.runtimeAnimatorController = rac;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LineWall"))
        {
            isStop = true;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnDisable()
    {
        isStop = false;
        boxCollider.enabled = true;
        boxCollider.isTrigger = true;
        animator.runtimeAnimatorController = null;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255);
    }

    private void Disable()
    {
        if(sprite.color.a == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
