using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;


public class Enemy02 : Enemy {

    public GameObject bullet;
    
    // Use this for initialization
    protected override void Start () {
        base.Start();     
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        AtackManager();
    }

    public void AtackManager() {
        float UltimoDisparo = 3;
        
        for (int i = 0; i < 4; i++){
            for (int e = 0; e < 4; e++) {
                Disparar(UltimoDisparo, bullet, transform, 90);
                UltimoDisparo += 0.2f;
            }
            UltimoDisparo += 0.6f;
        }
        UltimoDisparo = 12.5f;
        for (int i = 0; i < 4; i++) {
            for (int e = 0; e < 4; e++) {
                Disparar(UltimoDisparo, bullet, transform, -90);
                UltimoDisparo += 0.2f;
            }
            UltimoDisparo += 0.6f;
        }
        UltimoDisparo = 17.8f;
        for (int i = 0; i < 8; i++) {
            Disparar(UltimoDisparo, bullet, transform, -180);
            UltimoDisparo += 0.2f;
        }
    }
}
