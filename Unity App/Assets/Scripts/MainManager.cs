using UnityEngine;
using System;
using System.IO;
using System.Collections;
using MiniJSON;

public class MainManager : MonoBehaviour 
{	
	//PARAMETRES
	public float timeToNote = 2f;
	public float reflexTime = 0.1f;
	public float timeAfterNote = 0.3f;
	public float timeBeforeNote = 0.1f;
	public int countdown = 3;
	public bool debug = false;
	public float decalageMusique = 0.5f;
	
	public GUIText countdownLabel;
	public GUITexture AreaA;
	public GUITexture AreaB;
	public GUITexture AreaC;
	
	protected ArrayList notesA, notesB, notesC;
	protected bool started;
	protected bool countdownOver;
	protected float beginning;	
	
	public GUIText pointsLabel;
	protected int points;
	
	public GUIText comboLabel;
	protected int combo;
	
	public GUIText multiplieurlabel;
	protected int multipieur;
	
	public TextAsset jsonFile;
	
	// Use this for initialization
	IEnumerator Start () 
	{		
		
		this.pointsLabel.text = "Points: 0";
		this.comboLabel.text = "Combo: 0";
		this.multiplieurlabel.text = "Multiplieur: 1";
		this.started = false;
		this.countdownOver = false;
		this.countdownLabel.text = countdown.ToString();	
		this.beginning = Time.time;
		points = 0;
		combo = 0;
		multipieur = 1;
		
		initializeFromJSON();	
		AreaA.GetComponent<NoteManager>().init(notesA, Note.Which.A);
		AreaB.GetComponent<NoteManager>().init(notesB, Note.Which.B);
		AreaC.GetComponent<NoteManager>().init(notesC, Note.Which.C);
		
		//decalage musique
		yield return new WaitForSeconds(this.decalageMusique);
		audio.Play();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!this.started)
		{
			if(this.countdownOver)
			{
				this.started = true;
			}
			else
			{//gestion du countdown
				if((Time.time-this.beginning) > countdown)
				{
					countdownLabel.enabled = false;	
					this.countdownOver = true;
				}
				else
				{
					int val =  countdown - (int)(Time.time-this.beginning);
					this.countdownLabel.text = val.ToString();
				}
			}
			
		}
	}
	
	private void initializeFromJSON()
	{
		//lecture du fichier contenant les notes
		String json = jsonFile.text;
		
		//parsing du JSON
		IDictionary search = (IDictionary)Json.Deserialize (json);
		
		// récupération des notes de la piste A
		IList aa = (IList)search ["A"];
		notesA = new ArrayList();
		foreach (IDictionary note in aa) {
			notesA.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
		} 
		
		
		// récupération des notes de la piste B
		IList bb = (IList)search ["B"];
		notesB = new ArrayList();
		foreach (IDictionary note in bb) {
			notesB.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
		} 
		
		
		// récupération des notes de la piste C
		IList cc = (IList)search ["C"];
		notesC = new ArrayList();
		foreach (IDictionary note in cc) {
			notesC.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
		} 		
		
	}
	
	public void addPoints(int points)
	{
		this.points += (points*multipieur);	
		this.pointsLabel.text = "Points: "+this.points;
	}
	
	public void addCombo()
	{
		this.combo++;	
		this.comboLabel.text = "Combo: "+this.combo;
		setMultiplieur();
	}
	
	public void resetCombo()
	{
		this.combo=0;	
		this.comboLabel.text = "Combo: "+this.combo;
		setMultiplieur();
	}
	
	protected void setMultiplieur()
	{
		if(combo < 10)
			this.multipieur = 1;
		else if(combo < 30)
			this.multipieur = 2;
		else if(combo < 50)
			this.multipieur = 4;
		else
			this.multipieur = 8;
		this.multiplieurlabel.text = "Multiplieur: "+this.multipieur;
	}
	
	
}
