using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePolygonCollider : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D polygon;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //[SerializeField] private 

    private void Start()
    {
        polygon = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


}
