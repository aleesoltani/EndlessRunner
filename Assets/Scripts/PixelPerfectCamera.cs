using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {

	public static float pixelsPerUnits = 1f; // artwork's pixel per unit
	public static float scale = 1f; // camera scale
	public Vector2 nativeResolution = new Vector2 (240,160); // native resolution of the game

	void Awake () {
		var camera = GetComponent<Camera> ();//get a reference to the camera you are working with

		if (camera.orthographic) // check if the camera is set to 2d mode
		{
			scale = Screen.height / nativeResolution.y;
			// modify pixelsToUnits so that it relates to the scale in the game
			pixelsPerUnits*= scale; // can be used to check resolution - artwork isn't changed.
			camera.orthographicSize = (Screen.height/2.0f) / pixelsPerUnits; // to get the screen center we divide height by 2

		}

	}

}
