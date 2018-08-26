using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScore : MonoBehaviour {

    public static int proyectileScore;
    public static int timeScore;
    public static int shootScore;
    public static int damageScore;

    private float time;

	// Use this for initialization
	void Start () {
        proyectileScore = 0;
        timeScore = 0;
        shootScore = 0;
        damageScore = 3000;

        time = 0;

        ManagerGame managerGame = FindObjectOfType<ManagerGame>();
        managerGame.LevelWin += MostrarScore;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
	}

    void MostrarScore() {
        int TotalScore;
        timeScore -= (int)time * 2;
        TotalScore = proyectileScore + timeScore + shootScore + damageScore;
        Debug.Log("proyectileScore: " + proyectileScore);
        Debug.Log("timeScore: " + timeScore);
        Debug.Log("shootScore: " + shootScore);
        Debug.Log("damageScore: " + damageScore);
        Debug.Log("Total Score = " + TotalScore);
        
    }
}
