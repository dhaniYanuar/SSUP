﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {
    public static string _username;
    public static string _password;
    public static int _score;
    public static int _id;
    public static string _birth;
    public static string _email;
    public static string _phone;
    public static string _occupation;
    public static int _status;


    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
