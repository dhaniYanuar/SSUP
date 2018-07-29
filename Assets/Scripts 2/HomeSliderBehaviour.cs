using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSliderBehaviour : MonoBehaviour {
    [SerializeField] string url;

    private void OnMouseDown() {
        //open url
        Debug.Log("Open url \n" + url);
        Application.OpenURL(url);
    }
}
