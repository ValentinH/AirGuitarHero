using UnityEngine;
using System;
using System.Collections;
using MiniJSON;
public class MyKinectController : MonoBehaviour
{
	public SkeletonWrapper sw;
	public GameObject LeftHand;
	public GameObject RightHand;
	public GameObject Head;
	public GUITexture AreaA;
	public GUITexture AreaB;
	public GUITexture AreaC;
	public GUIText pointsLabel;
	public float scale = 1.0f;
	
	
	private GameObject[] _bones;
	private float lastTime;
	private float noteDuration;
	private Which noteActive;
	private GUITexture areaActive;
	private int points;
	private Which lastG, lastD;
	private ArrayList notesA, notesB, notesC;
	
	public enum Which
	{
		A = 0,
		B = 1,
		C = 2,
		NONE = -1
	}
	
	// Use this for initialization
	void Start ()
	{
		AreaA.enabled = false;	
		AreaB.enabled = false;	
		AreaC.enabled = false;
		noteActive = Which.NONE;
		lastTime = 0;
		points = 0;
		pointsLabel.text = "Points: 0";
		lastG = lastD = Which.NONE;
		noteDuration = 1;
		
		notesA = new ArrayList();
		notesB = new ArrayList();
		notesC = new ArrayList();
		
		initializeFromJSON();
		StartCoroutine(playNotes(notesA, Which.A));
		//StartCoroutine(playNotes(notesB, Which.B));
		//StartCoroutine(playNotes(notesC, Which.C));
	}
	
	// Update is called once per frame
	void Update ()
	{
		//update all of the bones positions
		/*if (sw.pollSkeleton ()) {
			//Head management index=3
			Head.transform.localPosition = new Vector3 (
						sw.bonePos [0, 3].x * scale,
						sw.bonePos [0, 3].y * scale,
						LeftHand.transform.localPosition.z);			
			
			//LeftHand management index=7
			LeftHand.transform.localPosition = new Vector3 (
						sw.bonePos [0, 7].x * scale,
						sw.bonePos [0, 7].y * scale,
						LeftHand.transform.localPosition.z);			
			
			//RightHand management index=11
			RightHand.transform.localPosition = new Vector3 (
						sw.bonePos [0, 11].x * scale,
						sw.bonePos [0, 11].y * scale,
						LeftHand.transform.localPosition.z);
			
			Vector2 headPos = new Vector2 (Head.transform.localPosition.x, Head.transform.localPosition.y);
			Vector2 leftPos = new Vector2 (LeftHand.transform.localPosition.x, LeftHand.transform.localPosition.y);
			Vector2 rightPos = new Vector2 (RightHand.transform.localPosition.x, RightHand.transform.localPosition.y);
			
			Which noteG = getNote (headPos, leftPos);
			Which noteD = getNote (headPos, rightPos);*/
			
			/*if (noteActive == Which.NONE && Time.time - lastTime > 0.4) {
				int newNote = (int)UnityEngine.Random.Range (0f, 3f);
				noteDuration = UnityEngine.Random.Range (0.5f, 1.5f);
				switch (newNote) {
				case 0:
					noteActive = Which.A;
					areaActive = AreaA;
					break;
				case 1:
					noteActive = Which.B;
					areaActive = AreaB;
					break;
				default:
					noteActive = Which.C;
					areaActive = AreaC;
					break;
				}
				areaActive.guiTexture.color = Color.red;
				areaActive.enabled = true;
				lastTime = Time.time;
			} else if (noteActive != Which.NONE && Time.time - lastTime > noteDuration) {
				noteActive = Which.NONE;
				if (areaActive != null)
					areaActive.enabled = false;
			}*/
						
			/*if (areaActive != null && areaActive.enabled && noteActive != Which.NONE && (noteActive == noteG || noteActive == noteD)) {
				areaActive.guiTexture.color = Color.green;
				points++;				
				pointsLabel.text = "Points: " + points;
			} else {				
				if (areaActive != null)
					areaActive.guiTexture.color = Color.red;
			}
			lastD = noteD;
			lastG = noteG;*/
		//}
	}
	
	private Which getNote (Vector2 headPos, Vector2 handPos)
	{
		if (handPos.y >= headPos.y - 0.2) {
			if (handPos.x > headPos.x + 0.2)
				return Which.C;
			if (handPos.x < headPos.x - 0.2)
				return Which.A;
			
			return Which.B;
		}
	
		return Which.NONE;
	} 
	
	private void initializeFromJSON()
	{
		String json = "{\"A\":[{\"start\":1000,\"length\":1000},{\"start\":3000,\"length\":1000},{\"start\":5000,\"length\":1000},{\"start\":7000,\"length\":1000},{\"start\":3150,\"length\":200}],\"B\":[{\"start\":1200,\"length\":100},{\"start\":1950,\"length\":700},{\"start\":2900,\"length\":200},{\"start\":3350,\"length\":150},{\"start\":3700,\"length\":250}],\"C\":[{\"start\":750,\"length\":100},{\"start\":1000,\"length\":100},{\"start\":1450,\"length\":150},{\"start\":1700,\"length\":100},{\"start\":2700,\"length\":300},{\"start\":3500,\"length\":300}]}";
		IDictionary search = (IDictionary)Json.Deserialize (json);
		IList aa = (IList)search ["A"];
		foreach (IDictionary note in aa) {
			notesA.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
		} 
		IList bb = (IList)search ["B"];
		foreach (IDictionary note in bb) {
			notesB.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
		} 
		IList cc = (IList)search ["C"];
		foreach (IDictionary note in cc) {
			notesC.Add(new Note(Int32.Parse(note ["start"].ToString()), Int32.Parse(note ["length"].ToString())));
		} 	
	}
	
	protected class Note
	{		
		public int start;
		public int length;
		
		public Note(int start, int length)
		{
			this.start = start;
			this.length = length;
		}
	}
	
	
    IEnumerator playNotes(ArrayList notes, Which which) 
	{
		GUITexture area;
		switch (which) {
			case Which.A:
				area = AreaA;
				break;
			case Which.B:
				area = AreaB;
				break;
			default:
				area = AreaC;
				break;
			}
		float current = 0;
		foreach(Note note in notes)
		{
			
			Debug.Log("current "+(current)+" "+which);
			Debug.Log("wait "+(note.start/1000f-current)+" "+which);
			yield return new WaitForSeconds(note.start/1000f-current);
			area.enabled = true;	
			
			Debug.Log("play "+(note.length/1000f)+" "+which);
			yield return new WaitForSeconds(note.length/1000f);	
			current += note.start/1000f + note.length/1000f;		
			area.enabled = false;		
		}
        yield return null;
    }

}

