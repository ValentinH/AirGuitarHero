using UnityEngine;
using System;
using System.Collections;
using System.IO;
using MiniJSON;

public class KinectMenuController : MonoBehaviour
{	
	public GameObject KinectPrefab;
	
	protected SkeletonWrapper sw;
	public GUITexture cursor;
	public GUIText debug;
	
	private GameObject[] _bones;
	
	private Vector3 rightHand;
	
	// Use this for initialization
	void Start ()
	{
		sw = (SkeletonWrapper) FindObjectOfType(typeof(SkeletonWrapper));
		if(sw == null)
		{
			Instantiate(KinectPrefab);
			sw = (SkeletonWrapper) FindObjectOfType(typeof(SkeletonWrapper));
		}
	}
	
	// Update is called once per frame
	void Update ()
	{		
		//update all of the bones positions
		if (sw.pollSkeleton ()) 
		{			
			//Head management index=3
			Vector3 headPos = new Vector3 (sw.bonePos [0, 3].x, sw.bonePos [0, 3].y, sw.bonePos [0, 3].z);			
			
			//LeftHand management index=7
			Vector3 leftPos = new Vector3 (sw.bonePos [0, 7].x,	sw.bonePos [0, 7].y,	sw.bonePos [0, 7].z);			
			
			//RightHand management index=11
			Vector3 rightPos = new Vector3 (sw.bonePos [0, 11].x, sw.bonePos [0, 11].y, sw.bonePos [0, 11].z); 
			
			
			float x = (headPos.x + rightPos.x )+ 0.5f;
			float y =  1 - (headPos.y - rightPos.y);			
			cursor.transform.position = new Vector3(x, y, cursor.transform.position.z);
			rightHand = new Vector3(x, y, rightPos.z - headPos.z);
		}
		
	}
	
	public Vector3 getRightHand()
	{
		return rightHand;	
	}
}

