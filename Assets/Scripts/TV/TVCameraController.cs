using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVCameraController : MonoBehaviour
{
	public static TVCameraController Instance;

	public GameObject goTVCamera;
	public GameObject goTVScreen;
	public GameObject goCameraPositions;
	Transform[] tranCameraPositions;

	private float rtTimer;

	// Use this for initialization
	void Start ()
	{
		if(Instance == null)
		{
			Instance = this;
		}

		tranCameraPositions = new Transform[ goCameraPositions.transform.childCount ];
		for (int i = 0; i < goCameraPositions.transform.childCount; ++i )
		{
			tranCameraPositions[ i ] = goCameraPositions.transform.GetChild( i ).transform;
		}

		rtTimer = 0;

		SetCameraPosition( 0 );
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			SetCameraPosition( 0 );
		}
		if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
		{
			SetCameraPosition( 1 );
		}
		if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
		{
			SetCameraPosition( 2 );
		}

		if ( rtTimer < 0 )
		{
			rtTimer = 2.5f;
			SetRenderTexture();
		}
		else
		{
			rtTimer -= Time.deltaTime;
		}
	}

	public void SetCameraPosition(int a_positionIndex)
	{
		goTVCamera.transform.SetPositionAndRotation(tranCameraPositions[a_positionIndex].position, tranCameraPositions[a_positionIndex].rotation);
	}

	public void SetRenderTexture()
	{
		RenderTexture rt = new RenderTexture( Screen.width, Screen.height, 24 );
		goTVCamera.GetComponent<Camera>().targetTexture = rt;
		rt.Create();

		goTVScreen.GetComponent<MeshRenderer>().material.mainTexture = rt;
	}
}
