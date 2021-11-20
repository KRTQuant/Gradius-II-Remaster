using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch_Enemy : MonoBehaviour
{
    [SerializeField] private float detectRange;
    [SerializeField] private GameObject rushPrefab;
    [SerializeField] private float rushAmount;
    [SerializeField] private float waitTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Debug.Log("Recieved Input");
            Takeoff();
        }
    }

    private void Detect()
    {


    }

    private void Takeoff()
    {
        StartCoroutine(WaitLaunch());
    }

    private IEnumerator WaitLaunch()
    {
        for (int i = 0; i < rushAmount; i++)
        {
            yield return new WaitForSeconds(waitTime);
            GameObject rush = Instantiate<GameObject>(rushPrefab, transform.position, Quaternion.identity);
            Debug.Log("Create at: " + Time.time);
            rush.transform.parent = this.gameObject.transform;
        }
    }
}
