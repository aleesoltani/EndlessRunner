using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security;
using System;

public class TimeManager : MonoBehaviour 
{
	/// <summary>
	/// /*Manipulates the time.*/
	/// </summary>
	/// <param name="newTime">New time.</param>
	/// <param name="duration">Duration. How long the transition should take from the current time to the new time.</param>
	public void ManipulateTime(float newTime, float duration)
	{
		if(Time.timeScale==0)
		{
			Time.timeScale = 0.1f;
		}
		StartCoroutine (FadeTo (newTime, duration));
	}
	/// <summary>
	/// /*the method we will be calling as the coroutine , to do the fading down of time.*/
	/// </summary>
	/// <returns>The to.</returns>
	/// <param name="value">Value.</param>
	/// <param name="time">Time.</param>
	IEnumerator FadeTo(float value, float time)
	{
		for (float t= 0f; t<1; t+=Time.deltaTime/time)
		{
			//to calculate what the timeScale should be, in the current iteration of the loop
			Time.timeScale = Mathf.Lerp (Time.timeScale,value,t);
			//lerp allows us to alter a value from start to end over a period of time.
			if(Mathf.Abs(value-Time.timeScale) < 0.01f)
			{
				Time.timeScale = value;
				yield break;
			}//if time is close enough to zero, we'll just set it to zero

			//finally, while this is executing, in order to get this to execute over
			//multiple frames, we're gonna have to return null through each iteration.
			yield return null;
		}
	}

}
