using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCompose : MonoBehaviour
{
    [SerializeField] public GameObject backgroundHierachy;
    [SerializeField] public float bgMovespeed;

    private void Update()
    {
        backgroundHierachy.transform.position -= bgMovespeed * Time.deltaTime * Vector3.right;
    }
}

