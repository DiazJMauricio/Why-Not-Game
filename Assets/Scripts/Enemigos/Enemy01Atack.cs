using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy01Atack : Attack {

    public GameObject bullet;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();	
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        AtackManager();
	}

    public void AtackManager()
    {
        float UltimoDisparo = 2f;
        Disparar(UltimoDisparo, bullet, transform, 180);
        Disparar(UltimoDisparo, bullet, transform, 165);
        Disparar(UltimoDisparo, bullet, transform, 150);

        for (int i = 0; i < 2; i++)
        {
            UltimoDisparo += 2f;
            Disparar(UltimoDisparo, bullet, transform, 180);
            Disparar(UltimoDisparo, bullet, transform, 195);
            Disparar(UltimoDisparo, bullet, transform, 210);

            UltimoDisparo += 2f;
            Disparar(UltimoDisparo, bullet, transform, 180);
            Disparar(UltimoDisparo, bullet, transform, 165);
            Disparar(UltimoDisparo, bullet, transform, 150);
        }
        for (int i = 0; i < 5; i++)
        {
            Disparar(UltimoDisparo, bullet, transform, 180);
            UltimoDisparo += 0.2f;
        }
    }
}
