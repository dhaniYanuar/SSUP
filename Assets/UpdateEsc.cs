using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEsc : MonoBehaviour {

    public GameObject objectUI, UIscore;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectUI.SetActive(true);
            UIscore.SetActive(false);
        }
    }
}
