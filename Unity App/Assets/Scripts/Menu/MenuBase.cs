using UnityEngine;
using System.Collections;

public class MenuBase : MonoBehaviour {
	
	//Use the skin defined in the inspector
	public GUISkin menuSkin;
	public GUIText debug;
	
	protected KinectMenuController kinectController;
	
	protected float screenWidth, screenHeight;
	
	protected bool clickEnabled;

	// Use this for initialization
	public void Start () {
		this.clickEnabled = false;
		StartCoroutine("enableClick");
		this.kinectController = (KinectMenuController) FindObjectOfType(typeof(KinectMenuController));
		
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	private IEnumerator enableClick()
	{
		yield return new WaitForSeconds(0.5f);
		this.clickEnabled = true;
	}
	
	protected Vector2 getHandPos()
	{
		Vector2 hand = kinectController.getRightHand();
		hand.x *= screenWidth;
		hand.y = 1 - hand.y;
		hand.y *= screenHeight;
		return hand;		
	}
}
