using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject EspecialShootEfect;
    
    public int maxVidas = 3;
    public int maxEnergia = 15;

    [HideInInspector]
    public int vidasActuales;
    private int energia;

    

    [HideInInspector]
    public bool energiaAlMax = false;
    [HideInInspector]
    public bool onHit = false;
    [HideInInspector]
    public bool onDash = false;
    [HideInInspector]
    public bool invulnerable = false;

    private UIManager uiManager;


    /// FUNCIONES MONOBEHAVIOUR
    private void Awake()
    {
        vidasActuales = maxVidas;
    }
    void Start () {
        //  Alertas
        if (EspecialShootEfect == null) {
            Debug.LogWarning("EspecialShootEfect.GameObject = null in PlayerController [" + gameObject.name + "]");
        }
        //  -------
   
        vidasActuales = maxVidas;

        uiManager = FindObjectOfType<UIManager>();
        uiManager.ActualizarVidas(vidasActuales);

        EspecialShootEfect.SetActive(false);
    }
	
	void Update () {
        ColorPlayer();

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

    public void CargarVida(int cant = 1) {
        vidasActuales += cant;
        if (vidasActuales > maxVidas)   vidasActuales = maxVidas;
        if (vidasActuales < 0)          vidasActuales = 0;
        
        if (vidasActuales == 0) {
            Debug.Log("GAME OVER");
            FindObjectOfType<GameManager>().GameOver();
        }
        uiManager.ActualizarVidas(vidasActuales);
    }

    public void ActivarEfectoDisparoEspecial() {
        EspecialShootEfect.SetActive(energiaAlMax);
    }

    public void ColorPlayer() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRenderer.color = Color.white;
        if (energiaAlMax) { spriteRenderer.color = new Color(255/227, 255/101, 255/136, 0.5f); }// 227  101  136
        if (onDash) { spriteRenderer.color = new Color(0.3f,0.3f,0.3f,0.8f); }
        if (onHit) { spriteRenderer.color = new Color(1f, 0.1f, 0.1f, 0.9f); ; }
    }


    public void Hit() {
        StartCoroutine("_Hit");
    }

    public void Defuncion() {
        Destroy(gameObject);
    }


    /// CORRUTINAS

    IEnumerator _Hit() {
        ShakeCamara shake = FindObjectOfType<ShakeCamara>();
        shake._ShakeCamara(0.5f);
        CargarVida(-1);

        invulnerable = true;
        onHit = true;
        yield return new WaitForSeconds(0.2f);
        onHit = false;
        yield return new WaitForSeconds(0.2f);
        onHit = true;
        yield return new WaitForSeconds(0.2f);
        onHit = false;
        invulnerable = false;

        yield return null;
    }
}
