using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSun : UnitAbs
{
    public virtual void OnBecameInvisible()
    {
        StartCoroutine(DelayToDisappear());
    }

    public IEnumerator DelayToDisappear()
    {
        if (isStart)
        {
            yield return new WaitForSeconds(deactiveDelay);
            this.gameObject.SetActive(false);
        }
    }
}
