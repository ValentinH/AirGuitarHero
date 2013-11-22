using UnityEngine;
using System;
using System.IO;
using System.Collections;
using MiniJSON;

public class MainManager : MonoBehaviour 
{	
	//PARAMETRES
	public bool clavier = false;
	public float timeToNote = 2f;
	public float reflexTime = 0.1f;
	public float timeAfterNote = 0.3f;
	public float timeBeforeNote = 0.1f;
	public int countdown = 3;
	public bool debug = false;
	public float decalageMusique = 2.4f;
	public bool fail_sound = true;
	
	public GUIText countdownLabel;
	public GUITexture AreaA;
	public GUITexture AreaB;
	public GUITexture AreaC;
	public GUITexture AreaD;
	
	protected ArrayList notesA, notesB, notesC, notesD;
	protected bool started;
	protected bool countdownOver;
	protected float beginning;	
	
	public GUIText pointsLabel;
	protected int points;
	
	public GUIText comboLabel;
	protected int combo;
	
	public GUIText multiplicateurlabel;
	protected int multiplicateur;
	protected bool bonusOn;
	
	public GUIText pourcentslabel;
	protected float notesReussies;
	protected float notesTotales;
	
	protected bool direct_start;
	
	public TextAsset jsonFile;
	public AudioClip music;
	
	protected MyKinectController kinectController;
	
	// Use this for initialization
	void Start () 
	{	
		MusicManager.StopMusic();
		this.direct_start = true;
		this.pointsLabel.text = "Points: 0";
		this.comboLabel.text = "Combo: 0";
		this.multiplicateurlabel.text = "Multiplicateur: 1";
		this.pourcentslabel.text = "100%";
		this.started = false;
		this.countdownOver = false;
		this.countdownLabel.text = countdown.ToString();	
		this.beginning = Time.time;
		points = 0;
		combo = 0;
		multiplicateur = 1;
		notesTotales = notesReussies = 0;
		bonusOn = false;
		
		this.kinectController = (MyKinectController) FindObjectOfType(typeof(MyKinectController));
		
		initializeFromJSON();
		if(direct_start)
			StartPistes();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Set Volume
		//this.audio.volume = MusicManager.GetVolume();
		
		if(!this.started)
		{
			if(this.countdownOver)
			{
				this.started = true;
				if(!direct_start)
					StartPistes();
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
		if(this.kinectController.bonusActivated){
			this.bonusOn = true;
			setMultiplieur();
		}
	}
	
	private void StartPistes () 
	{
		AreaA.GetComponent<NoteManager>().init(notesA, Note.Which.A);
		AreaB.GetComponent<NoteManager>().init(notesB, Note.Which.B);
		AreaC.GetComponent<NoteManager>().init(notesC, Note.Which.C);
		AreaD.GetComponent<NoteManager>().init(notesD, Note.Which.D);
		
		StartCoroutine("StartSong");
	}	
	
	private IEnumerator StartSong () 
	{
		//decalage musique
		yield return new WaitForSeconds(this.decalageMusique);
		MusicManager.SetMusic(music);
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
		
		// récupération des notes de la piste D
		IList dd = (IList)search ["D"];
		notesD = new ArrayList();
		if(dd != null)
		{
			foreach (IDictionary note in dd) {
				notesD.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
			}
		}
		
		if(search.Contains("direct_start"))
		{
			direct_start = (bool) search["direct_start"];
		}
		
	}
	
	public void addNote(bool reussie)
	{
		notesTotales++;
		if(reussie)
		{
			notesReussies++;
			addCombo();
			addPoints(100);
		}
		else
			resetCombo();
		int percent = (int)(notesReussies/notesTotales*100f);
		this.pourcentslabel.text = percent+"%";
	}
	
	public void addPoints(int points)
	{
		this.points += (points*multiplicateur);	
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
		this.bonusOn =false;
		this.combo=0;	
		this.comboLabel.text = "Combo: "+this.combo;
		setMultiplieur();
	}
	
	protected void setMultiplieur()
	{
		if(combo < 10)
			this.multiplicateur = 1;
		else if(combo < 30)
			this.multiplicateur = 2;
		else if(combo < 50)
			this.multiplicateur = 4;
		else
			this.multiplicateur = 8;
		if(this.multiplicateur > 1 && bonusOn)
			this.multiplicateur = 16;
		this.multiplicateurlabel.text = "Multiplicateur: "+this.multiplicateur;
	}
	
	
}
