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
	public int countdown = 3;
	
	public GUIText countdownLabel;
	public GUITexture AreaA;
	public GUITexture AreaB;
	public GUITexture AreaC;
	
	protected ArrayList notesA, notesB, notesC;
	protected bool started;
	protected bool countdownOver;
	protected float beginning;	
	
	public GUIText pointsLabel;
	protected int points = 0;
	
	// Use this for initialization
	void Start () 
	{		
		this.pointsLabel.text = "Points: 0";
		this.started = false;
		this.countdownOver = false;
		this.countdownLabel.text = countdown.ToString();
		initializeFromJSON();		
		this.beginning = Time.time;
		points = 0;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!this.started)
		{
			if(this.countdownOver)
			{
				//lancement des pistes de notes
				AreaA.GetComponent<NoteManager>().init(notesA, Note.Which.A);
				AreaB.GetComponent<NoteManager>().init(notesB, Note.Which.B);
				AreaC.GetComponent<NoteManager>().init(notesC, Note.Which.C);
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
		String fileName = "music.json";
		StreamReader sr = new StreamReader(Application.dataPath + "/" + fileName);
		String json = sr.ReadToEnd();
    	sr.Close();
		
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
		this.points += points;	
		this.pointsLabel.text = "Points: "+this.points;
	}
	
	
}
