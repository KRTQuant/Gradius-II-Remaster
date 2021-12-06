using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LineWall : MonoBehaviour
{
    public Tilemap destructableMap;

    private void Start()
    {
        destructableMap = GetComponent<Tilemap>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log("Collided by player bullet");
            Vector3 hitPos = Vector3.zero;
            foreach(ContactPoint2D hit in collision.contacts)
            {
                hitPos.x = hit.point.x - 0.01f * hit.normal.x;
                hitPos.y = hit.point.y - 0.01f * hit.normal.y;
                destructableMap.SetTile(destructableMap.WorldToCell(hitPos), null);
            }
        }
    }
}
