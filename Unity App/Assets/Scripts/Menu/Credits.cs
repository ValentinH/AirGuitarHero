using UnityEngine;
using System.Collections;


public class Credits : MonoBehaviour {
	
	//Use the skin defined in the inspector
	public GUISkin menuSkin;
	public float x = 3.5f;
	public float y = 3.5f;
	public float z = 7;
	
	// Use this for initialization
	void Start () {
		this.audio.volume = MusicManager.GetVolume();
		this.audio.clip = MusicManager.GetMusic();
		this.audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		float width = Screen.width;
		float height = Screen.height;
		
		GUI.skin = menuSkin;
		
		//Volume
		GUI.Label (new Rect(width / x, height / y, width / 3.5f, height / 8), "UTC");
	
		GUI.TextArea(new Rect(width / 7, height / 3.77f, width / 3.5f, height / 2.48f), "Developped by Valentin Hervieu and Vincent Meyer for the University of Technologie of Compiègne");
		
		//Back to settings
		if(GUI.Button(new Rect(width / 7, (float)(height / 1.44),(float)(width / 3.5), height / 8),"Back")){
			Application.LoadLevel("SettingsMenu");
		}
		GUI.Label(new Rect(width / 7, (float)(height / 1.2),(float)(width / 1.5), height / 6), "Play and play it again !");
		GUI.Box (new Rect(width / 10,height / 7, (float)(width / 2.6), (float)(height / 1.36)), "Credits");
			
	}
}
