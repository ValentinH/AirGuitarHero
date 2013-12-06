using UnityEngine;
using System.Collections;


public class Calibrate : MenuBase {
	
	
	protected Rect upRect;
	protected Rect downRect;
	protected Rect backRect;
	
	// Use this for initialization
	new void Start () {
		base.Start();
		
		backRect = new Rect(this.screenWidth / 2.7f, this.screenHeight / 2.3f, this.screenWidth / 4f, this.screenHeight / 8f);
		upRect = new Rect(this.screenWidth / 2.3f, this.screenHeight / 9f, this.screenWidth / 7f, this.screenHeight / 8f);
		downRect = new Rect(this.screenWidth / 2.3f, this.screenHeight / 1.3f, this.screenWidth / 7f, this.screenHeight / 8f);
	}
	
	// Update is called once per frame
	void Update () {
		//kinect management
		if(this.clickEnabled && this.kinectController.getRightHand().z > 0.5f)
		{
			if(this.upRect.Contains(getHandPos()))
			{
				this.kinectController.moveKinect(0.2f);	
			}
			else
				if(this.downRect.Contains(getHandPos()))
				{		
					this.kinectController.moveKinect(-0.2f);
				}
				else
					if(this.backRect.Contains(getHandPos()))
					{		
						Application.LoadLevel("SettingsMenu");			
					}
		}
	}
	
	void OnGUI() {		
		GUI.skin = menuSkin;
		
		//Buttons
		//GUI.Label (new Rect(this.screenWidth / x, this.screenHeight / y, this.screenWidth / 3.5f, this.screenHeight / 8), "UTC");
	
		//Back to settings
		if(GUI.Button(backRect,"Back")){
			Application.LoadLevel("SettingsMenu");
		}
		else if(GUI.Button(upRect,"UP")){
			this.kinectController.moveKinect(0.2f);
		}
		else if(GUI.Button(downRect,"DOWN")){
			this.kinectController.moveKinect(-0.2f);
		}
		
		//GUI.Label(new Rect(this.screenWidth / 7, (float)(this.screenHeight / 1.2),(float)(this.screenWidth / 1.5), this.screenHeight / 6), "Play and play it again !");
		GUI.Box (new Rect(0, 0, (float)(this.screenWidth), (float)(this.screenHeight)), "Calibrate");
			
	}
}
