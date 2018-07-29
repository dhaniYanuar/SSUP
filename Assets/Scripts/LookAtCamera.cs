using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    public Transform target;
    private MeshRenderer myMesh;

    private void Start()
    {
        myMesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        myMesh.enabled = false;
    }
}
