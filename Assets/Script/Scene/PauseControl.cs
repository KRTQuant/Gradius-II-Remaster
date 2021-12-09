using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;
    public Text pauseText;

    private void Start()
    {
        pauseText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (pauseText == null)
        {
            pauseText = GameObject.Find("PauseText").GetComponent<Text>();
            pauseText.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
        Debug.Log(pauseText.gameObject);
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            pauseText.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseText.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

}
