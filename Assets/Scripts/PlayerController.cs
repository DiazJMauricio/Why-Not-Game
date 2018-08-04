using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int vidasActuales;
    public int maxVidas = 3;

    public int maxEnergia = 15;
    private int energia;
    public bool energiaAlMax = false;
    public GameObject EspecialShootEfect;

    public UIManager uiManager;


    public bool invulnerable = false;
    Color colorOriginal;

    /// FUNCIONES MONOBEHAVIOUR

    void Start () {
        //  Alertas
        if (EspecialShootEfect == null) {
            Debug.LogWarning("EspecialShootEfect.GameObject = null in PlayerController [" + gameObject.name + "]");
        }
        //  -------

        vidasActuales = maxVidas;
        colorOriginal = gameObject.GetComponent<SpriteRenderer>().color;
        uiManager = FindObjectOfType<UIManager>();

        uiManager.ActualizarVidas(vidasActuales);

        EspecialShootEfect.SetActive(false);
    }
	
	void Update () {
        if (energia > maxEnergia) energia = maxEnergia;
        if (vidasActuales > maxVidas) vidasActuales = maxVidas;
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

        uiManager.ActualizarEnergia(energia);
        ActivarEfectoDisparoEspecial();
    }

    public void CargarVida(int cant = 1)
    {
        
        vidasActuales += cant;
        if (vidasActuales > maxVidas)   vidasActuales = maxVidas;
        if (vidasActuales < 0)          vidasActuales = 0;
        
        if (vidasActuales == 0) {
            Debug.Log("GAME OVER");
        }
        uiManager.ActualizarVidas(vidasActuales);
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
