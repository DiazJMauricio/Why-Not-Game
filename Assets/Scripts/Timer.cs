using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Timer : MonoBehaviour {

    private float personalTimer = 0;

    protected void SetSecondOnTimer(float time) {
        personalTimer = time;
    }

    protected void UpdateTimer() {
        personalTimer += Time.fixedDeltaTime;
        personalTimer = Mathf.Round(personalTimer * 100) / 100;
    }

    protected void FixedUpdateTimer()
    {
        personalTimer += Time.fixedDeltaTime;
        personalTimer = Mathf.Round(personalTimer * 100) / 100;
        Debug.Log(personalTimer + " " + gameObject.name);
    }

    protected float GetPersonalTimer() {
        return personalTimer;
    }
}
