using UnityEngine;
using System.Collections;

public class NoteManager : MonoBehaviour 
{
	protected Note.Which piste;
	protected ArrayList notesListe;
	protected MyKinectController kinectController;
	
	void Start () 
	{
		this.guiTexture.enabled = false;
		kinectController = (MyKinectController) FindObjectOfType(typeof(MyKinectController));
	}
	
	void Update () 
	{
		Debug.Log(kinectController.noteDroite);
	}
	
	public void init(ArrayList liste, Note.Which piste)
	{
		this.piste = piste;
		this.notesListe = liste;
		StartCoroutine(playNotes());
	}
	
	IEnumerator playNotes() 
	{		
		foreach(Note note in this.notesListe)
		{	// joue puis attend la fin de la note					
			yield return StartCoroutine(playNote(note.start/1000f, note.length/1000f));
		}
        yield return null;
        
    }
	
	IEnumerator playNote(float start, float length) 
	{		
		//attente avant de jouer la prochaine note
		yield return new WaitForSeconds(start-Time.time);
		
		//la note est jouée
		this.guiTexture.enabled = true;	
		
		//attente avant d'arreter la note
		yield return new WaitForSeconds(length);
		
		//la note est finie
		this.guiTexture.enabled = false;
		
        yield return null;
        
    }
	
	
	
	
}
