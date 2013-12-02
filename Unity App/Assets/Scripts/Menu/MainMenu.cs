using UnityEngine;
using System.Collections;


public class MainMenu : MonoBehaviour {
	
	//Use the skin defined in the inspector
	public GUISkin menuSkin;
	public GUIText debug;
	
	protected KinectMenuController kinectController;
	
	protected Rect playRect;
	protected Rect settingsRect;
	protected Rect exitRect;
	
	protected float screenWidth, screenHeight;
	
	// Use this for initialization
	void Start () {		
		this.kinectController = (KinectMenuController) FindObjectOfType(typeof(KinectMenuController));
		
		screenWidth = Screen.width;
		screenHeight = Screen.height;
		playRect = new Rect(screenWidth / 7, (float)(screenHeight / 3.5),(float)(screenWidth / 3.5), screenHeight / 6);
		settingsRect = new Rect(screenWidth / 7, (float)(screenHeight / 2.14),(float)(screenWidth / 3.5), screenHeight / 6);
		exitRect = new Rect(screenWidth / 7, (float)(screenHeight / 1.55),(float)(screenWidth / 3.5), screenHeight / 6);
	}
	
	// Update is called once per frame
	void Update () {
		if(kinectController.getRightHand().z > 0.5f)
		{
			if(playRect.Contains(getHandPos()))
			{		
				Application.LoadLevel("LevelSelection");	
			}
			else
				if(settingsRect.Contains(getHandPos()))
				{		
					Application.LoadLevel("SettingsMenu");	
				}
				else
					if(settingsRect.Contains(getHandPos()))
					{		
						Application.Quit();			
					}
		}
	}
	
	void OnGUI() {
		float width = Screen.width;
		float height = Screen.height;
		
		GUI.skin = menuSkin;
		
		GUI.Button(playRect, "PLAY");		
		GUI.Button(settingsRect,"SETTINGS");		
		GUI.Button(exitRect,"EXIT");
		
		GUI.Label(new Rect(width / 7, (float)(height / 1.2),(float)(width / 1.5), height / 6), "Play and play it again !");
		GUI.Box (new Rect(width / 10,height / 7, (float)(width / 2.6), (float)(height / 1.36)), "MENU");
	}
	
	protected Vector2 getHandPos()
	{
		Vector2 hand = kinectController.getRightHand();
		hand.x *= screenWidth;
		hand.y = 1 - hand.y;
		hand.y *= screenHeight;
		return hand;		
	}
}

