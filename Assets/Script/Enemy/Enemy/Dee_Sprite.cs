using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dee_Sprite : MonoBehaviour
{
    public Dee_Enemy dee_Enemy;
    private void OnBecameVisible()
    {
        dee_Enemy.isStart = true;
    }

    private void Start()
    {
        dee_Enemy.GetComponentInChildren<Dee_Enemy>();
    }
}
