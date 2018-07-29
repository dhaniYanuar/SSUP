using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSceneMenu : MonoBehaviour {

    public GameObject UIScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!UIScore.activeSelf)
            {
                SceneManager.LoadScene("LogIn");
            }
        }
    }
}
