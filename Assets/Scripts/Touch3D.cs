using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch3D : MonoBehaviour {

    public static int Point;
    public int MinRandom, MaxRandom;
    public GameObject[] Soal;
    public int noRandom;
    public GameObject BG, Logo, BoxSoal;
    public int NoImage;
    private string url_update = "http://testlaravel7.dev.ent.pens.ac.id/public/update";
    WWW www;
    WWWForm form;

    public void Update()
    {
        Debug.Log(DefaultTrackableEventHandler.activeSoal);
        if(NoImage == DefaultTrackableEventHandler.activeSoal && CoreController.IdUI_AR == 2 && CoreMarker.activeTutorSoal == 0 && CoreMarker.DetectMarker == 1)
        {
            OnMouseDownP();
            //CoreMarker.intMarker[NoImage - 1] = 1;
            NoImage = NoImage + 40;
            //CoreMarker.saveMarker();
        }
    }

    public void OnMouseDownP()
    {
        BG.SetActive(true);
        Logo.SetActive(true);
        BoxSoal.SetActive(true);
        noRandom = Random.Range(MinRandom, MaxRandom);
        Soal[noRandom].SetActive(true);
    }

    public void Benar()
    {
        Point = Point + 50;
        StartCoroutine(Up());
        CoreMarker.saveScore();
        CoreMarker.intMarker[NoImage - 41] = 1;
        CoreMarker.saveMarker();
        Soal[noRandom].SetActive(false);
        BG.SetActive(false);
        Logo.SetActive(false);
        BoxSoal.SetActive(false);
        //gameObject.SetActive(false);
    }
    public void Salah()
    {
        Point = Point - 0;
        CoreMarker.intMarker[NoImage - 41] = 1;
        CoreMarker.saveMarker();
        Soal[noRandom].SetActive(false);
        BG.SetActive(false);
        Logo.SetActive(false);
        BoxSoal.SetActive(false);
        //gameObject.SetActive(false);
    }

    IEnumerator Up()
    {

        form = new WWWForm();
        form.AddField("username", Data._username);
        form.AddField("password", Data._password);
        form.AddField("score", Point);

        www = new WWW(url_update, form);
        yield return www;

    }

}
