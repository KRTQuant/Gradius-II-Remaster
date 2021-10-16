using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public enum PhaseType { PHASE1, PHASE2, PHASE3, BOSS }

[System.Serializable]
public class PhaseInfo
{
    public PhaseType phase;
    public int sceneInPhase;
    public int currentBackground;
    public List<GameObject> listofScene = new List<GameObject>();
    public float timeInScene;
}*/

/*    [SerializeField] private List<PhaseInfo> listofPhase;
    [SerializeField] private PhaseType currentPhase;
    [SerializeField] private GameObject player;*/

public class SceneCompose : MonoBehaviour
{
    [SerializeField] private GameObject backgroundHierachy;
    [SerializeField] private float bgMovespeed;

    private void Start()
    {
        
    }

    private void Update()
    {
        backgroundHierachy.transform.position -= bgMovespeed * Time.deltaTime * Vector3.right;
    }
}

