using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch_Enemy : UnitAbs
{
    [Header("Hatch")]
    [SerializeField] private float detectRange;
    [SerializeField] private GameObject rushPrefab;
    [SerializeField] private float rushAmount;
    [SerializeField] private float waitTime;
    [SerializeField] private Animator anim;

    [SerializeField] private float delayAfterActive;

    private void Start()
    {
        anim = GetComponent<Animator>();
        SetHealth();
    }

    private void Update()
    {
        CheckDeath();
    }

    private void Takeoff()
    {
        StartCoroutine(WaitLaunch());
    }

    private IEnumerator WaitLaunch()
    {
        anim.SetBool("Open", true);
        for (int i = 0; i < rushAmount; i++)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject rush = Instantiate<GameObject>(rushPrefab, transform.position, Quaternion.identity);
            Debug.Log("Create at: " + Time.time);
            rush.transform.parent = this.gameObject.transform;
        }
    }

    public override void OnBecameVisible()
    {
        Invoke("Takeoff", delayAfterActive);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage((int)collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
        }
    }
}
