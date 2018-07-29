using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CekUIARactive : MonoBehaviour {

    public GameObject UItreaserHunt, UImaps;
    public GameObject[] markerSoal;
    public GameObject markerMaps;
    public GameObject Maps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(CoreController.IdUI_AR == 0)
        {
            UItreaserHunt.SetActive(false);
            UImaps.SetActive(false);
            Maps.SetActive(false);
            /*for (int i = 0; i <= 19; i++)
            {
                markerSoal[i].SetActive(false);
            }
            markerMaps.SetActive(false);*/
        }
		if(CoreController.IdUI_AR == 2)
        {
            UItreaserHunt.SetActive(true);
            UImaps.SetActive(false);
            Maps.SetActive(false);
            /*for(int i = 0; i <= 19; i++)
            {
                markerSoal[i].SetActive(true);
            }
            markerMaps.SetActive(false);*/
        }
        if (CoreController.IdUI_AR == 1)
        {
            UItreaserHunt.SetActive(false);
            UImaps.SetActive(true);
            Maps.SetActive(true);
            /*for (int i = 0; i <= 19; i++)
            {
                markerSoal[i].SetActive(false);
            }
            markerMaps.SetActive(true);*/
        }
    }
}
