using UnityEngine;
using System;
using System.Collections;
using System.IO;
using MiniJSON;

public class KinectMenuController : MonoBehaviour
{	
	public GameObject KinectPrefab;
	
	protected SkeletonWrapper sw;
	protected GUITexture leftCursor;
	protected GUITexture rightCursor;
	protected GUIText debug;
	
	private GameObject[] _bones;
	
	private Vector3 leftHand;
	private Vector3 rightHand;
	
	public const float CLICK_Z = 0.5f;
	
	// Use this for initialization
	void Start ()
	{
		sw = (SkeletonWrapper) FindObjectOfType(typeof(SkeletonWrapper));
		if(sw == null)
		{
			Instantiate(KinectPrefab);
			sw = (SkeletonWrapper) FindObjectOfType(typeof(SkeletonWrapper));
		}
		leftCursor = (GUITexture) GameObject.Find("LeftCursor").GetComponent<GUITexture>();
		rightCursor = (GUITexture) GameObject.Find("RightCursor").GetComponent<GUITexture>();
		debug = (GUIText) GameObject.Find("DebugLabel").GetComponent<GUIText>();
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
			
			
			float x = (rightPos.x - headPos.x)+ 0.15f;
			float y =  0.75f - (headPos.y - rightPos.y);
			rightCursor.transform.position = new Vector3(x, y, rightCursor.transform.position.z);
			rightHand = new Vector3(x, y, rightPos.z - headPos.z);
			
			x = (leftPos.x - headPos.x)+ 0.85f;
			y =  0.75f - (headPos.y - leftPos.y);
			leftCursor.transform.position = new Vector3(x, y, leftCursor.transform.position.z);
			leftHand = new Vector3(x, y, leftPos.z - headPos.z);
			
			
		}
		
	}
	
	public Vector3 getRightHand()
	{
		return rightHand;	
	}
	
	public Vector3 getLeftHand()
	{
		return leftHand;	
	}
	
	public void moveKinect(float yDiff)
	{
		KinectSensor ks = (KinectSensor) FindObjectOfType(typeof(KinectSensor));
		float y = ks.lookAt.y;
		y += yDiff;
		if(y>0 && y <2)
			ks.setKinectAngle(new Vector3(0f, y, 0f));
	}
}

