using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour 
{
	//get a reference to the animator instance
	private Animator animator;
	private InputState inputState;

	void Awake()
	{
		animator = GetComponent <Animator> ();
		inputState = GetComponent <InputState> ();
	}
	void Update () 
	{
		//we are always gonna assume that the player is running
		var running = true;// default state of the player
		if( inputState.absVelX > 0 && inputState.absVelY < inputState.standingThreshold)
		{
			running = false;
		}
		animator.SetBool ("Running", running);
	}
}
