using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01 : Enemy {
    public GameObject bullet;
    public GameObject bulletAPlayer;
    public Transform arma1;
    public Transform arma2;
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
        float UltimoDisparo = 4f;

        for (int i = 0; i < 15; i++) {
            Disparar(UltimoDisparo, bullet, arma1,  90 + i * 12);
            Disparar(UltimoDisparo, bullet, arma2, -90 - i * 12);
           
            UltimoDisparo += 0.1f;
        }
        UltimoDisparo += 1f;
        for (int i = 0; i < 19; i++) {
            Disparar(UltimoDisparo, bullet, arma1, -180 - i * 10);
        }
        UltimoDisparo += 2f;
        for (int i = 0; i < 19; i++) {
            Disparar(UltimoDisparo, bullet, arma2, 180 + i * 10);
            
        }
        for (int i = 0; i < 10; i++) {
            Disparar(UltimoDisparo, bulletAPlayer, arma1);
            Disparar(UltimoDisparo, bulletAPlayer, arma2);
            
            UltimoDisparo += 0.2f;
        }
        for (int i = 0; i < 10; i++) {
            Disparar(UltimoDisparo, bulletAPlayer, arma1);
            Disparar(UltimoDisparo, bulletAPlayer, arma2);
            
            UltimoDisparo += 0.2f;
        }
        UltimoDisparo += 1f;
        _reiniciarCiclo(UltimoDisparo, 4);
    }

    void _reiniciarCiclo(float time, float volverA) {
        personalTimer = Mathf.Round(personalTimer * 100) / 100;
        time = Mathf.Round(time * 100) / 100;
        if (personalTimer == time)
        {
            personalTimer = volverA;
        }
    }
}
