using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : UnitAbs
{
    PheonixBoss pheonix;

    public override void Start()
    {
        base.Start();
        pheonix = GetComponentInChildren<PheonixBoss>();
    }

    private void Update()
    {
        if(!pheonix.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
