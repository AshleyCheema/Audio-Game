using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCheck : MonoBehaviour
{
	private Vector3 _position;
	private Rigidbody _rb;

	private void Awake()
	{
		_rb = GetComponentInParent<Rigidbody>();
	}

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

	private void Update()
	{
		if(_rb.velocity.z < 0.1f && _rb.velocity.z > -0.1f)
		{
			_rb.velocity = new Vector3(0, 0, 0);
		}

		GameObject.Find("MorseCodeText").GetComponent<Text>().text = _rb.velocity.z.ToString();

	}
}
