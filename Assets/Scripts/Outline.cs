using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{

	private Material _material;
	public Color outLineColour;
	private bool touching;
	public bool Touching
	{
		get { return touching; }
		set { touching = value; }
	}

	private void Start()
	{
		SetColour();
	}

	// Update is called once per frame
	void Update ()
	{
		if(touching)
		{
			SetHighlight();
		}
	}

	void SetColour()
	{
		_material.SetColor("_OutlineColour", outLineColour);
	}

	void SetHighlight()
	{
		_material.SetFloat("_OutlineWidth", 1.0f);
	}

	void ClearHighlight()
	{
		_material.SetFloat("_OutlineWidth", 0.0f);
	}
}
