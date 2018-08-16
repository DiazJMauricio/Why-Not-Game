using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropp : MonoBehaviour {

    public int cantDrop;
    public GameObject drop;

    Health health;

	// Use this for initialization
	void Awake () {
        health = GetComponent<Health>();
        health.EventOnDead += IDrop;
	}

    void IDrop() {
        for (int i = 0; i < cantDrop; i++)
        {
            GameObject drop1 = Instantiate(drop).gameObject;
            drop1.transform.position = transform.position;
        }
        
    }
	
}
