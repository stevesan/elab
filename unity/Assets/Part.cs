﻿using UnityEngine;
using System.Collections;

public class Part : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        Debug.Log("clicked "+gameObject.name);
    }
}
