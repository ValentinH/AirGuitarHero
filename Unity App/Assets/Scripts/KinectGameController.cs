using UnityEngine;
using System;
using System.Collections;
using System.IO;
using MiniJSON;

public class KinectGameController : MonoBehaviour
{
	public SkeletonWrapper sw;
	public GameObject LeftHand;
	public GameObject RightHand;
	public GameObject Head;	
	public GameObject LeftKnee;
	public GameObject RightKnee;
	public float scale = 1.0f;
	
	
	//référence du Main manager
	protected MainManager mainManager;
	
	private GameObject[] _bones;
	
	public Note.Which noteGauche, noteDroite, noteGenous;
	public bool bonusActivated;
	
	
	// Use this for initialization
	void Start ()
	{
		this.mainManager = (MainManager) FindObjectOfType(typeof(MainManager));
		noteGauche = noteDroite = noteGenous = Note.Which.NONE;
		bonusActivated = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(mainManager.clavier)
		{
			noteGauche = noteDroite = noteGenous = Note.Which.NONE;
			if(Input.GetKey(KeyCode.LeftArrow))
				noteGauche = Note.Which.A;
			if(Input.GetKey(KeyCode.DownArrow))
			{
				if(noteGauche == Note.Which.NONE)
					noteGauche = Note.Which.B;
				else
					noteDroite = Note.Which.B;
			}
			if(Input.GetKey(KeyCode.RightArrow))
			{
				if(noteGauche == Note.Which.NONE)
					noteGauche = Note.Which.C;
				else
					noteDroite = Note.Which.C;
			}	
			if(Input.GetKey(KeyCode.Space))
			{
				noteGenous = Note.Which.D;
			}
			
			bonusActivated = false;
			if(Input.GetKey(KeyCode.LeftAlt))
			{
				bonusActivated = true;
			}				
		}
		else
		{
			//update all of the bones positions
			if (sw.pollSkeleton ()) 
			{			
				//Head management index=3
				Head.transform.localPosition = new Vector3 (
							sw.bonePos [0, 3].x * scale,
							sw.bonePos [0, 3].y * scale,
							sw.bonePos [0, 3].z * scale);			
				
				//LeftHand management index=7
				LeftHand.transform.localPosition = new Vector3 (
							sw.bonePos [0, 7].x * scale,
							sw.bonePos [0, 7].y * scale,
							sw.bonePos [0, 7].z * scale);			
				
				//RightHand management index=11
				RightHand.transform.localPosition = new Vector3 (
							sw.bonePos [0, 11].x * scale,
							sw.bonePos [0, 11].y * scale,
							sw.bonePos [0, 11].z * scale); 
				
				//LeftKnee management index=15
				LeftKnee.transform.localPosition = new Vector3 (
							sw.bonePos [0, 13].x * scale,
							sw.bonePos [0, 13].y * scale,
							sw.bonePos [0, 13].z * scale);
				
				
				//RightKnee management index=19
				RightKnee.transform.localPosition = new Vector3 (
							sw.bonePos [0, 17].x * scale,
							sw.bonePos [0, 17].y * scale,
							sw.bonePos [0, 17].z * scale);
				
				
				//détermination des notes
				if(!Head.transform.localPosition.Equals(new Vector3(0,0,0)))
				{
					noteGauche = getNote (Head.transform.localPosition, LeftHand.transform.localPosition);
					noteDroite = getNote (Head.transform.localPosition, RightHand.transform.localPosition);
					noteGenous = getNoteGenous(LeftKnee.transform.localPosition, RightKnee.transform.localPosition);
				}
				else
					noteGauche = noteDroite = noteGenous = Note.Which.NONE;
				
				bonusActivated = checkBonus(Head.transform.localPosition, LeftHand.transform.localPosition, RightHand.transform.localPosition);
			}
		}
	}
	
	private Note.Which getNote (Vector3 headPos, Vector3 handPos)
	{
		if (handPos.y >= headPos.y) {
			if (handPos.x > headPos.x + 0.25)
			{
				return Note.Which.C;
			}
			if (handPos.x < headPos.x - 0.25)
				return Note.Which.A;
			
			return Note.Which.B;
		}	
		return Note.Which.NONE;
	}
	
	private Note.Which getNoteGenous (Vector3 leftPos, Vector3 rightPos)
	{
		float diff = leftPos.y - rightPos.y;
		if(diff < 0) diff = -diff;
		if (diff > 0.1) {
					
			return Note.Which.D;
		}
		return Note.Which.NONE;
	}
	
	private bool checkBonus (Vector3 head, Vector3 leftHand, Vector3 rightHand)
	{
		bool b = false;
		if(rightHand.z - head.z > 0.5) b=true;
		b= b && (rightHand.z - head.z > 0.5);
		float diff = leftHand.x-rightHand.x;
		if(diff < 0) diff = -diff;
		b = b && diff<0.1;
		return b;
	}
}

