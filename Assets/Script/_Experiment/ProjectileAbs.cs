using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAbs : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isTrigger;

    private float SetVelocity(float mutiplier = 1)
    {
        float velo = mutiplier * speed * Time.deltaTime;
        Debug.Log(velo);
        return velo;
    }

    private void Update()
    {
        Debug.Log(SetVelocity());
    }
}
