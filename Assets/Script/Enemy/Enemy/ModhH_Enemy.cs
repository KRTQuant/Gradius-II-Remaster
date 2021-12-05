using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModhH_Enemy : UnitAbs
{

    [Header("Modh H")]
    [SerializeField] private GameObject maksurion;
    [SerializeField] private GameObject makParent;

    [SerializeField] private int spawnAmount;
    [SerializeField] private float delayBtwSpawn;

    [SerializeField] public bool isStartSpawn;
    [SerializeField] private float timer;
    [SerializeField] private bool isSpawn;

    [Tooltip("Delay before start spawn")]
    [SerializeField] public float delay;

    private void Start()
    {
        SetHealth();
    }
    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        ManageTimer();
        ControlSpawning();
    }

    void ManageTimer()
    {
        timer += Time.deltaTime;
    }

    void ControlSpawning()
    {
        if (isStart & !isSpawn)
        {
            Invoke("Spawn", delay);
            isSpawn = true;
        }
    }

    IEnumerator Spawner()
    {
        makParent = new GameObject("makParent");
        for (int i = 0; i < spawnAmount; i++)
        {
            GetComponent<Animator>().SetTrigger("StartSpawn");
            Instantiate(maksurion, transform.position, Quaternion.identity, makParent.transform);
            yield return new WaitForSeconds(delayBtwSpawn);
        }
    }

    private void Spawn()
    {
        StartCoroutine("Spawner");
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
