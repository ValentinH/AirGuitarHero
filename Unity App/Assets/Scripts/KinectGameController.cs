﻿using UnityEngine;
using System;
using System.Collections;
using System.IO;
using MiniJSON;

public class KinectGameController : MonoBehaviour
{
	public GameObject KinectPrefab;
	protected SkeletonWrapper sw;	
	
	//référence du Main manager
	protected MainManager mainManager;
	
	private GameObject[] _bones;
	
	public Note.Which noteGauche, noteDroite, noteGenous;
	public bool bonusActivated;
	
	protected Vector3 headPos, leftHand, rightHand, leftKnee, rightKnee;
	
	
	// Use this for initialization
	void Start ()
	{
		sw = (SkeletonWrapper) FindObjectOfType(typeof(SkeletonWrapper));
		if(sw == null)
		{
			Instantiate(KinectPrefab);
			sw = (SkeletonWrapper) FindObjectOfType(typeof(SkeletonWrapper));
		}
		
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
				headPos = new Vector3 (
							sw.bonePos [0, 3].x,
							sw.bonePos [0, 3].y,
							sw.bonePos [0, 3].z);			
				
				//LeftHand management index=7
				leftHand = new Vector3 (
							sw.bonePos [0, 7].x,
							sw.bonePos [0, 7].y,
							sw.bonePos [0, 7].z);			
				
				//RightHand management index=11
				rightHand = new Vector3 (
							sw.bonePos [0, 11].x,
							sw.bonePos [0, 11].y,
							sw.bonePos [0, 11].z); 
				
				//LeftKnee management index=15
				leftKnee = new Vector3 (
							sw.bonePos [0, 13].x,
							sw.bonePos [0, 13].y,
							sw.bonePos [0, 13].z);
				
				
				//RightKnee management index=19
				rightKnee = new Vector3 (
							sw.bonePos [0, 17].x,
							sw.bonePos [0, 17].y,
							sw.bonePos [0, 17].z);
				
				
				//détermination des notes
				if(!headPos.Equals(new Vector3(0,0,0)))
				{
					noteGauche = getNote (leftHand);
					noteDroite = getNote (rightHand);
					noteGenous = getNoteGenous();
				}
				else
					noteGauche = noteDroite = noteGenous = Note.Which.NONE;
				
				bonusActivated = checkBonus();
			}
		}
	}
	
	private Note.Which getNote (Vector3 handPos)
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
	
	private Note.Which getNoteGenous ()
	{
		float diff = leftKnee.y - rightKnee.y;
		if(diff < 0) diff = -diff;
		if (diff > 0.1) {
					
			return Note.Which.D;
		}
		return Note.Which.NONE;
	}
	
	private bool checkBonus ()
	{
		bool b = false;
		if(rightHand.z - headPos.z > 0.5) b=true;
		b= b && (rightHand.z - headPos.z > 0.5);
		float diff = leftHand.x-rightHand.x;
		if(diff < 0) diff = -diff;
		b = b && diff<0.1;
		return b;
	}
}

