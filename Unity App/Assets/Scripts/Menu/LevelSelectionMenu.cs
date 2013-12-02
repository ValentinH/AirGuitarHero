using UnityEngine;
using System.Collections;


public class LevelSelectionMenu : MonoBehaviour {
	
	//Use the skin defined in the inspector
	public GUISkin menuSkin;
	public float x = 6;
	public float y = 6;
	public float z = 6;
	
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
		
		if(GUI.Button(new Rect(width / 7, (float)(height / 3.5),(float)(width / 3.5), height / 8), "LEVEL 1")){
			Application.LoadLevel("Game");
		}
		else if(GUI.Button(new Rect(width / 7, (float)(height / 2.38),(float)(width / 3.5), height / 8),"LEVEL 2")){
			//Application.LoadLevel("MainMenu");
		}
		else if(GUI.Button(new Rect(width / 7, (float)(height / 1.8),(float)(width / 3.5), height / 8),"LEVEL 3")){
			//Application.LoadLevel("MainMenu");
		}
		else if(GUI.Button(new Rect(width / 7, (float)(height / 1.44),(float)(width / 3.5), height / 8),"BACK")){
			Application.LoadLevel("MainMenu");
		}
		GUI.Label(new Rect(width / 7, (float)(height / 1.2),(float)(width / 1.5), height / 6), "Play and play it again !");
		GUI.Box (new Rect(width / 10,height / 7, (float)(width / 2.6), (float)(height / 1.36)), "Select your level");
			
	}
}
