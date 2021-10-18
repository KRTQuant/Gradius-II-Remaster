using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HordeEnemyType
{
    FAN_UP,
    FAN_DOWN,
    GARUN,
    GARUN_GOLD,
    RUGAL,
    RUGAL_GOLD,
}

[System.Serializable] public class HordeInfo
{
    public HordeEnemyType type;
    public GameObject prefab;
    public GameObject singleUnit;
    public enum rowAxis { VERTICAL, HORIZONTAL }
    public rowAxis axis;
    public bool isReqFleet;
    public int amount;
    public GameObject fleet;
    public bool isDead;

    [HideInInspector] public List<GameObject> listofHorde;
}

public class HordeManager : MonoBehaviour
{
    [SerializeField] private List<HordeInfo> listofHorde;

    private void GenerateHorde(HordeInfo info)
    {
        if(info.isReqFleet) // require fleet 
        {
            //instantiate fleet leader
            Transform fleet = info.fleet.transform;
            GameObject parent = new GameObject();
            parent.name = info.type.ToString();
            GameObject leader = Instantiate(info.prefab, fleet.position, fleet.rotation, parent.transform);
            SpriteRenderer sprite = leader.GetComponentInChildren<SpriteRenderer>();
            info.listofHorde.Add(leader);
            if (info.amount > 0)
            {
                Debug.Log("amount more than 0");
                for (int i = 0; i < info.amount - 1; i++)
                {
                    GameObject spawned = Instantiate(info.singleUnit, fleet.position, fleet.rotation, parent.transform);
                    //adjust position
                    if (info.axis == HordeInfo.rowAxis.HORIZONTAL)
                    {
                        float width = sprite.bounds.extents.x * 2;
                        spawned.transform.position = new Vector3(leader.transform.GetChild(0).transform.position.x + (width * i), leader.transform.GetChild(0).transform.position.y, transform.position.z);
                        Debug.Log(spawned.transform.position);
                    }
                    if (info.axis == HordeInfo.rowAxis.VERTICAL)
                    {
                        float height = sprite.bounds.extents.y * 2;
                        spawned.transform.position = new Vector3(leader.transform.GetChild(0).transform.position.x, leader.transform.GetChild(0).transform.position.y + (height * i), transform.position.z);
                    }
                    if (info.type == HordeEnemyType.FAN_UP || info.type == HordeEnemyType.FAN_DOWN)
                    {
                        Debug.Log("Set duplicated's destination");
                        spawned.GetComponent<FanEnemyScript>().destination = leader.GetComponentInChildren<FanEnemyScript>().destination;
                    }
                    info.listofHorde.Add(spawned);
                }
            }
        }
        else // independent
        {
            GameObject leader = Instantiate(info.prefab, new Vector2(0, 0), Quaternion.identity);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
            GenerateHorde(listofHorde[0]);
        if (Input.GetKeyDown(KeyCode.Keypad1))
            GenerateHorde(listofHorde[1]);
        if (Input.GetKeyDown(KeyCode.Keypad2))
            GenerateHorde(listofHorde[2]);
        if (Input.GetKeyDown(KeyCode.Keypad3))
            GenerateHorde(listofHorde[3]);
        if (Input.GetKeyDown(KeyCode.Keypad4))
            GenerateHorde(listofHorde[4]);
        if (Input.GetKeyDown(KeyCode.Keypad5))
            GenerateHorde(listofHorde[5]);
    }
}
