using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    private void Update()
    {
        Solution1();
    }

    private void Solution1()
    {
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        FindReference();
        Vector3 viewPos = transform.position;
        //Debug.Log(-screenBounds.y);
        if (cameraFollow.enabled)
        {
            Debug.Log(SceneManager.GetActiveScene().name);
            if(SceneManager.GetActiveScene().name == "Stage02")
            {
                viewPos.y = Mathf.Clamp(viewPos.y, ((screenBounds.y + 6.25f) * -1) + objectHeight, screenBounds.y - objectHeight + 6.25f);
            }
            else if(SceneManager.GetActiveScene().name == "Stage01_AE")
            {
                viewPos.y = Mathf.Clamp(viewPos.y, ((screenBounds.y + 5) * -1) + objectHeight, screenBounds.y - objectHeight + 5);
            }
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

    private void FindReference()
    {
        if (phaseManager == null || cameraFollow == null)
        {
            phaseManager = GameObject.Find("PhaseManager").GetComponent<PhaseManager>();
            cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        }
    }
}
