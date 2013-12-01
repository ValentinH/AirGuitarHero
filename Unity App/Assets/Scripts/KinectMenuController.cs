using UnityEngine;
using System;
using System.Collections;
using System.IO;
using MiniJSON;

public class KinectMenuController : MonoBehaviour
{
	public SkeletonWrapper sw;
	public GUITexture cursor;
	
	private GameObject[] _bones;
	
	
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{		
		//update all of the bones positions
		if (sw.pollSkeleton ()) 
		{			
			//Head management index=3
			Vector2 headPos = new Vector2 (sw.bonePos [0, 3].x, sw.bonePos [0, 3].y);			
			
			//LeftHand management index=7
			Vector2 leftPos = new Vector2 (sw.bonePos [0, 7].x,	sw.bonePos [0, 7].y);			
			
			//RightHand management index=11
			Vector2 rightPos = new Vector3 (sw.bonePos [0, 11].x,sw.bonePos [0, 11].y); 
			
			
			float x = 0.5f + rightPos.x;
			float y = 0.5f + rightPos.y;			
			cursor.transform.position = new Vector3(x, y, cursor.transform.position.z);						
		}
		
	}
}

