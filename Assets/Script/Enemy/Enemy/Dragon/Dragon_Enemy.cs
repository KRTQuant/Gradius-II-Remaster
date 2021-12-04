using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_Enemy : UnitAbs
{
    [Header("Dragon Enemy")]
    public Transform player;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        SetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
    }
}
