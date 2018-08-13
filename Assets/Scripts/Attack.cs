using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Timer {

    MoveWithPattern moveWithPattern;
	// Use this for initialization
	protected virtual void Awake () {
        SetSecondOnTimer(0f);
        moveWithPattern = GetComponent<MoveWithPattern>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        UpdateTimer();
	}


    //  Instancia un objeto con ciertas propiedades.
    protected virtual void Disparar(float time, GameObject bullet, Transform pos, float rotacion = 0)
    {
        if (!GameManager.pause)
        {
            if (_controlDisparo(time))
            {
                GameObject objeto = _InstanciarEnemyBullet(bullet);
                rotacion = _presisarRotacion(rotacion);
                _setPositionYRotacionEnemyBullet(objeto, rotacion, pos);
            }
        }
    }
        /// funciones para Disparar
        private bool _controlDisparo(float time)
        {
            time = Mathf.Round(time * 100) / 100;
            return (GetPersonalTimer() == time);
        }
        float _presisarRotacion(float r)
        {
            if (moveWithPattern.inversion.y < 0) r += (180 - r) * 2;
            if (moveWithPattern.inversion.x < 0) r += (180 - r) * 2 + 180;
            return r;
        }
        private GameObject _InstanciarEnemyBullet(GameObject go)
        {
            GameObject objetoInstanciado = Instantiate(go).gameObject;
            objetoInstanciado.tag = "EnemyBullet";
            return objetoInstanciado;
        }
        private void _setPositionYRotacionEnemyBullet(GameObject bullet, float rotacion, Transform position)
        {
            bullet.transform.position = position.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, rotacion);
        }
}
