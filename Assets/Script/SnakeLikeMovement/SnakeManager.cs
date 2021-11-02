using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] private float distanceBetween = 0.1f;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> snakeBody = new List<GameObject>();

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
                snakeBody[i].transform.position = markM.markerList[0].position;
                snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                markM.markerList.RemoveAt(0);
            }
        }
    }

    void CreateBodyParts()
    {
        if(snakeBody.Count == 0)
        {
            GameObject temp1 = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
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
        if(countUp >= distanceBetween)
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
