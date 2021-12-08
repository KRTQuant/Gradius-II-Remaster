using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable] public class PhaseInfo
{
    public GameObject phaseTransform;
    public List<GameObject> unitList = new List<GameObject>();
    public bool isActiveBoundary;
    public Vector2 phasePosition;
    public bool isBoss;
}


public class PhaseManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] public List<PhaseInfo> phase;
    [SerializeField] private SceneCompose sceneCompose;
    [SerializeField] private float loadCheckpointDelay;
    [SerializeField] private CameraFollow camFollow;
    [SerializeField] public bool isDecrease;
    [SerializeField] public int currentPhase = 0;

    //[Header("Singleton")]
    //public static PhaseManager _instance;
    //public static PhaseManager Instance { get { return _instance; } }

    private void Awake()
    {
        //Singleton();
    }
    private void Start()
    {
        GetPhaseUnit();
        GetComponentReference();
    }

    private void FixedUpdate()
    {
        //GetPhaseUnit();
        //ClearPhaseUnitList();
    }

    private void Update()
    {
        if(IsAllUnitDeactive())
        {
            HandleChangePhase();
            if(currentPhase == phase.Count)
            {
                Debug.Log("Just change scene");
                int temp = SceneManager.GetActiveScene().buildIndex;
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    temp = SceneManager.GetActiveScene().buildIndex;
                    gameManager.LoadScene(temp + 1);
                }
                if (SceneManager.GetActiveScene().buildIndex == 4)
                {
                    gameManager.LoadScene(1);
                }
            }
        }
        DecreaseCameraLimit();
        Cheat();
    }

    private void GetComponentReference()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sceneCompose = GameObject.Find("SceneComposer").GetComponent<SceneCompose>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        camFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    //private void Singleton()
    //{
    //    // if the singleton hasn't been initialized yet
    //    if (_instance != null && _instance != this)
    //    {
    //        Destroy(this.gameObject);
    //        return;//Avoid doing anything else
    //    }
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //}

    private void HandleChangePhase()
    {
        //change phase
        ResetPhaseInfo();
        currentPhase++;
        if (phase[currentPhase].isActiveBoundary)
        {
            camFollow.enabled = true;
        }
        if(phase[currentPhase-1].isActiveBoundary)
        {
            isDecrease = true;
        }
        if(phase[currentPhase].isBoss)
        {
            sceneCompose.bgMovespeed = 0;
        }
        sceneCompose.backgroundHierachy = phase[currentPhase].phaseTransform;
    }


    private void GetPhaseUnit()
    {
        for(int i = 0; i < phase.Count; i++)
        {
            var units = phase[i].phaseTransform.GetComponentsInChildren<UnitAbs>();
            foreach(var unit in units)
            {
                phase[i].unitList.Add(unit.gameObject);
            }
        }
    }

    private bool IsAllUnitDeactive()
    {
        if(currentPhase > phase.Count - 1)
        {
            ChangeScene();
            return true;
        }
        if(phase[currentPhase] == null)
        {
            return true;
        }
        if (phase[currentPhase] != null)
        {
            foreach (var unit in phase[currentPhase].unitList)
            {
                if (unit.activeSelf)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void Cheat()
    {
        //deactive all unit
        if(Input.GetKeyDown(KeyCode.Keypad0))
        {
            var units = FindObjectsOfType<UnitAbs>();
            foreach(var unit in units)
            {
                unit.gameObject.SetActive(false);
            }
        }

        //deactive all unit in current phase list
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            var units = phase[currentPhase].phaseTransform.GetComponentsInChildren<UnitAbs>();
            foreach (var unit in units)
            {
                unit.gameObject.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            playerMovement.HandleAfterDead();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            playerMovement.gameObject.GetComponent<BoxCollider2D>().enabled = !playerMovement.gameObject.GetComponent<BoxCollider2D>().enabled;
        }
    }

    private void ManageBoundary()
    {
        if(phase[currentPhase].isActiveBoundary)
        {

        }
    }

    private void ActiveFleet()
    {
        phase[currentPhase].phaseTransform.SetActive(true);
        if(phase[currentPhase].isBoss)
            sceneCompose.bgMovespeed = 0;
        else
            sceneCompose.bgMovespeed = 1;
    }

    private void ResetPhaseInfo()
    {
        phase[currentPhase].phaseTransform.SetActive(false);
        phase[currentPhase].phaseTransform.transform.position = Vector3.zero * 0;
        sceneCompose.bgMovespeed = 0;
        foreach (var unit in phase[currentPhase].unitList)
        {
            unit.GetComponent<UnitAbs>().ResetPosition();
            unit.SetActive(true);
        }
    }

    private void ChangeScene()
    {
        Debug.Log("Just change scene");
        int temp = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            temp = SceneManager.GetActiveScene().buildIndex;
            gameManager.LoadScene(temp + 1);
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            gameManager.LoadScene(1);
        }
    }

    public void LoadCheckPoint()
    {
        StartCoroutine(DelayBeforeLoadCheckPoint());
    }

    public IEnumerator DelayBeforeLoadCheckPoint()
    {
        yield return new WaitForSeconds(loadCheckpointDelay);
        ResetPhaseInfo();
        playerMovement.gameObject.SetActive(true);
        yield return new WaitForSeconds(loadCheckpointDelay);
        ActiveFleet();
    }

    public IEnumerator DelayBeforeLoadCheckPoint(int phaseIndex)
    {
        yield return new WaitForSeconds(loadCheckpointDelay);
        var units = phase[currentPhase].phaseTransform.GetComponentsInChildren<UnitAbs>();
        foreach (var unit in units)
        {
            unit.gameObject.SetActive(false);
        }
        //ResetPhaseInfo();
        currentPhase = phaseIndex;
        yield return new WaitForSeconds(loadCheckpointDelay);
        ActiveFleet();
        sceneCompose.backgroundHierachy = phase[currentPhase].phaseTransform;
    }

    private void DecreaseCameraLimit()
    {
        if(isDecrease)
        {
            camFollow.bottomLimit += Time.deltaTime;
            camFollow.topLimit -= Time.deltaTime;
            if(camFollow.topLimit < 0 && camFollow.bottomLimit > 0)
            {
                camFollow.enabled = false;
                isDecrease = false;
                ActiveFleet();
            }
        }
        else
        {
            ActiveFleet();
        }
    }
}
