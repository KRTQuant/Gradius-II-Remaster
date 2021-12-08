using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    public static GameManager _instance;

    [SerializeField] SkillManager skillManager;
    [SerializeField] float skillSelectedType = 0;
    [SerializeField] float waitSec;
    [SerializeField] bool isParamsWasPass;
    [SerializeField] private Text scoreText;

    [SerializeField] public int score = 0;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Update()
    {
        if ((SceneManager.GetActiveScene().name == "Stage01_AE" || SceneManager.GetActiveScene().name == "Stage02") && !isParamsWasPass)
        {
            Debug.Log("Begin passing powerset");
            PassPowersetSelection();
            isParamsWasPass = true;
            //Debug.Log((Skillset)skillSelectedType);
            FindRef();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isParamsWasPass = false;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void HandleSkillSetSelection(int type)
    {
        skillSelectedType = type;
        Debug.Log("(Immortal Obj)You select type : " + type);
        StartCoroutine(HandleSkillSelected());
    }

    public void PassPowersetSelection()
    {
        if (GameObject.Find("Player"))
        {
            skillManager = GameObject.Find("Player").GetComponent<SkillManager>();
            skillManager.SetSkillPreset((Skillset)skillSelectedType);
            Debug.Log((Skillset)skillSelectedType);
        }
    }

    IEnumerator HandleSkillSelected()
    {
        yield return new WaitForSeconds(waitSec);
        Debug.Log("Wait for " + waitSec + "seconds");
        Debug.Log("Load Scene");
        SceneManager.LoadScene("Stage01_AE");
    }

    public void IncreaseScore(int score)
    {
        this.score += score;
        FindRef();
        if(scoreText != null)
            scoreText.text = this.score.ToString();
    }

    public void FindRef()
    {
        if(scoreText == null)
        {
            scoreText = GameObject.Find("ScoreNumber").GetComponent<Text>();
        }
    }

    public void HandlePlayerDeath()
    {
        StartCoroutine(DelayToMainMenu());
    }

    IEnumerator DelayToMainMenu()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Load scene plz");
        SceneManager.LoadScene(1);
        StopCoroutine(DelayToMainMenu());
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
