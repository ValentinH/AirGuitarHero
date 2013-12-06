using UnityEngine;
using System.Collections;


public class LevelSelectionMenu : MenuBase {
		
	protected Rect l1Rect;
	protected Rect l2Rect;
	protected Rect l3Rect;
	protected Rect backRect;
	
	
	// Use this for initialization
	new void Start () {
		base.Start();
		
		l1Rect = new Rect(this.screenWidth / 7, (float)(this.screenHeight / 3.5),(float)(this.screenWidth / 3.5), this.screenHeight / 8);
		l2Rect = new Rect(this.screenWidth / 7, (float)(this.screenHeight / 2.38),(float)(this.screenWidth / 3.5), this.screenHeight / 8);
		l3Rect = new Rect(this.screenWidth / 7, (float)(this.screenHeight / 1.8),(float)(this.screenWidth / 3.5), this.screenHeight / 8);
		backRect = new Rect(this.screenWidth / 7, (float)(this.screenHeight / 1.44),(float)(this.screenWidth / 3.5), this.screenHeight / 8);
	}
	
	// Update is called once per frame
	void Update () {
		//kinect management
		if(this.clickEnabled && this.kinectController.getRightHand().z > 0.5f)
		{
			if(this.l1Rect.Contains(getHandPos()))
			{						
				Application.LoadLevel("Game");	
			}
			else if(this.l2Rect.Contains(getHandPos()))
			{		
				Application.LoadLevel("MainMenu");	
			}
			else if(this.l3Rect.Contains(getHandPos()))
			{		
				Application.LoadLevel("MainMenu");			
			}
			else if(this.l3Rect.Contains(getHandPos()))
			{		
				Application.LoadLevel("MainMenu");		
			}
		}
	}
	
	void OnGUI() {
		
		GUI.skin = menuSkin;
		
		if(GUI.Button(l1Rect, "LEVEL 1")){
			Application.LoadLevel("Game");
		}
		else if(GUI.Button(l2Rect,"LEVEL 2")){
			Application.LoadLevel("MainMenu");
		}
		else if(GUI.Button(l3Rect,"LEVEL 3")){
			Application.LoadLevel("MainMenu");
		}
		else if(GUI.Button(backRect,"BACK")){
			Application.LoadLevel("MainMenu");
		}
		
		GUI.Label(new Rect(this.screenWidth / 7, (float)(this.screenHeight / 1.2),(float)(this.screenWidth / 1.5), this.screenHeight / 6), "Play and play it again !");
		GUI.Box (new Rect(this.screenWidth / 10,this.screenHeight / 7, (float)(this.screenWidth / 2.6), (float)(this.screenHeight / 1.36)), "Select your level");
			
	}
}
