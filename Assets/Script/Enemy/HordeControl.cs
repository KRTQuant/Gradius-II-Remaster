/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeControl : MonoBehaviour
{
    [SerializeField] public enum EnemyType { FAN, GARUN, GARUN_GOLD, RUGAL, RUGAL_GOLD }
    [SerializeField] private EnemyType selectEnemy;
    [SerializeField] private float hordeAmount;
    [SerializeField] private List<GameObject> enemyObj = new List<GameObject>();
    [SerializeField] private GameObject fanSpawnPos;
    [SerializeField] private GameObject fanEnemy;

    private void CallHorde(EnemyType type, int amount)
    {
        GameObject fleet = new GameObject();
        fleet.name = type.ToString() + " Fleet";
        fleet.gameObject.AddComponent<FleetControl>();
        GameObject enemy;
        if (selectEnemy == EnemyType.FAN)
        {
            enemy = Instantiate(enemyObj[(int)selectEnemy], fanSpawnPos.transform.position, Quaternion.identity);
            enemy.transform.parent = fleet.transform;
            fleet.GetComponent<FleetControl>().enemyShip.Add(enemy);
            while (hordeAmount > 1 && fleet.GetComponent<FleetControl>().enemyShip.Count < hordeAmount)
            {
                GameObject spawned = Instantiate<GameObject>(fanEnemy, GameObject.Find("FanEnemy").transform.position, Quaternion.identity);
                fleet.GetComponent<FleetControl>().enemyShip.Add(spawned);
                spawned.GetComponent<FanEnemyScript>().destination = enemy.GetComponentInChildren<FanEnemyScript>().destination;
                spawned.transform.parent = fleet.transform;
                Debug.Log("Spawned something");
            }
            fleet.GetComponent<FleetControl>().UpdatePosition(selectEnemy.ToString());
        }
        for (int i = 0; i < amount; i++)
        {
            if (selectEnemy != EnemyType.FAN)
            {
                enemy = Instantiate(enemyObj[(int)selectEnemy], transform.position, Quaternion.identity);
                enemy.transform.parent = fleet.transform;
                fleet.GetComponent<FleetControl>().enemyShip.Add(enemy);
            }
            fleet.GetComponent<FleetControl>().UpdatePosition(selectEnemy.ToString());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
            CallHorde(selectEnemy, (int)hordeAmount);
    }

    private void Start()
    {
        //CallHorde(selectEnemy, (int)hordeAmount);
    }
}
*/