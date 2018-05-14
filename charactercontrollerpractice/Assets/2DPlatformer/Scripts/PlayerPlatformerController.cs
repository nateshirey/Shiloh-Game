﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

	public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

    private bool attatched;
    public float swingSpeed;

    //Grappling hook variables
    public GameObject hook;

    private bool isHooked;
    private bool inRange;

    private Vector3 target;
    private Vector3 hookLocation;
    public float hookSpeed;


	// Use this for initialization
	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
        inRange = false;
	}

	protected override void ComputeVelocity ()
	{
        //control player movement
        Vector2 move = Vector2.zero;


        move.x = Input.GetAxis("Horizontal");


		if (Input.GetButtonDown ("Jump") && grounded)
		{
			velocity.y = jumpTakeOffSpeed;
		}
		else if (Input.GetButtonUp ("Jump"))
		{
			if (velocity.y > 0)
				velocity.y = velocity.y * .5f;
		}

		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if (flipSprite) 
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetBool("grounded", grounded);
		animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

		targetVelocity = move * maxSpeed;

	}

    void sendHook()
    {
        hook.transform.position = Vector2.MoveTowards(hookLocation, target, hookSpeed * Time.deltaTime);
    }

    void returnHook()
    {
        hook.transform.position = Vector2.MoveTowards(hookLocation, this.transform.position, hookSpeed * Time.deltaTime);
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Grapple Point")
        {
            target = col.gameObject.transform.position;
            inRange = true;
        }
    }
}