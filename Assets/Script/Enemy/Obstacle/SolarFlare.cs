using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarFlare : UnitAbs
{
    [SerializeField] Animator animator;
    [SerializeField] private float delay;
    [SerializeField] private float interval;
    [SerializeField] private bool isEnabled = false;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        Debug.Log("Init pos");
    }

    private void Update()
    {
        if(isStart && !isEnabled)
        {
            InvokeRepeating("ActiveFlare", delay, interval);
            isEnabled = true;
        }
    }

    private void ActiveFlare()
    {
        animator.SetTrigger("Launch");
        GetComponent<UpdatePolygonCollider>().enabled = true;
    }

    public override void OnBecameInvisible()
    {
        GetComponent<UpdatePolygonCollider>().enabled = false;
        base.OnBecameInvisible();
    }
}
