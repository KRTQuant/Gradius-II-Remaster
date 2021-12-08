using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> fleetList;
    [SerializeField] private GameObject upgradeCapsule;
    [SerializeField] private Vector3 tempPos;

    private void Start()
    {
        GetAllUnitInFleet();
    }

    private void Update()
    {
        CheckStatus();
    }

    public void GetAllUnitInFleet()
    {
        foreach(Transform child in this.transform)
        {
            fleetList.Add(child.gameObject);
        }
    }

    public void CheckStatus()
    {
        foreach(var unit in fleetList.ToArray())
        {
            if(!unit.activeSelf)
            {
                if(fleetList.Count == 1)
                {
                    Instantiate<GameObject>(upgradeCapsule, fleetList[0].transform.position, Quaternion.identity);
                }
                fleetList.Remove(unit);
            }
        }
    }

    //Get all unit in fleet
    //if all unit are deactivated => spawn capsule
}
