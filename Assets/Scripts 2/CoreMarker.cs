using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreMarker : MonoBehaviour {

    public static int[] intMarker = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
    public GameObject[] objectMarker;
    public static string[] nameMarker = new string[] { "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at" };

    public static int activeTutorSoal;
    public static int activeTutorExperience;
    public static int DetectMarker;

    public GameObject TutorSoal, TutorExperience, Maps, Zine1, Zine2;
    // Use this for initialization
	void Start () {

    }

    public static void saveScore()
    {
        PlayerPrefs.SetInt("myScoreFix", Touch3D.Point);
        PlayerPrefs.Save();
    }

    public static void saveMarker()
    {
        for(int i = 0; i <= 19; i++)
        {
            PlayerPrefs.SetInt(nameMarker[i], intMarker[i]);
            PlayerPrefs.Save();
        }
    }

	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i <= 19; i++)
        {
            intMarker[i] = PlayerPrefs.GetInt(nameMarker[i]);
        }

        for (int i = 0; i <= 19; i++)
        {
            if (intMarker[i] != 0)
            {
                objectMarker[i].SetActive(false);
            }
        }
        Debug.Log(intMarker[0].ToString());
        Touch3D.Point = PlayerPrefs.GetInt("myScoreFix");

        if (TutorSoal.active)
            activeTutorSoal = 1;
        else
            activeTutorSoal = 0;

        if (TutorExperience.active || CoreController.IdUI_AR == 0 || CoreController.IdUI_AR == 2)
        {
            Maps.SetActive(false);
            Zine1.SetActive(false);
            Zine2.SetActive(false);
        }
            
        if(!TutorExperience.active && CoreController.IdUI_AR == 1)
        {
            Maps.SetActive(true);
            Zine1.SetActive(true);
            Zine2.SetActive(true);
        }
	}
}
