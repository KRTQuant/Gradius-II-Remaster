using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level
{ 
    LEVEL_1,
    LEVEL_2
}

[System.Serializable]
public struct LevelInfo
{
    [SerializeField] public Level level;
    [SerializeField] List<GameObject> phaseList;

}

public class PhaseManager : MonoBehaviour
{
    [SerializeField] private List<LevelInfo> phaseList;
    public static PhaseManager Instance { get { return _instance; } }
    public static PhaseManager _instance;

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
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
}
