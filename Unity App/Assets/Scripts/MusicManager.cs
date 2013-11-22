using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour 
{	
    private static MusicManager instance = null;
	
    public static MusicManager Instance {
        get { return instance; }
    }
	
    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);			
			instance.transform.position = Camera.main.transform.position;
            return;
        } else {
            instance = this;
			MusicManager.SetVolume(0.5f);			
			instance.transform.position = Camera.main.transform.position;
        }
        DontDestroyOnLoad(this.gameObject);
    }
	
	private static float volume = 0.5f;
	private static AudioClip music;
	
	public static void SetVolume(float v){
		volume = v;
		instance.audio.volume = v;
	}
	
	public static float GetVolume(){
		return volume;	
	}
	
	public static void SetMusic(AudioClip m){
		music = m;
		instance.audio.clip = m;
		instance.audio.Play();
	}
	
	public static void PlayMusic(){
		instance.audio.Play();
	}
	
	public static void StopMusic(){
		instance.audio.Stop();
	}
	
	public static AudioClip GetMusic(){
		return music;
	}
 

}
