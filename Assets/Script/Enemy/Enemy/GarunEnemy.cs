using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarunEnemy : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float magnitude;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        HandleMove();
        Debug.Log("Test");
    }

    private void HandleMove()
    {
        transform.position -= transform.right * Time.deltaTime * speed;
        transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude/100;
    }
}
