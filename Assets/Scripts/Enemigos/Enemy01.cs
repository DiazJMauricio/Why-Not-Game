using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01 : Enemy {
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

    public void AtackManager() {
        float UltimoDisparo = 2f;
        Disparar(UltimoDisparo,bullet, transform, 180);
        Disparar(UltimoDisparo,bullet, transform, 165);
        Disparar(UltimoDisparo,bullet, transform, 150);

        for (int i = 0; i < 2; i++) {
            UltimoDisparo += 2f;
            Disparar(UltimoDisparo, bullet, transform, 180);
            Disparar(UltimoDisparo, bullet, transform, 195);
            Disparar(UltimoDisparo, bullet, transform, 210);
            
            UltimoDisparo += 2f;
            Disparar(UltimoDisparo, bullet, transform, 180);
            Disparar(UltimoDisparo, bullet, transform, 165);
            Disparar(UltimoDisparo, bullet, transform, 150);
        }
        for (int i = 0; i < 5; i++) {
            Disparar(UltimoDisparo, bullet, transform, 180);
            UltimoDisparo += 0.2f;
        }
    }
}
