using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantVelocity : MonoBehaviour {

	public Vector2 velocity = Vector2.zero;//property for storing the velocity

	private Rigidbody2D body2d;//reference to the rigidbody of the object.

	void Awake()
	{
		// to set the body2d reference
		body2d = GetComponent <Rigidbody2D> ();
	}


	void FixedUpdate () 
	{
		body2d.velocity = velocity;
	}
}
