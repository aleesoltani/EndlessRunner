using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DestroyOffScreen : MonoBehaviour 
{
	//two public propoerties for finding out the player is alive or not
	public delegate void OnDestroy();
	public event OnDestroy DestroyCallBack;//the actual event that we'll map this to

	public float offset = 16f;// represents the value of how far off the scene it needs to be to get destroyed.
	private bool offscreen;//to check whether the object is off screen, to be destroyed.
	private float offscreenX = 0f;
	private Rigidbody2D body2d;//we need to set the rigidbody in the awake method
	// we need body2d for velocity


	void Awake ()
	{
		body2d = GetComponent <Rigidbody2D> ();
	}
	//we need to figure out where the offscreenX value actually is.
	void Start () 
	{
		// this is gonna give us the actual width of the screen before we cut it in half
		// remember we have to re-adjust for the scale of our camera
		// to take into account how everything has been altered based on the different resolutions.
		offscreenX = (Screen.width / PixelPerfectCamera.pixelsPerUnits)
						/ 2 + offset;
		// meaning eventhough we find the halfway position of the screen, we're still gonna take little bit extra off in order
		// to account for how far off the object needs to be from the screen

	}

	//add the logic to calculate when the object is out of the screen.
	void Update () 
	{
		var posX = transform.position.x;//to store position x

		//to keep track of the actual velocity so that we know which direction the object is facing.
		var dirX = body2d.velocity.x;

		if ( Mathf.Abs (posX) > offscreenX) 
		{
			if (dirX < 0 && posX < -offscreenX)
			{
				offscreen = true;
			}
			else if (dirX > 0 && posX > offscreenX)
			{
				offscreen = true;
			}
		}
		else 
		{
			offscreen = false;
		}

		if(offscreen)
		{
			OnOutOfBounds();
		}
	}

	public void OnOutOfBounds()
	{
		offscreen = false;
		GameObjectUtil.Destroy(gameObject);

		if(DestroyCallBack != null)
		{
			DestroyCallBack ();
		}
	}
}
