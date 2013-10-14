using UnityEngine;
using System.Collections;

public class NoteManager : MonoBehaviour 
{
	public GUIText label;
	public GameObject startPoint;
	public GameObject notePrefab;
	
	//déterminant de la piste (A, B ou C)
	protected Note.Which piste;	
	//liste des notes à jouer
	protected ArrayList notesListe;	
	//référenc du Main manager
	protected MainManager mainManager;	
	//référence du Kinect controller
	protected MyKinectController kinectController;	
	//temps du lancement de la piste
	protected float beginning;	
	//si la piste à commencer
	protected bool initialized;	
	
	//si une note est actellement jouée
	protected bool active;
	//temps de début de la note
	protected float noteStart;
	//si une note a été jouée par le joueur
	protected bool played;
	//si une note a été ratée par le joueur
	protected bool missed;
	
	protected ArrayList noteObjects;
	
	void Start () 
	{
		this.guiTexture.enabled = false;
		this.kinectController = (MyKinectController) FindObjectOfType(typeof(MyKinectController));
		this.mainManager = (MainManager) FindObjectOfType(typeof(MainManager));
		this.beginning = 0;
		this.initialized = false;
		this.noteStart = 0;
		this.noteObjects = new ArrayList();
	}
	
	void Update () 
	{
		//si la musique a commencée
		if(this.initialized)
		{
			//si une note est actuellement jouée
			if(this.active)
			{
				// si la note est jouée à temps par le joueur	
				if(getTime()-this.noteStart < this.mainManager.reflexTime)
				{
					if((this.kinectController.noteGauche == this.piste || this.kinectController.noteDroite == this.piste))
					{
						if(!played)
						{
							this.played = true;
							setCurrentNoteColor(Color.green);
							this.label.text = "OK";
							this.mainManager.addPoints(100);
						}
						else
						{						
							this.mainManager.addPoints(1);	
						}
					}
					else
					{								
							setCurrentNoteColor(Color.red);
					}
				}
				else
				{		
					if(!played && !missed)
					{
						this.missed = true;
						setCurrentNoteColor(Color.red);
						this.label.text = "FAIL";
					}
					else
						if(played)
						{					
							this.mainManager.addPoints(1);									
						}
				}
			}
		}
	}
	
	protected void setCurrentNoteColor(Color c)
	{
		if(this.noteObjects.Count > 0)
		{
			GameObject noteObject = (GameObject) this.noteObjects[0];
			noteObject.transform.renderer.material.color = c;
		}	
	}
	
	public void init(ArrayList liste, Note.Which piste)
	{
		this.piste = piste;
		this.notesListe = liste;
		this.beginning = Time.time;
		this.initialized = true;
		StartCoroutine(playNotes());
		StartCoroutine(renderNotes());
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
		yield return new WaitForSeconds(this.mainManager.timeToNote + start-getTime());
		
		//la note est jouée
		this.noteStart = getTime();
		this.active = true;
		this.played = false;
		this.missed = false;
		//this.guiTexture.enabled = true;	
		
		//attente avant d'arreter la note
		yield return new WaitForSeconds(length);
		
		//la note est finie
		this.active = false;
		//this.guiTexture.enabled = false;		
		this.label.text = "";
		if(this.noteObjects.Count > 0)
			this.noteObjects.RemoveAt(0);
		
        yield return null;        
    }	
	IEnumerator renderNotes() 
	{		
		foreach(Note note in this.notesListe)
		{	// joue puis attend la fin de la note					
			yield return StartCoroutine(renderNote(note.start/1000f, note.length/1000f));
		}
        yield return null;
        
    }
	
	IEnumerator renderNote(float start, float length) 
	{		
		//attente avant de jouer la prochaine note
		yield return new WaitForSeconds(start-getTime());
				
		//on calcule la longueur de la note et son decalage de position pour que le bord inferieur soit au début de la piste
		float z =  1f * (length/0.2f);
		notePrefab.transform.localScale = new Vector3(notePrefab.transform.localScale.x, notePrefab.transform.localScale.y, 1f * (length/0.2f));
		Vector3 startPosition = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, startPoint.transform.position.z +(z-1)/2);
		GameObject obj = (GameObject) Instantiate(notePrefab, startPosition, startPoint.transform.rotation);
		
		this.noteObjects.Add(obj);
		//on prevoit la destruction de l'objet 3 secondes après la fin de sa note
		Destroy(obj, length + 3f);
		
		//attente avant d'arreter la note
		yield return new WaitForSeconds(length);
		
        yield return null;        
    }
	
	protected float getTime()
	{
		return (Time.time-this.beginning);
	}
	
}
