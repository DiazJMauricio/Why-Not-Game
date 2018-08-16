using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour {

    public int targetRate = 60;
	// Use this for initialization
	void Start () {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
	}
	
	// Update is called once per frame
	void Update () {
        if (targetRate != Application.targetFrameRate) {
            Application.targetFrameRate = targetRate;
        }
	}
}
