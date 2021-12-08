using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathHand_Behavior
{ 
    INACTIVE,
    INIT,
    ACTIVE,
    RETREAT
}

public class DeathHand_Enemy : UnitAbs
{
    [Header("Death Hand")]
    [SerializeField] private List<GameObject> childArm;
    [SerializeField] public DeathHand_Behavior currentBehav;
    [SerializeField] private Transform stopPosition;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public bool isNestActive;
    [SerializeField] float moveSpeed;
    [SerializeField] float turnSpeed;

    [SerializeField] GameObject player;
    [SerializeField] GameObject basePosition;
    [SerializeField] GameObject baseNode;

    public override void Start()
    {
        base.Start();
        currentBehav = DeathHand_Behavior.INACTIVE;
        baseNode = childArm[childArm.Count - 1];
    }

    private void Update()
    {
        if(isStart)
            UpdateBehav();
    }

    private void UpdateBehav()
    {
        if (currentBehav == DeathHand_Behavior.INIT)
            InitBehavior();

        if(transform.position.y == stopPosition.position.y)
        {
            currentBehav = DeathHand_Behavior.ACTIVE;
        }

        if (currentBehav == DeathHand_Behavior.ACTIVE)
            ActiveBehavior();
    }

    public void InitBehavior()
    {
        Invoke("MoveToTarget", 3);
    }

    private void ActiveBehavior()
    {
        //get traget
        player = GameObject.Find("Player");
        //rotate to player
        for(int i = 0; i < childArm.Count; i++)
        {
            Vector2 dir = player.transform.position - childArm[i].transform.position;
            dir.Normalize();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, transform.forward);
            childArm[i].transform.rotation = Quaternion.Lerp(childArm[i].transform.rotation, rotation, turnSpeed * Time.deltaTime);
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, stopPosition.position, moveSpeed * Time.deltaTime);
    }

    public void HandleBulletCollide()  
    {
        //retreat one node of death hand back to nest
        baseNode = childArm[childArm.IndexOf(baseNode) - 1];
        Debug.Log(childArm.IndexOf(baseNode));
        //baseNode.transform.position = Vector2.MoveTowards(baseNode.transform.position, basePosition.transform.position, moveSpeed * Time.deltaTime);
        //StartCoroutine(MoveObject(baseNode, baseNode.transform, basePosition.transform, moveSpeed));

        baseNode.transform.localPosition = Vector3.zero;

    }

    IEnumerator MoveObject(GameObject gameObject, Transform start, Transform end, float speed)
    {
        yield return new WaitForSeconds(0);
        gameObject.transform.position = Vector2.MoveTowards(start.position, end.position, speed * Time.deltaTime);
    }
}
