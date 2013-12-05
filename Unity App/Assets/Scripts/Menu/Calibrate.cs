using UnityEngine;
using System.Collections;


public class Calibrate : MonoBehaviour {
	
	//Use the skin defined in the inspector
	public GUISkin menuSkin;
	public float x = 3.5f;
	public float y = 3.5f;
	public float z = 7;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		float width = Screen.width;
		float height = Screen.height;
		
		GUI.skin = menuSkin;
		
		//Buttons
		//GUI.Label (new Rect(width / x, height / y, width / 3.5f, height / 8), "UTC");
	
		//Back to settings
		if(GUI.Button(new Rect(width / 2.7f, height / 2.3f, width / 4f, height / 8f),"Back")){
			Application.LoadLevel("SettingsMenu");
		}
		else if(GUI.Button(new Rect(width / 2.3f, height / 9f, width / 7f, height / 8f),"UP")){
			//Up the screen
		}
		else if(GUI.Button(new Rect(width / 2.3f, height / 1.3f, width / 7f, height / 8f),"DOWN")){
			//Down the screen
		}
		else if(GUI.Button(new Rect(width / 11f, height / 2.3f, width / 7f, height / 8f),"RIGHT")){
			//Turn right the screen
		}
		else if(GUI.Button(new Rect(width / 1.27f, height / 2.3f, width / 7f, height / 8f),"LEFT")){
			//Turn left the screen
		}
		
		//GUI.Label(new Rect(width / 7, (float)(height / 1.2),(float)(width / 1.5), height / 6), "Play and play it again !");
		GUI.Box (new Rect(0, 0, (float)(width), (float)(height)), "Calibrate");
			
	}
}
