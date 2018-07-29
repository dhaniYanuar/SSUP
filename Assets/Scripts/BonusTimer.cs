using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusTimer : MonoBehaviour {

    public Text labelScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        labelScore.text = Touch3D.Point.ToString();
	}
}
