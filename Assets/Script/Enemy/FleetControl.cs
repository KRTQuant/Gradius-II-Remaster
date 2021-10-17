/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetControl : MonoBehaviour
{
    [SerializeField] public List<GameObject> enemyShip = new List<GameObject>();
    // Start is called before the first frame update
    public void UpdatePosition(string enemyType)
    {
        if (enemyType == HordeControl.EnemyType.FAN.ToString() || enemyType == HordeControl.EnemyType.FAN.ToString())
        {
            Debug.Log(enemyShip[0].GetComponentInChildren<SpriteRenderer>().name);
            float width = enemyShip[0].GetComponentInChildren<SpriteRenderer>().bounds.size.x / enemyShip.Count;
            for (int i = 0; i < enemyShip.Count; i++)
            {
                enemyShip[i].transform.position = new Vector2(enemyShip[i].transform.position.x + (width * i), enemyShip[i].transform.position.y);
            }
        }
        if (enemyType == HordeControl.EnemyType.GARUN.ToString() || enemyType == HordeControl.EnemyType.GARUN_GOLD.ToString())
        {
            Debug.Log(enemyShip[0].GetComponentInChildren<SpriteRenderer>().name);
            float height = enemyShip[0].GetComponentInChildren<SpriteRenderer>().bounds.size.x / enemyShip.Count;
            for (int i = 0; i < enemyShip.Count; i++)
            {
                enemyShip[i].transform.position = new Vector2(enemyShip[i].transform.position.x, enemyShip[i].transform.position.y + (height * i));
            }
        }
        else
        {
            return;
        }
    }
}
*/