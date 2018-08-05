using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//points:
//1.to reposition the floor to the bottom of the screen no matter what the resolution is
//2.get a reference to the spawner so that we can turn it off when the game first begins
using System.Configuration;

public class GameManager : MonoBehaviour 
{
	private bool gameStarted;//flag to know when the game is started or is over
	public Text continueText;//the text shown in the middle of the screen
	public Text scoreText;//score and best time

	private float timeElapsed = 0f;
	private float bestTime = 0f;
	private bool beatBestTime;

	private float blinkTime = 0f;//to manaage the blink time
	private bool blink;// to blink or not

	private TimeManager timeManager;
	//a reference to the player prefab so we can create it when we reset our game
	public GameObject playerPrefab;
	//a reference to store the player's instance
	private GameObject player;
	private GameObject floor;
	private Spawner spawner;

	void Awake ()
	{
		floor = GameObject.Find ("Foreground");
		spawner = GameObject.Find ("Spawner").GetComponent <Spawner> ();//because spawner is a script. we also have a gameobj named spawner.
		timeManager = GetComponent<TimeManager>();
	}

	//get the height of the floor gameobject in the start method
	void Start () 
	{
		var floorHeight = floor.transform.localScale.y;
		var pos = floor.transform.position;

		pos.x = 0;
		//we're gonna wanna calculate where the bottom of the screen is and 
		//in this case we need to take the full height of the screen and divide it
		//by 2. and then once we figure out the height from the center of the screen
		//to the bottom,we'll then offset that by the height of the floor 
		//(half of it because we want the floor to be centered along the bottom of the screen.
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsPerUnits) / 2);
		//now that is gonna get us to the bottom of the screen from the center point.

		//now we'll add half the floor's height to move it back up and centered along the bottom of the screen
		pos.y += floorHeight/2;

		//now set it
		floor.transform.position = pos;
		spawner.active = false;

		Time.timeScale = 0;
		if(Application.platform == RuntimePlatform.Android)
		{
			continueText.text = "Touch Anywhere To Start!";
		}
		else
		{
			continueText.text = "Press Any Button To Start!";
		}

		bestTime = PlayerPrefs.GetFloat ("BestTime");
	}
	void Update () 
	{
		if (!gameStarted && Time.timeScale == 0)
		{
			if(Input.anyKeyDown)
			{
				timeManager.ManipulateTime (1, 1f);
				ResetGame ();
			}
		}
		//to make the text blink we need time difference but when the game is stopped , it is 0.
		//so we are going to do sth else. first we should make sure the game is paused.
		if(!gameStarted)
		{
			blinkTime++;
			if(blinkTime % 28 == 0) // increase for more delay, decrease for less delay between blinks.
			{
				blink = !blink;
			}
			continueText.canvasRenderer.SetAlpha (blink ? 0 : 1);
			//to change text color if new highscore beaten
			var textColor = beatBestTime ? "#FF0" : "#9A1515";
			scoreText.text = "TIME: " + FormatTime (timeElapsed) + "\n<color="+textColor+">BEST: " + FormatTime (bestTime)+"</color>";
		}
		else
		{
			timeElapsed += Time.deltaTime;
			scoreText.text = "TIME: " + FormatTime (timeElapsed);
		}

	}
	//the method that gets called when our player gets killed
	void OnPlayerKilled()
	{
		spawner.active=false;
		var playerDestroyScript = player.GetComponent <DestroyOffScreen>();
		playerDestroyScript.DestroyCallBack -= OnPlayerKilled;
		//we should also reset the velocity of the player
		player.GetComponent <Rigidbody2D>().velocity= Vector2.zero;
		//manipulate time down to zero over a few seconds
		timeManager.ManipulateTime (0,5.5f);
		gameStarted = false;
		if(Application.platform == RuntimePlatform.Android)
		{
			continueText.text = "Touch Anywhere To Restart!";
		}
		else
		{
			continueText.text = "Press Any Button To Restart!";
		}

		if(timeElapsed > bestTime)
		{
			beatBestTime = true;
			bestTime = timeElapsed;
			//this class "PlayerPrefs" allows us to save values into unity similar to how cookies work in a web browser
			PlayerPrefs.SetFloat ("BestTime", bestTime);
		}
	}

	void ResetGame()
	{
		spawner.active = true;
		player = GameObjectUtil.Instantiate (playerPrefab, 
												new Vector3 (0, (Screen.height / PixelPerfectCamera.pixelsPerUnits) / 2 + 100, 0));

		//we need a reference to destroyOffScreen in order to connect the delegate
		var playerDestroyScript = player.GetComponent <DestroyOffScreen>();
		//the next line does this : 
		//it tells the playerDestroyScript that when DestroyCallBack gets called
		//it's actually going to call OnPlayerKilled inside our game manager.
		playerDestroyScript.DestroyCallBack += OnPlayerKilled;

		gameStarted = true;
		continueText.canvasRenderer.SetAlpha (0);//this will hide the text, when the game starts.
		timeElapsed = 0f;
		beatBestTime = false;
	}
	string FormatTime(float timeValue)
	{
		//using System
		TimeSpan t = TimeSpan.FromSeconds (timeValue);
		return string.Format ("{0:D2}:{1:D2}",t.Minutes,t.Seconds);
	}
}
