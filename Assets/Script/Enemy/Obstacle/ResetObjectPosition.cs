using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjectPosition : UnitAbs
{
    public float timer;
    const float delay = 15;
    public SpriteRenderer spriteRenderer;
    public Renderer renderer;

    public override void Start()
    {
        
        timer = delay;
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(isStart)
        {
            if (renderer.isVisible)
            {
                timer = delay;
            }
            if (!renderer.isVisible)
            {
                timer -= Time.deltaTime;
            }
            if (timer < 0)
            {
                this.gameObject.SetActive(false);
            }
        }

    }
}
