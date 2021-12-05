using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheonixSpread : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
