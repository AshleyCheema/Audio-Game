using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	public GameObject goAudioLis;
	public GameObject goAudioListners;
	Transform[] tranAudioListeners;

	// Use this for initialization
	void Start ()
	{
		tranAudioListeners = new Transform[goAudioListners.transform.childCount];
		for ( int i = 0; i < goAudioListners.transform.childCount; ++i )
		{
			tranAudioListeners[i] = goAudioListners.transform.GetChild(i).transform;
			tranAudioListeners[i].gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			SetListenerPosition(0);
		}
		if ( Input.GetKeyDown(KeyCode.Alpha2) )
		{
			SetListenerPosition(1);
		}
		if ( Input.GetKeyDown(KeyCode.Alpha3) )
		{
			SetListenerPosition(2);
		}
	}

	void SetListenerPosition(int a_audioIndex)
	{
		goAudioLis.transform.SetPositionAndRotation(tranAudioListeners[a_audioIndex].position, Quaternion.identity);
		ActivateAudioSource(a_audioIndex);
	}

	void ActivateAudioSource( int a_audioIndex )
	{
		for ( int i = 0; i < tranAudioListeners.Length; ++i )
		{
			if ( i == a_audioIndex )
			{
				tranAudioListeners[a_audioIndex].gameObject.SetActive(true);
			}
			else
			{
				if(tranAudioListeners[i].gameObject.activeInHierarchy)
				{
					tranAudioListeners[i].gameObject.SetActive(false);
				}
			}
		}
	}
}
