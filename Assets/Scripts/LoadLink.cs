using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLink : MonoBehaviour {
	public void OpenWeb(string LinkWeb)
    {
        Application.OpenURL(LinkWeb);
    }
}
