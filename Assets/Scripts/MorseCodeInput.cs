using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MorseCodeInput : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device Controller
	{
		get { return SteamVR_Controller.Input(( int )trackedObj.index); }
	}
	private GameObject collidingObject;

	private GameObject _morseCodeText;
	private GameObject _morseCodeTimer;

	private List<char> _values;

	/*
	 * Yes
	 * Y: - . - -, E: ., S: . . .
	 * N: - ., O: - - -
	 * */
	private float _timer;
	private bool _triggerDown;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		_morseCodeText = GameObject.Find("MorseCodeText");
		_morseCodeTimer = GameObject.Find("MorseCodeTimerText");
	}

	// Use this for initialization
	void Start ()
	{
		_values = new List<char>();
		_timer = 0;
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
	void Update ()
	{
		_morseCodeTimer.GetComponent<Text>().text = "Timer: " + _timer;

		if (Controller.GetHairTriggerDown())
		{
			if ( collidingObject )
			{
				if ( collidingObject.tag == "MorseCodeClicker" )
				{
					_triggerDown = true;
				}
			}
		}
		else if(Controller.GetHairTriggerUp())
		{
			if (collidingObject)
			{
				if (collidingObject.tag == "MorseCodeClicker")
				{
					SetValues ();
				}
				else if (collidingObject.tag == "MorseCodeSend") 
				{
					CheckCode ();
				}
				else if(collidingObject.tag == "MorseCodeReset")
				{
					_values.Clear();
					_morseCodeText.GetComponent<Text>().text = "Code Cleared";
				}
				_triggerDown = false;
			}

		}

		if(_triggerDown)
		{
			_timer += Time.deltaTime;
		}
	}

	private void CheckCode()
	{
		string returnString = "";

		for (int i = 0; i < _values.Count; ++i )
		{
			returnString = returnString + _values[i].ToString();
		}

		if(returnString ==  "-.---")
		{
			_morseCodeText.GetComponent<Text>().text = "Code: " + returnString + " string is NO";
		}
		else
		{
			_morseCodeText.GetComponent<Text>().text = "Code: " + returnString;
		}

	}

	private void SetValues()
	{

		if ( _timer <= 0.25f )
		{
			_values.Add('.');
		}
		else if ( _timer > 0.25f )
		{
			_values.Add('-');
		}

		_timer = 0;
	}
}
