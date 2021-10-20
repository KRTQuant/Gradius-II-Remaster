using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneIntroManager : MonoBehaviour
{
    public AnimateLogo Scene;
    public GameObject Blackbox;
    // Start is called before the first frame update
    void Start()
    {
        Blackbox = GameObject.Find("BlackBox");
        Scene = Blackbox.GetComponent<AnimateLogo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Scene.AnimateEnd == true)
        {
            SceneManager.LoadScene("UI_Menu");
        }
    }
}
