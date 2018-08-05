using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour , IRecycle 
{
	//to change the artwork
	public Sprite[] sprites;
	//to fix vertical collider for different tall objects
	public Vector2 colliderOffset = Vector2.zero;

	public void Restart()
	{
		//get a reference to the sprite renderer and pick a random sprite to display
		var renderer = GetComponent <SpriteRenderer>();
		renderer.sprite = sprites [Random.Range (0, sprites.Length)];

		//logic to resize the collider
		var collider = GetComponent <BoxCollider2D> ();
		var size = renderer.bounds.size;
		// we need to recenter the box collider
		size.y += colliderOffset.y;
		collider.size = size;

		collider.offset = new Vector2 (-colliderOffset.x, collider.size.y / 2 - colliderOffset.y);
	}
	public void Shutdown()
	{
		
	}
}
