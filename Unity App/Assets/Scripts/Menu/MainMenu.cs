﻿using UnityEngine;
using System.Collections;


public class MainMenu : MenuBase {
	
	protected Rect playRect;
	protected Rect settingsRect;
	protected Rect exitRect;
	
	
	// Use this for initialization
	new void Start () {	
		base.Start();
		
		playRect = new Rect(this.screenWidth / 7, (float)(this.screenHeight / 3.5),(float)(this.screenWidth / 3.5), this.screenHeight / 6);
		settingsRect = new Rect(this.screenWidth / 7, (float)(this.screenHeight / 2.14),(float)(this.screenWidth / 3.5), this.screenHeight / 6);
		exitRect = new Rect(this.screenWidth / 7, (float)(this.screenHeight / 1.55),(float)(this.screenWidth / 3.5), this.screenHeight / 6);
	}
	
	// Update is called once per frame
	void Update () {
		//kinect management
		if(this.clickEnabled && this.kinectController.getRightHand().z > 0.5f)
		{
			if(this.playRect.Contains(getHandPos()))
			{		
				Application.LoadLevel("LevelSelection");	
			}
			else
				if(this.settingsRect.Contains(getHandPos()))
				{		
					Application.LoadLevel("SettingsMenu");	
				}
				else
					if(this.settingsRect.Contains(getHandPos()))
					{		
						Application.Quit();			
					}
		}
	}
	
	void OnGUI() {		
		GUI.skin = this.menuSkin;
		
		//mouse management
		if(GUI.Button(playRect, "PLAY"))
			Application.LoadLevel("LevelSelection");
		else  
			if(GUI.Button(settingsRect,"SETTINGS"))
				Application.LoadLevel("SettingsMenu");		
		else  
			if(GUI.Button(exitRect,"EXIT"))
				Application.Quit();
		
		GUI.Label(new Rect(this.screenWidth / 7, (float)(this.screenHeight / 1.2),(float)(this.screenWidth / 1.5), this.screenHeight / 6), "Play and play it again !");
		GUI.Box (new Rect(this.screenWidth / 10,this.screenHeight / 7, (float)(this.screenWidth / 2.6), (float)(this.screenHeight / 1.36)), "MENU");
	}
	
}

