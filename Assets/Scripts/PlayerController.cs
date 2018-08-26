using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject EspecialShootEfect;

    public int maxEnergia = 15;
    private int energia;

    [HideInInspector]
    public bool energiaAlMax = false;

    private UIManager uiManager;
    private Health health;
    private PlayerMoveManager moveManager;
    private ManagerGame managerGame;
    

    bool onHit;

    /// FUNCIONES MONOBEHAVIOUR
    private void Awake()
    {
        //  Alertas
        if (EspecialShootEfect == null)
        {
            Debug.LogWarning("EspecialShootEfect.GameObject = null in PlayerController [" + gameObject.name + "]");
        }
        //  -------
        
    }

    void Start () {
        uiManager = FindObjectOfType<UIManager>();
        health = GetComponent<Health>();
        moveManager = GetComponent<PlayerMoveManager>();
        managerGame = FindObjectOfType<ManagerGame>();

        health.EventOnHit += Hit;
        health.EventOnDead += managerGame.GameOver;

        uiManager.ActualizarVidas(health.GetActualHealth());

        EspecialShootEfect.SetActive(false);
    }

  

    void Update () {
        ColorPlayer();
    }


    /// FUNCIONES PROPIAS
    
    public void CargarEnergia(int cant = 1) {
        energia += cant;
        if (energia > maxEnergia) energia = maxEnergia;
        if (energia < 0) energia = 0;
        energiaAlMax = (energia == maxEnergia);

        uiManager.ActualizarEnergia(energia);
        EspecialShootEfect.SetActive(energiaAlMax);
        
    }


    public void ColorPlayer() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        
        spriteRenderer.color = Color.white;
        if (energiaAlMax) { spriteRenderer.color = new Color(255/227, 255/101, 255/136, 0.5f); }// 227  101  136
        if (moveManager.OnDash) { spriteRenderer.color = new Color(0.3f,0.3f,0.3f,0.8f); }
        if (onHit) { spriteRenderer.color = new Color(1f, 0.1f, 0.1f, 0.9f); ; }
    }


    public void Hit() {
        uiManager.ActualizarVidas(health.GetActualHealth());
        ManagerScore.damageScore -= 1000;
        StartCoroutine("_Hit");
    }
    public void Dead()
    {
        moveManager.encerrar = false;
        ShakeCamara shake = FindObjectOfType<ShakeCamara>();
        shake._ShakeCamara(0.5f, 0.2f);
    }
    



    /// CORRUTINAS
    
    IEnumerator _Hit() {
        ShakeCamara shake = FindObjectOfType<ShakeCamara>();
        shake._ShakeCamara(0.5f,0.1f);

        health.invulnerable = true;
        onHit = true;
        yield return new WaitForSeconds(0.2f);
        onHit = false;
        yield return new WaitForSeconds(0.2f);
        onHit = true;
        yield return new WaitForSeconds(0.2f);
        onHit = false;
        health.invulnerable = false;

        yield return null;
    }
}
