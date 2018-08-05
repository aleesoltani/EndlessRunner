using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// to animate materials like the background
public class AnimatedTexture : MonoBehaviour {

	public Vector2 speed = Vector2.zero;// The Speed that our Texture would be animated in
	private Vector2 offset = Vector2.zero; //to keep track of the default offset position of the texture
	private Material material;// a reference to the moving material

	// Use this for initialization
	void Start () 
	{
		//get a reference to the material that is attached to the quad
		material = GetComponent <Renderer>().material;
		offset = material.GetTextureOffset ("_MainTex"); //point the offset to the material's own offset

	}
	
	// Update is called once per frame
	// we will increment the offset and re-apply it to the material in the update function
	void Update () 
	{
		offset += speed * Time.deltaTime; //deltaTime represents the difference in time between one frame and the next
		// because FPS may not be locked, we need to reference time.deltaTime in the calculation above.
		// so no matter what the frame rate is, our speed will be consistent.
		material.SetTextureOffset ("_MainTex",offset); 

	}
}
