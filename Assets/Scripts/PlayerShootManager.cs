using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootManager : MonoBehaviour {

    public Proyectile bullet1;
    private bool bullet1Enable = true;
    public float bullet1Cadencia;

    public GameObject canon1;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Fire1") == 1)
        {
            StartCoroutine("DisparoDelantero");
        }
        if (Input.GetButton("Fire2"))
        {
            //StartCoroutine("DisparoTrasero");
        }
        /*if (Input.GetButton("Fire3") && pc.energiaAlMax)
        {
            StartCoroutine("DisparoEspecial");
        }*/
    }

    IEnumerator DisparoDelantero()
    {
        if (bullet1Enable)
        {
            bullet1Enable = false;
            GameObject bullet = Instantiate(bullet1).gameObject;
            bullet.transform.position = canon1.transform.position;
            bullet.tag = "PlayerBullet";

            yield return new WaitForSeconds(bullet1Cadencia);
            bullet1Enable = true;
        }
    }
    /*
    IEnumerator DisparoEspecial()
    {
        if (disparoDisponible3)
        {
            disparoDisponible3 = false;
            GameObject bullet1 = Instantiate(bulletEspecial).gameObject;
            bullet1.transform.position = bulletPosition.transform.position;
            bullet1.tag = "PlayerEspecialBullet";
            pc.CargarEnergia(-pc.maxEnergia);
            pc.EspecialShootEfect.SetActive(false);
            yield return new WaitForSeconds(cadencia);
            disparoDisponible3 = true;
        }
    }

    IEnumerator DisparoTrasero()
    {
        if (disparoDisponible2)
        {
            disparoDisponible2 = false;
            for (int i = 0; i < cantDisparosTraseros; i++)
            {
                GameObject bullet2 = (GameObject)Instantiate(bulletTrasera);
                bullet2 b2 = bullet2.GetComponent<bullet2>();
                b2.direccion = direccionDisparoTrasero;
                bullet2.transform.position = bulletPosition2.transform.position;
                bullet2.tag = "PlayerBullet";
                direccionDisparoTrasero = !direccionDisparoTrasero;
                yield return new WaitForSeconds(tiempoEntreDTraseros);
            }
            yield return new WaitForSeconds(cadenciaDTrasero);
            disparoDisponible2 = true;
        }
    }*/
}
