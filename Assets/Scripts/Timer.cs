using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timer : MonoBehaviour {

    private float personalTimer = 0;

    protected void SetSecondOnTimer(float time) {
        personalTimer = time;
    }

    protected void UpdateTimer() {
        personalTimer += Mathf.Round(Time.fixedDeltaTime * 100) / 100;
    }

    protected float GetPersonalTimer() {
        return Mathf.Round(personalTimer * 100) / 100;
    }
}
