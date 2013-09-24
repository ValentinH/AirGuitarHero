using UnityEngine;
using System;
using System.Collections;


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
	private Note noteActive;
	private GUITexture areaActive;
	private int points;
	private Note lastG, lastD;
	
	public enum Note
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
		noteActive = Note.NONE;
		lastTime = 0;
		points = 0;
		pointsLabel.text = "Points: 0";
		lastG = lastD = Note.NONE;
		noteDuration = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//update all of the bones positions
		if (sw.pollSkeleton())
		{
			//Head management index=3
			Head.transform.localPosition = new Vector3(
						sw.bonePos[0,3].x * scale,
						sw.bonePos[0,3].y * scale,
						LeftHand.transform.localPosition.z);			
			
			//LeftHand management index=7
			LeftHand.transform.localPosition = new Vector3(
						sw.bonePos[0,7].x * scale,
						sw.bonePos[0,7].y * scale,
						LeftHand.transform.localPosition.z);			
			
			//RightHand management index=11
			RightHand.transform.localPosition = new Vector3(
						sw.bonePos[0,11].x * scale,
						sw.bonePos[0,11].y * scale,
						LeftHand.transform.localPosition.z);
			
			Vector2 headPos = new Vector2(Head.transform.localPosition.x, Head.transform.localPosition.y);
			Vector2 leftPos = new Vector2(LeftHand.transform.localPosition.x, LeftHand.transform.localPosition.y);
			Vector2 rightPos = new Vector2(RightHand.transform.localPosition.x, RightHand.transform.localPosition.y);
			
			Note noteG = getNote(headPos, leftPos);
			Note noteD = getNote(headPos, rightPos);
			
			if(noteActive == Note.NONE && Time.time - lastTime > 0.4)
			{
				int newNote =  (int) UnityEngine.Random.Range(0f,3f);
				noteDuration = UnityEngine.Random.Range(0.5f,2.5f);
				switch(newNote)
				{
				case 0:
					noteActive = Note.A;
					areaActive = AreaA;
					break;
				case 1:
					noteActive = Note.B;
					areaActive = AreaB;
					break;
				default:
					noteActive = Note.C;
					areaActive = AreaC;
					break;
				}
				areaActive.guiTexture.color = Color.red;
				areaActive.enabled = true;
				lastTime = Time.time;
			}
			else
				if(noteActive != Note.NONE && Time.time - lastTime > noteDuration)
				{
					noteActive = Note.NONE;
					if(areaActive != null)
						areaActive.enabled = false;
				}
						
			if(areaActive !=null && areaActive.enabled && noteActive != Note.NONE && (noteActive == noteG  || noteActive == noteD ))
			{
				areaActive.guiTexture.color = Color.green;
				points++;				
				pointsLabel.text = "Points: "+points;
			}
			else
			{				
				if(areaActive !=null)
					areaActive.guiTexture.color = Color.red;
			}
			lastD = noteD;
			lastG = noteG;
		}
	}
	
	private Note getNote(Vector2 headPos, Vector2 handPos)
	{
		if(handPos.y >= headPos.y-0.2)
		{
			if(handPos.x > headPos.x +0.2)
				return Note.C;
			if(handPos.x < headPos.x -0.2)
				return Note.A;
			
			return Note.B;
		}
	
		return Note.NONE;
	}
}
