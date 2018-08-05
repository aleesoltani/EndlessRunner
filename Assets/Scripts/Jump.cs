using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour 
{

	public float jumpSpeed = 240f;
	public float forwardSpeed = 20f; //to make sure the player moves if he lands on an object
	private Rigidbody2D body2d;//reference to the rigidbody2d for velocity and stuff
	private InputState inputState; //to connect this class to the inputState so that we know what the player is doing.


	void Awake() 
	{
		body2d = GetComponent <Rigidbody2D> ();
		inputState = GetComponent <InputState> ();
	}

	void Update () 
	{
		//test if the player is standing 
		if(inputState.standing)
		{
			if(inputState.actionButton)
			{
				body2d.velocity = new Vector2 (transform.position.x <0 ? forwardSpeed : 0, jumpSpeed);
			}
		}
	}

}
