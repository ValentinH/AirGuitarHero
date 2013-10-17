using UnityEngine;
using System;
using System.Collections;
using System.IO;
using MiniJSON;

public class MyKinectController : MonoBehaviour
{
	public SkeletonWrapper sw;
	public GameObject LeftHand;
	public GameObject RightHand;
	public GameObject Head;	
	public float scale = 1.0f;
	public bool clavier = false;
	
	
	private GameObject[] _bones;
	
	public Note.Which noteGauche, noteDroite;
	
	
	// Use this for initialization
	void Start ()
	{
		noteGauche = noteDroite = Note.Which.NONE;		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(clavier)
		{
			noteGauche = noteDroite = Note.Which.NONE;
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
				
				//détermination des notes
				if(headPos != new Vector2(0,0))
				{
					noteGauche = getNote (headPos, leftPos);
					noteDroite = getNote (headPos, rightPos);
				}
				else
					noteGauche = noteDroite = Note.Which.NONE;	
			}
		}
	}
	
	private Note.Which getNote (Vector2 headPos, Vector2 handPos)
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
}

