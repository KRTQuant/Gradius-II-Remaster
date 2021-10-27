using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SkillManager skillManager;

    [Header("Velocity")]
    [SerializeField] public float constantSpeed;
    [SerializeField] public float addonSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        skillManager = GetComponent<SkillManager>();
    }

    private void FixedUpdate()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKey(KeyCode.UpArrow)))
        {
            rb.velocity = new Vector2(rb.velocity.x, SetVelocity());
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKey(KeyCode.DownArrow)))
        {
            rb.velocity = new Vector2(rb.velocity.x, -SetVelocity());
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            rb.velocity = new Vector2(-SetVelocity(), rb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKey(KeyCode.RightArrow)))
        {
            rb.velocity = new Vector2(SetVelocity(), rb.velocity.y);
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            skillManager.HandleUpgradeSkill();
        }
    }

    public float SetVelocity()
    {
        float velocity = ((skillManager.speedLevel + 1) * addonSpeed) + constantSpeed;
        return velocity;
    }
}
