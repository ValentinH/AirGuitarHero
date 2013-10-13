using UnityEngine;
using System;
using System.IO;
using System.Collections;
using MiniJSON;

public class MainManager : MonoBehaviour 
{	
	public GUITexture AreaA;
	public GUITexture AreaB;
	public GUITexture AreaC;
	
	// Use this for initialization
	void Start () 
	{		
		initializeFromJSON();
	}
	
	// Update is called once per frame
	void Update () {
	
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
		ArrayList notesA = new ArrayList();
		foreach (IDictionary note in aa) {
			notesA.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
		} 
		
		
		// récupération des notes de la piste B
		IList bb = (IList)search ["B"];
		ArrayList notesB = new ArrayList();
		foreach (IDictionary note in bb) {
			notesB.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
		} 
		
		
		// récupération des notes de la piste C
		IList cc = (IList)search ["C"];
		ArrayList notesC = new ArrayList();
		foreach (IDictionary note in cc) {
			notesC.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
		} 	
		
		//lancement des pistes de notes
		AreaA.GetComponent<NoteManager>().init(notesA, Note.Which.A);
		AreaB.GetComponent<NoteManager>().init(notesB, Note.Which.B);
		AreaC.GetComponent<NoteManager>().init(notesC, Note.Which.C);
	}
	
	
}
