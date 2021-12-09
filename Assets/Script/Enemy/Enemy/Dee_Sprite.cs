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

    private void OnEnable()
    {
        dee_Enemy.gameObject.SetActive(true);
    }

    private void Start()
    {
        dee_Enemy.GetComponentInChildren<Dee_Enemy>();
    }

    private void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
        dee_Enemy.gameObject.SetActive(false);
    }
}
