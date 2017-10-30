﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialControl : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObj;

	private GameObject collidingObject;
	private GameObject objectInHand;

	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input(( int )trackedObj.index); }
	}

	private Vector3 _rot;
	private int _index;
	private bool _hasTicked;

	private float _lastZ;
	private bool _triggerDown;
	private float _range = 20;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		_rot = gameObject.transform.rotation.eulerAngles;
		_index = 0;
		_hasTicked = true;

		_triggerDown = false;
	}

	public void OnTriggerEnter( Collider other )
	{
		SetCollidingObject(other);
	}

	public void OnTriggerStay( Collider other )
	{
		SetCollidingObject(other);
	}

	public void OnTriggerExit( Collider other )
	{
		if ( !collidingObject )
		{
			return;
		}

		collidingObject = null;
	}

	private void SetCollidingObject( Collider col )
	{
		if ( collidingObject )
		{
			return;
		}

		collidingObject = col.gameObject;
	}

	// Update is called once per frame
	void Update()
	{
		if ( Controller.GetHairTrigger() )
		{
		if ( collidingObject )
		{
			if ( collidingObject.tag == "Dial" )
			{
				if ( AudioController.Instance.DialWithInRange(gameObject.transform.rotation.eulerAngles.z))
				{

				}
			}
		}
		}
		if ( _hasTicked )
		{
			_hasTicked = false;
			_rot = gameObject.transform.rotation.eulerAngles;
		}

		//if ( Controller.GetHairTriggerDown() )
		//{
		//	if ( collidingObject )
		//	{
		//		if ( collidingObject.tag == "Dial" )
		//		{
		//			_lastZ = gameObject.transform.rotation.eulerAngles.z;
		//
		//			_triggerDown = true;
		//		}
		//	}
		//}
		//
		//if(Controller.GetHairTriggerUp())
		//{
		//	_triggerDown = false;
		//}
		//
		//if( _triggerDown )
		//{
		//	if ( collidingObject )
		//	{
		//		if ( collidingObject.tag == "Dial" )
		//		{
		//			if ( gameObject.transform.rotation.eulerAngles.z > _lastZ + 120 )
		//			{
		//				collidingObject.transform.rotation *= Quaternion.Euler(40, 0, 0);
		//
		//				_lastZ = gameObject.transform.rotation.eulerAngles.z;
		//			}
		//		}
		//	}
		//}
	}

	bool WithInRange( float a_float)
	{
		float min = a_float - _range;
		if(min < 0)
		{
			min = 360 + min;
		}

		float max = a_float + _range;
		if(max > 360)
		{
			max = max - 360;
		}

		if(GetDialRotation(gameObject.transform.rotation.eulerAngles.x ) > min && 
			GetDialRotation( gameObject.transform.rotation.eulerAngles.x ) < max )
		{
			return true;
		}

		return false;
	}

	public float GetDialRotation(float a_float)
	{
		float f = 360;

		if ( Mathf.Sign( a_float ) == -1 )
		{
			f += a_float;
		}

		return f;
	}

}
