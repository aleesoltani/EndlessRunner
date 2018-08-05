using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : MonoBehaviour {

	public bool actionButton; // to represent the action button.
	public float absVelX = 0f; // absolute velocity X value
	public float absVelY = 0f; // absolute velocity Y value
	public bool standing; //whether we are standing or not
	//to keep track of the threshold between when the velocity is slowing down and when we determine he is standing.
	public float standingThreshold = 1;

	private Rigidbody2D body2d;

	//use Awake to get a reference to the players rigidbody
	void Awake()
	{
		body2d = GetComponent <Rigidbody2D> ();
	}

	//we are gonna test whether any key is pressed and set it to the action button inside update
	void Update () 
	{
		actionButton = Input.anyKeyDown;// Sample any key pressed.
	}

	//to check whether the player is standing or not (physics calculations, SO we must use FixedUpdate() method
	void FixedUpdate()
	{
		absVelX = System.Math.Abs(body2d.velocity.x);
		absVelY = System.Math.Abs(body2d.velocity.y);

		//to test whether the player is standing or not
		standing = absVelY <= standingThreshold;
	}
}
