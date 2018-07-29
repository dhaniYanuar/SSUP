using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleSoal : MonoBehaviour {

    public float Timer = 30.0f;
    public Text label;
    public GameObject BG, Logo, BoxSoal;
    private string url_update = "http://testlaravel7.dev.ent.pens.ac.id/public/update";
    WWW www;
    WWWForm form;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Timer = 0.0f;
        }
        int intTimer = (int)Timer;
        label.text = "BONUS:" + intTimer.ToString();
    }

    public void Bonus()
    {
        Touch3D.Point += (int)Timer;
        StartCoroutine(Up());
        CoreMarker.saveScore();
    }

    IEnumerator Up()
    {

        form = new WWWForm();
        form.AddField("username", Data._username);
        form.AddField("password", Data._password);
        form.AddField("score", Touch3D.Point);

        www = new WWW(url_update, form);
        yield return www;

    }
}
