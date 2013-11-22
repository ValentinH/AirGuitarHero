using UnityEngine;
using System.Collections;


public class MainMenu : MonoBehaviour {
	
	//Use the skin defined in the inspector
	public GUISkin menuSkin;
	public float x = 1;
	public float y = 1;
	public float z = 1;
	
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
		
		if(GUI.Button(new Rect(width / 7, (float)(height / 3.5),(float)(width / 3.5), height / 6), "PLAY")){
			Application.LoadLevel("LevelSelection");
		}
		else if(GUI.Button(new Rect(width / 7, (float)(height / 2.14),(float)(width / 3.5), height / 6),"SETTINGS")){
			Application.LoadLevel("SettingsMenu");
		}
		else if(GUI.Button(new Rect(width / 7, (float)(height / 1.55),(float)(width / 3.5), height / 6),"EXIT")){
			Application.Quit();
		}
		GUI.Label(new Rect(width / 7, (float)(height / 1.2),(float)(width / 1.5), height / 6), "Play and play it again !");
		GUI.Box (new Rect(width / 10,height / 7, (float)(width / 2.6), (float)(height / 1.36)), "MENU");
			
	}
}

