using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnakeManager : MonoBehaviour
{
    [SerializeField] private float distanceBetween;
    [SerializeField] private List<GameObject> bodyParts = new List<GameObject>();
    [SerializeField] private List<GameObject> snakeBody = new List<GameObject>();

    float countUp = 0;

    private void FixedUpdate()
    {
        if(bodyParts.Count > 0)
        {
            CreateBodyParts();
        }
        SnakeMovement();
    }

    private void Start()
    {
        CreateBodyParts();
    }

    void SnakeMovement()
    {
        if(snakeBody.Count > 1)
        {
            for(int i = 1; i < snakeBody.Count; i++)
            {
                MarkerManager markM = snakeBody[i - 1].GetComponent<MarkerManager>();
                if (markM.markerList.Count < 30)
                    return;
                else
                {
                    snakeBody[i].transform.position = markM.markerList[0].position;
                    snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                    markM.markerList.RemoveAt(0);
                }
            }
        }
    }

    void CreateBodyParts()
    {
        if(snakeBody.Count == 0)
        {
            GameObject temp1 = this.gameObject;
            if (!temp1.GetComponent<MarkerManager>())
                temp1.AddComponent<MarkerManager>();
            if (!temp1.GetComponent<Rigidbody2D>())
            {
                temp1.AddComponent<Rigidbody2D>();
                temp1.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            snakeBody.Add(temp1);
            bodyParts.RemoveAt(0);
        }
        MarkerManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();
        if(countUp == 0)
        {
            markM.ClearMarkerList();
        }
        countUp += Time.deltaTime;
        if(countUp >= distanceBetween + 1)
        {
            GameObject temp = Instantiate(bodyParts[0], markM.markerList[0].position, markM.markerList[0].rotation, transform);
            if (!temp.GetComponent<MarkerManager>())
                temp.AddComponent<MarkerManager>();
            if (!temp.GetComponent<Rigidbody2D>())
            {
                temp.AddComponent<Rigidbody2D>();
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            snakeBody.Add(temp);
            bodyParts.RemoveAt(0);
            temp.GetComponent<MarkerManager>().ClearMarkerList();
            countUp = 0;
        }
    }
}
