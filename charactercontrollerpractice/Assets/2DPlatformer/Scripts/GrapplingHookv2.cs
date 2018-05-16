using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookv2 : MonoBehaviour
{

	public GameObject hook;

	private bool isHooked;
	private bool inRange;

	private Vector3 target;
    private Rigidbody2D targetRb2d;
	private Vector3 hookLocation;
	public float hookSpeed;

    private DistanceJoint2D joint;

	// Use this for initialization
	void Start()
	{
		inRange = false;
        joint = this.GetComponent<DistanceJoint2D>();
        joint.enabled = false;

	}

	// Update is called once per frame
	void Update()
	{
		hookLocation = hook.transform.position;
        joint.enabled = false;

        if (Input.GetKey("e") && inRange == true)
        {
            joint.connectedBody = targetRb2d;
            joint.connectedAnchor = target;
            joint.enabled = true;
            sendHook();
        }
        else if (hookLocation != this.transform.position)
        {
            returnHook();
        }
	}
 
	void sendHook()
	{
           hook.transform.position = Vector2.MoveTowards(hookLocation, target, hookSpeed * Time.deltaTime);
           Debug.Log(hookLocation);
	}

    void returnHook()
    {
        hook.transform.position = Vector2.MoveTowards(hookLocation, this.transform.position, hookSpeed * Time.deltaTime);
    }

	void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.tag == "Grapple Point")
		{
			target = col.gameObject.transform.position;
            targetRb2d = col.GetComponent<Rigidbody2D>();
            inRange = true;
			Debug.Log("in range");
		}
	}
}
