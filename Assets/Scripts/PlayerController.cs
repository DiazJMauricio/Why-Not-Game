using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private int vidas;
    public int maxVidas = 3;

    public int maxEnergia = 15;
    private int energia;
    public bool energiaAlMax = false;
    public GameObject EspecialShootEfect;


    public bool invulnerable = false;
    Color colorOriginal;

    /// FUNCIONES MONOBEHAVIOUR

    void Start () {
        //  Alertas
        if (EspecialShootEfect == null) {
            Debug.LogWarning("EspecialShootEfect.GameObject = null in PlayerController [" + gameObject.name + "]");
        }
        //  -------

        vidas = maxVidas;
        colorOriginal = gameObject.GetComponent<SpriteRenderer>().color;
        
        EspecialShootEfect.SetActive(false);
    }
	
	void Update () {
        if (energia > maxEnergia) energia = maxEnergia;
        if (vidas > maxVidas) vidas = maxVidas;
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "EnemyBullet" && !invulnerable) {
            Hit();
        }
    }


    /// FUNCIONES PROPIAS
    
    public void CargarEnergia(int cant = 1) {
        
        
        energia += cant;
        if (energia > maxEnergia) energia = maxEnergia;
        if (energia < 0) energia = 0;
        energiaAlMax = (energia == maxEnergia);

        ActivarEfectoDisparoEspecial();
    }

    public void CargarVida(int cant = 1)
    {
        
        vidas += cant;
        if (vidas > maxVidas)   vidas = maxVidas;
        if (vidas < 0)          vidas = 0;
        
        if (vidas == 0) {
            Debug.Log("GAME OVER");
        }
    }

    public void ActivarEfectoDisparoEspecial() {

        energiaAlMax = (energia == maxEnergia);
        EspecialShootEfect.SetActive(energia == maxEnergia);
    }

    public void Hit() {
        StartCoroutine("_Hit");
    }

    public void Defuncion() {
        Destroy(gameObject);
    }


    /// CORRUTINAS

    IEnumerator _Hit() {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ShakeCamara shake = FindObjectOfType<ShakeCamara>();
        shake._ShakeCamara(0.5f);
        CargarVida(-1);

        invulnerable = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = colorOriginal;
        invulnerable = false;

        yield return null;
    }
}
