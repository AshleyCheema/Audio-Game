using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desk : MonoBehaviour
{

	public GameObject draw001;
	public GameObject tempController;

	Vector3 _lastControllerPos;

	float minPos = 0.066f;
	float maxPos = 0.2f;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.RightArrow))
		{
			

			
			//if ( draw001.transform.localPosition.x >= minPos && draw001.transform.localPosition.x <= maxPos )
			//{
			//	draw001.transform.localPosition += new Vector3( 1.0f * Time.deltaTime, 0.0f, 0.0f );
			//}
			//if ( draw001.transform.localPosition.x > maxPos )
			//{
			//	draw001.transform.localPosition = new Vector3( maxPos, draw001.transform.localPosition.y, draw001.transform.localPosition.z );
			//}
		}
		if ( Input.GetKey( KeyCode.LeftArrow ) )
		{
			tempController.transform.position += new Vector3( -1.0f * Time.deltaTime, 0.0f, 0.0f );

			//if ( draw001.transform.localPosition.x >= minPos && draw001.transform.localPosition.x <= maxPos )
			//{
			//	draw001.transform.localPosition += new Vector3( -1.0f * Time.deltaTime, 0.0f, 0.0f );
			//}
			//if ( draw001.transform.localPosition.x < minPos )
			//{
			//	draw001.transform.localPosition = new Vector3( minPos, draw001.transform.localPosition.y, draw001.transform.localPosition.z );
			//}
		}
	}
}
