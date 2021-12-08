using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    public CameraFollow cameraFollow;
    public PhaseManager phaseManager;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        phaseManager = GameObject.Find("PhaseManager").GetComponent<PhaseManager>();
    }

    private void Update()
    {
        Solution1();
    }

    private void Solution1()
    {
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        Vector3 viewPos = transform.position;
        //Debug.Log(-screenBounds.y);
        if (cameraFollow.enabled)
        {
            viewPos.y = Mathf.Clamp(viewPos.y, ((screenBounds.y + 5) * -1) + objectHeight, screenBounds.y - objectHeight + 5);
            //Debug.Log("CameraFollow enabled");
            Debug.Log((screenBounds.y * -1) + " : " + screenBounds.y);
        }
        if(phaseManager.isDecrease || !cameraFollow.enabled)
        {
            viewPos.y = Mathf.Clamp(viewPos.y, (screenBounds.y * -1) + objectHeight, screenBounds.y - objectHeight);
            //Debug.Log((screenBounds.y * -1) + " : " + screenBounds.y);
        }
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        transform.position = viewPos;
    }
}
