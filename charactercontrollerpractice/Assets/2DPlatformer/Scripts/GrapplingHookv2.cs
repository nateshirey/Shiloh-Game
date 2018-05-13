using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookv2 : MonoBehaviour
{

	public GameObject hook;

	private bool isHooked;
	private bool inRange;

	private Vector3 target;
	private Vector3 hookLocation;
	public float hookSpeed;

	// Use this for initialization
	void Start()
	{

		inRange = false;

	}

	// Update is called once per frame
	void Update()
	{

		hookLocation = hook.transform.position;

		if (Input.GetKey("e") && inRange == true)
		{

			hook.transform.position = Vector2.MoveTowards(hookLocation, target, hookSpeed * Time.deltaTime);
			Debug.Log(hookLocation);

		}


	}
 
	void sendHook()
	{

			
	}

	void OnTriggerStay2D(Collider2D col)
	{
        
		if(col.gameObject.tag == "Grapple Point")
		{

			target = col.gameObject.transform.position;
			inRange = true;
			Debug.Log("in range");

		}

	}

}
