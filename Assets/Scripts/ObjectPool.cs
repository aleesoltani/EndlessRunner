using System.Collections;
using System.Collections.Generic;//used for lists and dictionaries
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	//reference to the prefab that this will manage
	public RecycleGameObject prefab;
	//we need a list ( so that it has a dynamic size) of instances
	private List<RecycleGameObject> poolInstances = new List<RecycleGameObject>();

	//create an instance that we need
	//we need to use the original engine's instantiate, because if the objectpool
	//asks the gameObjectUtil to create it for him, and the Util asks the objectPool
	//to create it for him, we would have an infinite loop.

	private RecycleGameObject CreateInstance(Vector3 pos)
	{
		// we need a clone of the prefab that the object pool manages
		var clone = GameObject.Instantiate (prefab);
		clone.transform.position = pos;// to set spawn position
		clone.transform.parent = transform; // to nest it into the objectpool gameobject in the hierarchy view 

		poolInstances.Add (clone);
		return clone;
	}
	public  RecycleGameObject NextObject(Vector3 pos)
	{
		RecycleGameObject instance = null;

		foreach(var go in poolInstances)
		{
			if(go.gameObject.activeSelf != true)//because go is RecycleGameObject we need to access its GameObject
			{
				instance = go;
				instance.transform.position = pos;
			}
		}
		if (instance == null) 
		{
			instance = CreateInstance (pos);
		}
		instance.Restart ();
	
		return instance;
	}

}
