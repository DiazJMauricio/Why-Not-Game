using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Timer {

    MoveWithPattern moveWithPattern;


	protected virtual void Awake () {
        SetSecondOnTimer(0f);
        moveWithPattern = GetComponent<MoveWithPattern>();
	}
	

	protected virtual void Update () {
        UpdateTimer();
	}




    //  Instancia un objeto con ciertas propiedades.
    protected virtual void Disparar(float time, GameObject bullet, Transform pos, float rotacion = 0)
    {
        
        if (!GameManager.pause)
        {
            time = Mathf.Round(time * 100) / 100;
            if (time == GetPersonalTimer())
            {
                rotacion = _presisarRotacion(rotacion);
                
                GameObject objeto = Instantiate(bullet).gameObject;
                objeto.transform.position = pos.position;
                objeto.transform.rotation = Quaternion.Euler(0, 0, rotacion);
            }
        }
    }
        /// funciones para Disparar
        float _presisarRotacion(float r)
        {
            if (moveWithPattern.inversion.y < 0) r += (180 - r) * 2;
            if (moveWithPattern.inversion.x < 0) r += (180 - r) * 2 + 180;
            return r;
        }        
}
