using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenStar : UnitAbs
{
    [Header("Greenstar")]
    [SerializeField] private GameObject rushPrefab;
    [SerializeField] private float rushAmount;
    [SerializeField] private float delayBetweenSpawning;

    [SerializeField] private float delayAfterActive;
    [SerializeField] private float repeatTime;

    private void Start()
    {
        SetHealth();
    }

    private void Update()
    {
        //CheckDeath();
    }

    private void Takeoff()
    {
        StartCoroutine(WaitLaunch());
    }

    private IEnumerator WaitLaunch()
    {
        for (int i = 0; i < rushAmount; i++)
        {
            yield return new WaitForSeconds(delayBetweenSpawning);
            GameObject rush = Instantiate<GameObject>(rushPrefab, transform.position, Quaternion.identity);
            //Debug.Log("Create at: " + Time.time);
            rush.transform.parent = this.gameObject.transform;
        }
    }

    public override void OnBecameVisible()
    {
        repeatTime = (rushAmount * delayBetweenSpawning) + 1;
        Debug.Log(repeatTime);
        InvokeRepeating("Takeoff", delayAfterActive, repeatTime);
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
