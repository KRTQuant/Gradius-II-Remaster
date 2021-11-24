using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dee_Enemy : UnitAbs
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject player;
    [SerializeField] private Text text;
    [SerializeField] private float gunCooldown = 1.5f;
    [SerializeField] private float fireRate;
    [SerializeField] private float speed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject barrel;


    private void FixedUpdate()
    {
        Solution2();
        //Test();
    }

    private void Solution1()
    {
        float angle = Vector3.Angle(this.gameObject.transform.position, player.transform.position);
        Debug.Log(angle);
    }

    private void Solution2()
    {
        Vector2 dir = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dir, Color.green);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log((int)angle);

        //transform.rotation = Quaternion.Slerp(transform.rotation, angleAxis, Time.deltaTime * 50);
        if (angle > 144 && angle <= 180)
        {
            text.text = "144 - 180";
            Quaternion rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = rotation;
            Shoot();
        }
        if (angle > 108 && angle <= 144)
        {
            text.text = "108 - 144";
            Quaternion rotation = Quaternion.Euler(0, 0, -45);
            transform.rotation = rotation;
            Shoot();
        }
        if (angle > 72 && angle <= 108)
        {
            text.text = "72 - 108";
            Quaternion rotation = Quaternion.Euler(0, 0, -90);
            transform.rotation = rotation;
            Shoot();
        }
        if (angle > 36 && angle <= 72)
        {
            text.text = "36 - 72";
            Quaternion rotation = Quaternion.Euler(0, 0, -135);
            transform.rotation = rotation;
            Shoot();
        }
        if (angle > 0 && angle <= 36)
        {
            text.text = "0 - 36";
            Quaternion rotation = Quaternion.Euler(0, 0, -180);
            transform.rotation = rotation;
            Shoot();
        }

    }

    private void Shoot()
    {
        if(isStart)
        {
            if (fireRate <= 0)
            {
                GameObject bullet = Instantiate<GameObject>(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = -transform.right * speed;
            }
            else
            {
                fireRate -= Time.deltaTime;
            }
        }
    }

    private void Test()
    {
        Vector2 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        Debug.Log((int)angle);
        Debug.Log(Mathf.Sin(45 * Mathf.Deg2Rad));
    }
}
