using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;// an array of objects that the spawner will spawn.
	public float delay = 2.0f;
	public bool active = true; // to find out if the spawner is active or not to shut it down after Game Over!
	public Vector2 delayRange = new Vector2(1,2); // to create random delays between spawns


	void Start () 
	{
		ResetDelay();
		StartCoroutine (EnemyGenerator());
	}

	IEnumerator EnemyGenerator() // in order to be used as a coroutine it must be of type IEnumerator
	{
		//we need a yield here to execute after our delay
		yield return new WaitForSeconds (delay);

		if(active) // if the spawner is active
		{
			var newTransform= transform; // represents the position that we are gonna spawn things at.connected to the existing transform of our gameobject

			//to create a new instance of our prefabs
			// the third parameter is the rotation
			GameObjectUtil.Instantiate (prefabs [Random.Range (0, prefabs.Length)], newTransform.position);

			ResetDelay();
		}

		StartCoroutine (EnemyGenerator ());// so that the coroutine gets started again after one has ended.
	}

	//method for reseting the delay everytime we spawn something new
	void ResetDelay()
	{
		delay = Random.Range (delayRange.x, delayRange.y);
	}

}
