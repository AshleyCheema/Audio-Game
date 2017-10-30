using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	public static AudioController Instance;

	public GameObject goAudioLis;
	public GameObject goAudioListners;
	private Transform[] _tranAudioListeners;

	public GameObject goDial;
	private float[] _dialRotation;
	private int _dialIndex;
	private float _rotateAmount;

	// Use this for initialization
	void Start()
	{
		if ( Instance == null )
		{
			Instance = this;
		}

		_tranAudioListeners = new Transform[ goAudioListners.transform.childCount ];
		_dialRotation = new float[ goAudioListners.transform.childCount ];

		_dialIndex = 0;
		_rotateAmount = 240;
		_rotateAmount /= _dialRotation.Length;
		Debug.Log( _rotateAmount );

		for ( int i = 0; i < goAudioListners.transform.childCount; ++i )
		{
			_tranAudioListeners[ i ] = goAudioListners.transform.GetChild( i ).transform;
			_tranAudioListeners[ i ].gameObject.SetActive( false );

			if ( i == 0 )
			{
				_dialRotation[ i ] = -90 + ( _rotateAmount * 0.5f );
			}
			else
			{
				_dialRotation[ i ] = _dialRotation[ i - 1 ] + _rotateAmount;
			}
		}

		goDial.transform.rotation.eulerAngles.Set( _dialRotation[ _dialIndex ], 0.0f, 0.0f );

		foreach ( float f in _dialRotation )
		{
			Debug.Log( f );
		}
	}

	// Update is called once per frame
	void Update()
	{
		if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
		{
			SetListenerPosition( 0 );
		}
		if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
		{
			SetListenerPosition( 1 );
		}
		if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
		{
			SetListenerPosition( 2 );
		}
	}

	public void SetListenerPosition( int a_audioIndex )
	{
		goAudioLis.transform.SetPositionAndRotation( _tranAudioListeners[ a_audioIndex ].position, Quaternion.identity );
		ActivateAudioSource( a_audioIndex );
	}

	public bool DialWithInRange( float a_float )
	{
		float controller = ConvertRotationFloat( a_float );
		if ( _dialIndex == 0 )
		{
			float min = ConvertRotationFloat( _dialRotation[ _dialIndex + 1] ) - 20;
			ConvertRotationFloat( min );
			float max = ConvertRotationFloat( _dialRotation[ -_dialIndex + 1] ) + 20;
			ConvertRotationFloat( max );

			// only check for position 1
			if ( controller > min && controller < max )
			{
				_dialIndex++;
				return true;
			}

		}
		else if ( _dialIndex == _dialRotation.Length - 1 )
		{
			float min = ConvertRotationFloat( _dialRotation[ _dialIndex -1 ] ) - 20;
			ConvertRotationFloat( min );
			float max = ConvertRotationFloat( _dialRotation[ -_dialIndex - 1 ] ) + 20;
			ConvertRotationFloat( max );
			// check for the second to last position

			if ( AngleBetween(controller, min, max ))
			{

				_dialIndex--;
				return true;
			}
		}
		else
		{
			float min = ConvertRotationFloat( _dialRotation[ _dialIndex + 1 ] ) - 20;
			ConvertRotationFloat( min );
			float max = ConvertRotationFloat( _dialRotation[ -_dialIndex + 1 ] ) + 20;
			ConvertRotationFloat( max );
			// one above
			if ( controller > min && controller < max )
			{
				_dialIndex++;
				return true;
			}

			min = ConvertRotationFloat( _dialRotation[ _dialIndex - 1 ] ) - 20;
			ConvertRotationFloat( min );
			max = ConvertRotationFloat( _dialRotation[ _dialIndex - 1 ] ) + 20;
			ConvertRotationFloat( max );
			// one below
			if ( controller > min && controller < max )
			{
				_dialIndex--;
				return true;
			}
		}
		return false;
	}

	float ConvertRotationFloat( float a_float )
	{
		float f = 360;

		if ( Mathf.Sign( a_float ) == -1 )
		{
			f += a_float;
		}
		else
		{
			f = a_float;
		}
		return f;
	}

	bool AngleBetween( float n, float a, float b )
	{
		n = ( 360 + ( n % 360 ) ) % 360;
		a = ( 3600000 + a ) % 360;
		b = ( 3600000 + b ) % 360;

		if ( a < b )
			return a <= n && n <= b;
		return a <= n || n <= b;
	}

	void ActivateAudioSource( int a_audioIndex )
	{
		for ( int i = 0; i < _tranAudioListeners.Length; ++i )
		{
			if ( i == a_audioIndex )
			{
				_tranAudioListeners[ a_audioIndex ].gameObject.SetActive( true );
			}
			else
			{
				if ( _tranAudioListeners[ i ].gameObject.activeInHierarchy )
				{
					_tranAudioListeners[ i ].gameObject.SetActive( false );
				}
			}
		}
	}
}
