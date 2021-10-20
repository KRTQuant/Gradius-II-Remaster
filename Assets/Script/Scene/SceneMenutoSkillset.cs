using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenutoSkillset : MonoBehaviour
{
    public void OnclickToSkillSetScene()
    {
        SceneManager.LoadScene("SkillSelect");
    }
}
