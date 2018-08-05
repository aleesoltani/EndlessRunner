using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//we need an interface so that a class that uses it will always have the same type of public methods
using System;

public interface IRecycle
{
	void Restart();
	void Shutdown();

	//we need to find a way for irecycle to loop through other children scripts
	//on the gameObject it's attached to and see if they implement this interface
	//to do this we need to keep reference of all the gameObject scripts that have the interface attached to it.


}

public class RecycleGameObject : MonoBehaviour 
{
	private List<IRecycle> recycleComponents;

	void Awake()
	{
		var components = GetComponents <MonoBehaviour>();
		recycleComponents = new List<IRecycle> ();

		//loop through all the components we found to see if they implement IRecycle
		foreach(var component in components)
		{
			if(component is IRecycle)
			{
				recycleComponents.Add (component as IRecycle); 
			}
		}
	}


	//to re-instantite an object
	public void Restart()
	{
		gameObject.SetActive (true); // gameObject is the reference to the object that the script is attached to.
		foreach ( var component in recycleComponents)
		{
			component.Restart ();
		}
	}
	//to delete an object
	public void Shutdown()
	{
		gameObject.SetActive (false);
		foreach ( var component in recycleComponents)
		{
			component.Shutdown ();
		}
	}
}
