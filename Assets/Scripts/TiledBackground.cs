using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This Script works best with textures that are powers of two. 16,32,64,128,..
// So if you changed the background to your own texture, try to keep it within power of two.
using System;


public class TiledBackground : MonoBehaviour 
{
	public int textureSize = 32;// represents the native texture size

	//we need to be able to scale this horizontaly or verticaly so that we are able to use the script 
	//for other repeatable textures like floor, so we need two other variables
	public bool scaleHorizontally = true;
	public bool scaleVertically = true;


	void Start () 
	{
		//we are gonna try to calculate how many tiles will fit inside the screen
		//so we are gonna use Ceiling function to round width up so we wont have less texture than the resolution
		var newWidth = 1f;
		var newHeight = 1f;
		if(scaleHorizontally)
			newWidth =  Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));//calculate a new width and height
		else 
			newWidth = 1f;
		
		if (scaleVertically)
			newHeight = Mathf.Ceil (Screen.height / (textureSize * PixelPerfectCamera.scale));
		else
			newHeight = 1f;
		// now we are gonna change the scale of the Quad
		// based on this new width and height
		transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1); 
		//because at core unity is a 3d engine,things like scale,position need Vector3 *important
		// and if we set the Z value of localScale to 0, The image would disappear.


		// now we need to tell the material that it has a new scale
		GetComponent <Renderer>().material.mainTextureScale = new Vector3 (newWidth,newHeight,1);// to change the main texture scale
		//no need to calculate the full size again.

	}

}
