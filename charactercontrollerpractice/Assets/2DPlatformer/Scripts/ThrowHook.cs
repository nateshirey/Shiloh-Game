﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour {


	public GameObject hook;

	GameObject curHook;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0))
		{
			Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);


			curHook = (GameObject)Instantiate(hook, transform.position, Quaternion.identity);

			//curHook.GetComponent<Rope>().destiny = destiny;
			curHook.GetComponent<Rope>();

			//curHook.destiny = destiny;
			Debug.Log(curHook);
		}

        
	}
}
