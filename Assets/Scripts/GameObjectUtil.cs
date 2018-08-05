using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//because the utility is gonna be accessible through out the game we remove monobehaviour inheritance
public class GameObjectUtil 
{
	private static Dictionary<RecycleGameObject , ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool>(); //static because the class and its members are static

	//we are gonna create static methods for other classes to access the methods in this class.
	//parameters: what prefab and where to instantiate it
	public static GameObject Instantiate(GameObject prefab, Vector3 pos)
	{
		GameObject instance = null;

		// to make sure that the prefab is actually a recyclegameobject
		var recycledScript = prefab.GetComponent <RecycleGameObject> ();
		if(recycledScript != null)
		{
			var pool = GetObjectPool (recycledScript);
			instance = pool.NextObject(pos).gameObject; 
		}
		else 
		{

			instance = GameObject.Instantiate (prefab);
			instance.transform.position = pos;
		}
			
		return instance;
	}
	public static void Destroy(GameObject gameObject)
	{
		// test if the game object has the recycle script attached to it.
		var recycleGameObject = gameObject.GetComponent <RecycleGameObject> ();
		if ( recycleGameObject!= null)
		{
			recycleGameObject.Shutdown ();
		}
		else 
		{
			GameObject.Destroy (gameObject);	
		}
	}

	// a method that'll return us the instance of the pool based on the gameObject we're requesting
	private static ObjectPool GetObjectPool(RecycleGameObject reference)
	{
		ObjectPool pool = null; // the object that we are going to return

		if (pools.ContainsKey (reference) )//making sure that the key exists
		{
			pool = pools [reference];
		}
		else 
		{
			//if we don't have the objectPool, we need to create it.
			var poolContainer = new GameObject (reference.gameObject.name + "ObjectPool");
			pool = poolContainer.AddComponent <ObjectPool> ();//adding the objectpool script to the new poolContainer and setting it to the pool value
			pool.prefab=reference;//so that the pool knows which type of object to create
			pools.Add (reference,pool);//adding the new pool to the dictionary

		}
		return pool;
	}
}
