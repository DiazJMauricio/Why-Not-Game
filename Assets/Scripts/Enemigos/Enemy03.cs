using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy03 : Enemy {

    public GameObject bullet;

    // Use this for initialization
    new void Start () {
        PadreStar();
    }
	
	// Update is called once per frame
	new void Update () {
        PadreUpdate();
        AtackManager();
    }

    public void AtackManager()
    {
        float UltimoDisparo = 1;

        for (int i = 0; i < 6; i++) {
            Disparar(UltimoDisparo, bullet, transform, 90);
            UltimoDisparo += 2.0f; 
        }
    }
}
