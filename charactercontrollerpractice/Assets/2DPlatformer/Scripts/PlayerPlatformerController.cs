using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    [SerializeField] private GameObject character;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool attatched;
    public float swingSpeed;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        //control player movement
        Vector2 groundMove = Vector2.zero;


        groundMove.x = Input.GetAxis("Horizontal");


        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }

        bool flipSprite = (spriteRenderer.flipX ? (groundMove.x > 0.01f) : (groundMove.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = groundMove * maxSpeed;

    }

}