using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {
    //gravity variables//
	public float gravityModifier = 1f;
	//collision//
	public float minGroundNormalY = .65f;

    //gravity//
	protected Rigidbody2D rb2d;
	protected Vector2 velocity;

	//collision variables//
	protected const float minMoveDistance = 0.001f;
	protected const float shellRadius = 0.01f;

	protected Vector2 targetVelocity;
	protected bool grounded;
	protected Vector2 groundNormal;
	protected ContactFilter2D contactFilter;
	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
   

    //gravity thing//
	private void OnEnable()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () 
	{
		//collision thing//
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
		contactFilter.useLayerMask = true; 

	}
	
	// Update is called once per frame
	void Update () 
	{
		targetVelocity = Vector2.zero;
		ComputeVelocity ();
	}

	protected virtual void ComputeVelocity ()
	{
		
	}

    //gravity thing//
	private void FixedUpdate()
	{
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

		//collision//
		grounded = false;

		Vector2 deltaPosition = velocity * Time.deltaTime;

		Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

		Vector2 move = moveAlongGround * deltaPosition.x;
             
		Movement(move, false);

		move = Vector2.up * deltaPosition.y;
        
		Movement (move, true);
	}
    //gravity thing//
	void Movement(Vector2 move, bool yMovement)
	{
		//collision shit//
		float distance = move.magnitude; 

		if (distance > minMoveDistance)
		{
			int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
			hitBufferList.Clear();
			for (int i = 0; i < count; i++) {
				hitBufferList.Add(hitBuffer[i]);
			}

			for (int i = 0; i < hitBufferList.Count; i++)
			{
				//will not allow character to slide down slope//
				Vector2 currentNormal = hitBufferList[i].normal;
				if (currentNormal.y > minGroundNormalY)
				{
					grounded = true;
					if (yMovement)
					{
						groundNormal = currentNormal;
						currentNormal.x = 0;
					}
				}

				float projection = Vector2.Dot(velocity, currentNormal);
				if (projection < 0)
				{
					velocity = velocity - projection * currentNormal;
				}

				float modifiedDistance = hitBufferList[i].distance - shellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance;
			}


		}

        //gravity//
		rb2d.position = rb2d.position + move.normalized * distance;
	}
}
