using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePolygonCollider : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D polygon;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite currentSprite;

    //[SerializeField] private 

    private void Start()
    {
        polygon = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentSprite = spriteRenderer.sprite;
    }

    private void Update()
    {
        if(currentSprite != spriteRenderer.sprite)
        {
            Destroy(polygon);
            currentSprite = spriteRenderer.sprite;
            polygon = gameObject.AddComponent<PolygonCollider2D>();
        }
    }


}
