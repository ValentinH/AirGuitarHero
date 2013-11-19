using UnityEngine;
using System.Collections;


public class MainMenu : MonoBehaviour {
	
	//Use the skin defined in the inspector
	public GUISkin menuSkin;
	public AudioClip sound;
	public float x = 1;
	public float y = 1;
	public float z = 1;
	
	// Use this for initialization
	void Start () {
		this.audio.volume = MusicManager.GetVolume();
		MusicManager.SetMusic(sound);
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

public static class MusicManager {
	private static float volume = 0.5f;
	private static AudioClip music;
	
	public static void SetVolume(float v){
		volume = v;
	}
	
	public static float GetVolume(){
		return volume;	
	}
	
	public static void SetMusic(AudioClip m){
		music = m;
	}
	
	public static AudioClip GetMusic(){
		return music;
	}
}
