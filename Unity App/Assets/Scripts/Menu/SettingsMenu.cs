using UnityEngine;
using System.Collections;


public class SettingsMenu : MonoBehaviour {
	
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
		float volume = 1;
		
		GUI.skin = menuSkin;
		
		//Volume
		GUI.Label (new Rect(width / 4.3f, height / 3.77f, width / 3.5f, height / 8), "Volume");
		volume = GUI.HorizontalSlider(new Rect(width / 7, (float)(height / 3),(float)(width / 3.5), height / 8), MusicManager.GetVolume(), (float)0.1, 1);
		MusicManager.SetVolume (volume);
		this.audio.volume = MusicManager.GetVolume();
		
		//Calibrage
		if(GUI.Button(new Rect(width / 7, (float)(height / 2.38),(float)(width / 3.5), height / 8),"Calibrate")){
			//Application.LoadLevel("MainMenu");
		} //Credits
		else if(GUI.Button(new Rect(width / 7, (float)(height / 1.8),(float)(width / 3.5), height / 8),"Credits")){
			Application.LoadLevel("Credits");
		}//Back to main menu
		else if(GUI.Button(new Rect(width / 7, (float)(height / 1.44),(float)(width / 3.5), height / 8),"Menu")){
			Application.LoadLevel("MainMenu");
		}
		GUI.Label(new Rect(width / 7, (float)(height / 1.2),(float)(width / 1.5), height / 6), "Play and play it again !");
		GUI.Box (new Rect(width / 10,height / 7, (float)(width / 2.6), (float)(height / 1.36)), "Settings");
			
	}
}
