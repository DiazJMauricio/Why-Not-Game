using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour {

    public Enemy bullet;
    public GameObject bulletTrasera;
    public GameObject bulletEspecial;

    public GameObject bulletPosition;
    public GameObject bulletPosition2;
    
    private bool disparoDisponible = true;
    private bool disparoDisponible2 = true;
    private bool disparoDisponible3 = true;
    public float cadencia;
    private bool direccionDisparoTrasero;
    public int cantDisparosTraseros;
    public float cadenciaDTrasero;
    public float tiempoEntreDTraseros;

    PlayerController pc;

	// Use this for initialization
	void Start () {
        pc = gameObject.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetAxis("Fire1") == 1) {
            StartCoroutine("DisparoDelantero");
        }

        if (Input.GetButton("Fire2"))
        {
            StartCoroutine("DisparoTrasero");
        }
        if (Input.GetButton("Fire3") && pc.energiaAlMax)
        {
            StartCoroutine("DisparoEspecial");
        }
    }

    IEnumerator DisparoDelantero()
    {
        if (disparoDisponible)
        {
            disparoDisponible = false;
            GameObject bullet1 =Instantiate(bullet).gameObject;
            bullet1.transform.position = bulletPosition.transform.position;
            bullet1.tag = "PlayerBullet";
            yield return new WaitForSeconds(cadencia);
            disparoDisponible = true;
        }
    }
    IEnumerator DisparoEspecial()
    {
        if (disparoDisponible3)
        {
            disparoDisponible3 = false;
            GameObject bullet1 = Instantiate(bulletEspecial).gameObject;
            bullet1.transform.position = bulletPosition.transform.position;
            bullet1.tag = "PlayerBullet";
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
    }
}
