using UnityEngine;
using System.Collections;
using System;


public class ScoreMenu : MenuBase {
	
	protected Rect continueRect;
	
	
	public AudioClip music;
	
	public GUIText maxComboLabel, pointsLabel, notesLabel;
	
	
	// Use this for initialization
	new void Start () {		
		base.Start();
		
		MusicManager.StopMusic();
		MusicManager.SetMusic(music);
		MusicManager.PlayMusic();
		
		Vector2 middle = new Vector2(this.screenWidth / 2f, this.screenHeight / 2f);
		continueRect = new Rect((middle.x -  this.screenWidth /12f), (this.screenHeight/1.2f), this.screenWidth / 6f, this.screenHeight / 8f);
		
		//adjust label size to screen
		GameObject.FindGameObjectWithTag("ScoreTitle").guiText.fontSize = (int) Math.Round(Screen.width / 14d);
		pointsLabel.fontSize = (int) Math.Round(Screen.width / 28d);
		maxComboLabel.fontSize = (int) Math.Round(Screen.width / 28d);
		notesLabel.fontSize = (int) Math.Round(Screen.width / 28d);
		
		Hashtable h =  SceneManager.GetSceneArguments();
		if( h!=null)
		{
			int points = (int) h["points"];
			pointsLabel.text += " "+points;
			int maxCombo = (int) h["maxCombo"];
			maxComboLabel.text += " "+maxCombo;
			int notesTot = (int) h["notesTotales"];
			int notesReussies = (int) h["notesReussies"];
			float percent = (float) ((float)notesReussies) / ((float)notesTot) * 100f;
			float roundPercent = Mathf.Round(percent*10) /10;
			this.notesLabel.text += " "+notesReussies+" / "+notesTot+" => "+roundPercent+"%";
			
		}
	}
	
	// Update is called once per frame
	void Update () {
		//kinect management
		if(this.clickEnabled && (this.kinectController.getRightHand().z > KinectMenuController.CLICK_Z || (this.kinectController.getLeftHand().z > KinectMenuController.CLICK_Z)))
		{
			if(checkClick(this.continueRect))
			{		
				Application.LoadLevel("MainMenu");	
			}
		}
	}
	
	void OnGUI() {		
		GUI.skin = this.menuSkin;
		
		//mouse management
		if(GUI.Button(continueRect,"Continue")){
			Application.LoadLevel("MainMenu");
		}
	}
	
}

