using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCheck : MonoBehaviour
{
	Vector3 _position;

	private void OnTriggerEnter( Collider other )
	{
		if ( other.tag == "DrawCheck" )
		{
			_position = gameObject.transform.parent.position;

			if ( gameObject.transform.parent.position != _position )
			{
				gameObject.transform.parent.position = _position;
			}
		}
	}

	private void OnTriggerStay( Collider other )
	{
		if ( other.tag == "DrawCheck" )
		{
			gameObject.transform.parent.position = _position;
		}
	}
}
