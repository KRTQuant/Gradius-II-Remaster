using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheonixBoss : UnitAbs
{
    [SerializeField] private List<Transform> moveDir = new List<Transform>();
    [SerializeField] private float currentDir;
    [SerializeField] private float destination;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float offset;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private enum behaviorState { INIT, FLY, SHOOTFIREBALL, SHOOTSPREADSHOT }
    [SerializeField] private behaviorState currentBehavior;
    [SerializeField] private Animator animator;

    [Header("Spreadshot")]
    [SerializeField] private float bulletSpeed_Spread;
    [SerializeField] private int bulletSpreadAmount;
    [SerializeField] private GameObject spreadPrefab;
    [Range(0,720)]
    [SerializeField] private float endSpreadAngle = 0;
    [Range(0, 720)]
    [SerializeField] private float startSpreadAngle = 0;
    [SerializeField] private Vector3 spreadBulletDir;

    [Header("Fireball")]
    [SerializeField] private float bulletSpeed_Fireball;
    [SerializeField] private int bulletFireballAmount;
    [SerializeField] private int bulletWave;
    [SerializeField] private GameObject fireballPrefab;
    [Range(0, 360)]
    [SerializeField] private float startFireBallAngle = 0;
    [Range(0, 360)]
    [SerializeField] private float endFireballAngle = 0;
    [SerializeField] private Vector3 fireballDir;
    [SerializeField] private float delayTime;
    [SerializeField] private float timer;
    [SerializeField] private int waveCounter;
    [SerializeField] private bool isBurst = false;
    [SerializeField] private float fireballDelayTime;

    public override void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Keypad0))
        //{
        //    Fly();
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad1))
        //{
        //    Spreadshot();
        //}
        //if (Input.GetKeyDown(KeyCode.Keypad2))
        //{
        //    if (!isBurst)
        //        Fireball();
        //}

        if (currentBehavior == behaviorState.FLY || currentBehavior == behaviorState.INIT)
        {
            if (transform.position.x <= moveDir[(int)destination].position.x + offset && transform.position.x >= moveDir[(int)destination].position.x - offset)
            {
                //Debug.Log("Destination : " + moveDir[(int)destination]);
                rb.velocity = Vector2.zero * 0;
                currentDir = destination;
                Spreadshot();
            }
        }
        CheckDeath();
    }

    private void Fly()
    {
        destination = currentDir + 1;
        if (currentDir >= moveDir.Count - 1)
        {
            currentDir = 0;
        }
        if (destination >= moveDir.Count)
        {
            destination = 0;
        }
        currentBehavior = behaviorState.FLY;
        if (transform.position != moveDir[(int)destination].position)
        {
            Vector3 dir = (moveDir[(int)destination].position - transform.position).normalized;
            rb.velocity = dir * speed;
            //Debug.Log(rb.velocity);
        }
    }

    private void Spreadshot()
    {
        //Debug.Log("Spreadshot was called");
        currentBehavior = behaviorState.SHOOTSPREADSHOT;
        rb.velocity = Vector2.zero;
        SpreadSolution1();
        animator.SetTrigger("Attack1");
    }

    private void Fireball()
    {
        Debug.Log("Fireball was called");
        currentBehavior = behaviorState.SHOOTFIREBALL;
        rb.velocity = Vector2.zero;
        FireballSolution2();
        animator.SetTrigger("Attack2");
    }

    private void SpreadSolution1()
    {
        float angleStep = (endSpreadAngle - startSpreadAngle) / bulletSpreadAmount;
        float angle = startSpreadAngle;

        for (int i = 0; i < bulletSpreadAmount + 1; i++)
        {
            float bulletDirX = bulletSpawnPos.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulletDirY = bulletSpawnPos.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 spreadVector = new Vector2(bulletDirX, bulletDirY);
            Vector3 spreadBulletDir = (spreadVector - bulletSpawnPos.position).normalized;

            //Debug.Log("Bullet Dir : " + spreadBulletDir);
            Debug.Log(angle);
            GameObject bullet = Instantiate(spreadPrefab, bulletSpawnPos.position, Quaternion.Euler(0,0,angle + 360));
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().velocity = spreadBulletDir * bulletSpeed_Spread;

            angle += angleStep;
        }
        Invoke("Fireball", fireballDelayTime);
        //Fireball();
    }
    private void SpreadSolution2()
    {
        float angleStep = (endSpreadAngle - startSpreadAngle) / bulletSpreadAmount;
        float angle = startSpreadAngle;

        for (int i = 0; i < bulletSpreadAmount - 1; i++)
        {
            float dirX = bulletSpawnPos.position.x + Mathf.Sin((endSpreadAngle * Mathf.PI) / 180);
            float dirY = bulletSpawnPos.position.y + Mathf.Cos((endSpreadAngle * Mathf.PI) / 180);

            Vector3 bulletVector = new Vector3(dirX, dirY, 0);
            Vector2 bulletDir = (bulletVector - bulletSpawnPos.position).normalized;


            GameObject bullet = Instantiate(spreadPrefab, bulletSpawnPos.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDir * speed * Time.deltaTime;

            angle += angleStep;
        }
    }
    private void FireballSolution1()
    {
        for (int j = 0; j < bulletWave; j++)
        {
            float angleStep = (endFireballAngle - startFireBallAngle) / bulletFireballAmount;
            float angle = startFireBallAngle;

            int count = 0;
            for (int i = 0; i < bulletFireballAmount; i++)
            {
                float dirX = bulletSpawnPos.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
                float dirY = bulletSpawnPos.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

                Vector3 bulletVector = new Vector3(dirX, dirY, 0);
                Vector2 bulletDir = (bulletVector - bulletSpawnPos.position).normalized;


                GameObject bullet = Instantiate(fireballPrefab, bulletSpawnPos.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDir * speed * Time.deltaTime;

                angle += angleStep;
                count++;
                //StartCoroutine(DelayFireball(i, bulletFireballAmount));
            }
            Debug.Log("Count : " + count);
            Debug.Log("angle : " + angle);
        }
    }

    private void FireballSolution2()
    {
        StartCoroutine(BurstFire(delayTime, bulletWave));
    }

    private void HandleSpawnFireball()
    {
        float angleStep = (endFireballAngle - startFireBallAngle) / bulletFireballAmount;
        float angle = startFireBallAngle;

        for (int i = 0; i < bulletFireballAmount; i++)
        {
            float dirX = bulletSpawnPos.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float dirY = bulletSpawnPos.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector3 bulletVector = new Vector3(dirX, dirY, 0);
            Vector2 bulletDir = (bulletVector - bulletSpawnPos.position).normalized;


            GameObject bullet = Instantiate(fireballPrefab, bulletSpawnPos.position, Quaternion.identity);
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDir * bulletSpeed_Fireball;

            angle += angleStep;
        }
        timer = delayTime;
    }

    private IEnumerator BurstFire(float delay, int loop)
    {
        for (int i = 0; i < loop; i++)
        {
            HandleSpawnFireball();
            yield return new WaitForSeconds(delay);
        }
        isBurst = true;
        Fly();
    }

    private void OnDisable()
    {
        currentBehavior = behaviorState.INIT;
        destination = 0;
    }

    public override void OnEnable()
    {
        base.Start();
        SetHealth();
        Application.targetFrameRate = 45;
        // fly to destination 1
        Vector3 dir = (moveDir[0].transform.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            TakeDamage((int)collision.GetComponent<ArmamentControl>().damage);
            Debug.Log("Collide with bullet");
        }
    }

}
