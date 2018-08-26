using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreByKill : MonoBehaviour {

    public int points;

	void Start () {
        Health health = GetComponent<Health>();
        health.EventOnDead += SetScoreByHit;
	}

    void SetScoreByHit() {
        ManagerScore.shootScore += points;
    }
}
