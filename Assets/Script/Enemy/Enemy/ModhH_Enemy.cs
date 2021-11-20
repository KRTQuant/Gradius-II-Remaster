using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModhH_Enemy : UnitAbs
{
    [TextArea] [Header("Note")]
    [SerializeField] private string Notes;

    [Space(10)]
    [SerializeField] private GameObject maksurion;
    [SerializeField] private GameObject makParent;

    [SerializeField] private int spawnAmount;
    [SerializeField] private float delayBtwSpawn;

    [SerializeField] public bool isStartSpawn;
    [SerializeField] private float timer;
    [SerializeField] private bool isSpawn;


    // Update is called once per frame
    void Update()
    {
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
            if (isStartSpawn)
            {
                StartCoroutine(Spawner());
            }
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
        isSpawn = false;
    }
}
