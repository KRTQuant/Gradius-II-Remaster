using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillSelectManager : MonoBehaviour
{
    [SerializeField] private List<Button> powerStyle = new List<Button>();
    [SerializeField] private GameObject indicator;
    [SerializeField] private float currentOrder;
    [SerializeField] private int maxIndicator;
    [SerializeField] private int minIndicator;
    [SerializeField] private float waitSec;
    [SerializeField] private bool isSelected = false;

    [SerializeField] private GameManager gameManager;
     
    public void OnMouseOver(int value)
    {
        currentOrder = value;
        //Debug.Log("set current order : " + value);
        SetIndicatorPos();
    }

    public void OnMouseClick(int value)
    {
        if(!isSelected)
        {
            gameManager.HandleSkillSetSelection((int)currentOrder);
            Debug.Log("sent order : " + value);
            isSelected = true;
        }
    }

    private void Update()
    {
        ControlIndicator();
    }

    private void Start()
    {
        maxIndicator = powerStyle.Count - 1;
        minIndicator = 0;
    }

    private void SetIndicatorPos()
    {
        Vector2 tempPos = new Vector2(indicator.transform.position.x, powerStyle[(int)currentOrder].gameObject.transform.position.y);
        indicator.transform.position = tempPos;
    }


    private void ControlIndicator()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentOrder++;
            //Debug.Log("Order down");
            if (currentOrder > maxIndicator)
                currentOrder = 0;
            SetIndicatorPos();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentOrder--;
            //Debug.Log("Order up");
            if (currentOrder < minIndicator)
                currentOrder = maxIndicator;
            SetIndicatorPos();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            if(!isSelected)
            {
                gameManager.HandleSkillSetSelection((int)currentOrder);
                isSelected = true;
            }
        }
    }
}
