using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> fleetList;
    [SerializeField] private GameObject upgradeCapsule;

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
        if (IsUnitsActive())
        {
            return;
        }
        else
        {
            Instantiate<GameObject>(upgradeCapsule, transform.position, Quaternion.identity);
        }
    }

    public bool IsUnitsActive()
    {
        for (int i = 0; i < fleetList.Count; i++)
        {
            if (fleetList[i].activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    //Get all unit in fleet
    //if all unit are deactivated => spawn capsule
}
