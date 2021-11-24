using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beans_Enemy : UnitAbs
{
    [SerializeField] private float frequency = 5f;
    [SerializeField] private float magnitude;
    [SerializeField] private float speed;
    [SerializeField] private float divisor;
    [SerializeField] private float waitTime;

    [SerializeField] private Rigidbody2D rb;


    private void FixedUpdate()
    {
        if (isStart)
        {
            transform.position -= transform.right * Time.deltaTime * speed;
            if (divisor == 0)
                transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
            else
                transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequency) * magnitude / divisor;
        }
    }
}
