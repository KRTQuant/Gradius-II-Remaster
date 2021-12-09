using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rugal_Enemy : UnitAbs
{
    [Header("Rugal")]
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float timer;
    [SerializeField] private bool isSpawnCapsule;
    [SerializeField] private PhaseManager phaseManager;
    [SerializeField] private GameObject capsule;

    public override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        SetHealth();
        phaseManager = GameObject.Find("PhaseManager").GetComponent<PhaseManager>();

    }

    private void Update()
    {
        SetPlayerRef();
        CheckDeath();

        if (isStart)
        {
            Homing();
            timer += Time.deltaTime;
        }
    }

    private void Homing()
    {
        Vector2 dir = player.transform.position - this.transform.position;
        dir.Normalize();
        float rotateAmount = Vector3.Cross(dir, -transform.right).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
        rb.velocity = -transform.right * speed;
    }

    public override void OnBecameVisible()
    {
        isStart = true;
    }

    public override void OnBecameInvisible()
    {
        if(isStart && timer > 5)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void SetPlayerRef()
    {
        if (player == null)
        {
            player = GameObject.Find("VicViper");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage((int)collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
        }
    }

    public override void TriggerOnDeath()
    {
        if (isSpawnCapsule)
        {
            Instantiate<GameObject>(capsule, transform.position, Quaternion.identity, phaseManager.phase[phaseManager.currentPhase].phaseTransform.gameObject.transform);

        }
        base.TriggerOnDeath();
    }
}
