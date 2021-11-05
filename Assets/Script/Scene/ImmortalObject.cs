using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImmortalObject : MonoBehaviour
{
    [SerializeField] PlayerScript mc_controller;
    [SerializeField] float skillSelectedType = 0;
    [SerializeField] float waitSec;
    [SerializeField] bool isParamsWasPass;
    [SerializeField] private Scene Level_1;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void HandleSkillSetSelection(int type)
    {
        skillSelectedType = type;
        Debug.Log("(Immortal Obj)You select type : " + type);
        StartCoroutine(HandleSkillSelected());
    }

    public void PassPowersetSelection()
    {
        if (GameObject.Find("playerScript"))
        {
            mc_controller = GameObject.Find("Player").GetComponent<PlayerScript>();
            mc_controller.selectedSkillset = (PlayerScript.Skillset)skillSelectedType;
        }
    }
    IEnumerator HandleSkillSelected()
    {
        yield return new WaitForSeconds(waitSec);
        Debug.Log("Wait for " + waitSec + "seconds");
        Debug.Log("Load Scene");
        SceneManager.LoadScene("Stage01_A");
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Stage01_A" && !isParamsWasPass)
        {
            Debug.Log("Begin passing powerset");
            PassPowersetSelection();
            isParamsWasPass = true;
            Debug.Log((PlayerScript.Skillset)skillSelectedType);
            GameObject.Find("Player").GetComponent<PlayerScript>().HandleSelectSkillset((PlayerScript.Skillset)skillSelectedType);
        }
    }
}
