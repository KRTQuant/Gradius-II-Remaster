using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeFollowManager : MonoBehaviour
{
    [SerializeField] public float distanceBetween = 0.1f;
    [SerializeField] private List<GameObject> fleetParts = new List<GameObject>();
    [SerializeField] List<GameObject> fleetBody = new List<GameObject>();
    [SerializeField] private Transform player;
    [SerializeField] private Transform parent;
    [SerializeField] private Rigidbody2D rb;

    float countUp = 0;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        rb = this.GetComponent<Rigidbody2D>();
        CreatefleetParts();
    }

    private void FixedUpdate()
    {
        ManageFleet();
        SnakeMovement();
    }

    void SnakeMovement()
    {
        if (fleetBody.Count > 1)
        {
            for (int i = 1; i < fleetBody.Count; i++)
            {
                MarkerManager markM = fleetBody[i - 1].GetComponent<MarkerManager>();
                fleetBody[i].transform.position = markM.markerList[0].position;
                fleetBody[i].transform.rotation = markM.markerList[0].rotation;
                markM.markerList.RemoveAt(0);
            }
        }
    }

    void CreatefleetParts()
    {
        if (fleetBody.Count == 0)
        {
            GameObject temp1 = this.gameObject;
            if (!temp1.GetComponent<MarkerManager>())
                temp1.AddComponent<MarkerManager>();
            if (!temp1.GetComponent<Rigidbody2D>())
            {
                temp1.AddComponent<Rigidbody2D>();
                temp1.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            fleetBody.Add(temp1);
            fleetParts.RemoveAt(0);
        }
        MarkerManager markM = fleetBody[fleetBody.Count - 1].GetComponent<MarkerManager>();
        if (countUp == 0)
        {
            markM.ClearMarkerList();
        }
        countUp += Time.deltaTime;
        if (countUp >= distanceBetween)
        {
            GameObject temp = Instantiate(fleetParts[0], markM.markerList[0].position, markM.markerList[0].rotation, parent);
            if (!temp.GetComponent<MarkerManager>())
                temp.AddComponent<MarkerManager>();
            if (!temp.GetComponent<Rigidbody2D>())
            {
                temp.AddComponent<Rigidbody2D>();
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            fleetBody.Add(temp);
            fleetParts.RemoveAt(0);
            temp.GetComponent<MarkerManager>().ClearMarkerList();
            countUp = 0;
        }
    }

    void UpdateFleetBody()
    {
        for(int i = 0; i < fleetBody.Count; i++)
        {
            if(fleetBody[i] == null || !fleetBody[i].gameObject.activeSelf)
            {
                fleetBody.RemoveAt(i);
                i--;
            }
        }
    }

    void ManageFleet()
    {
        if (fleetParts.Count > 0)
        {
            CreatefleetParts();
        }
        for (int i = 0; i < fleetBody.Count; i++)
        {
            if (fleetBody[i] == null || !fleetBody[i].gameObject.activeSelf)
            {
                fleetBody.RemoveAt(i);
                i--;
            }
        }
    }
}
